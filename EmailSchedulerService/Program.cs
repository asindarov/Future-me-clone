using System.Reflection;
using EmailSchedulerService.Broker;
using EmailSchedulerService.Data;
using EmailSchedulerService.Features.Email.Services;
using EmailSchedulerService.Jobs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("Db");

// Quartz configurations
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey(nameof(PublishEmailJob));
    q.AddJob<PublishEmailJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(options => options
        .ForJob(jobKey)
        .WithIdentity("PublishEmailJob-job")
        .WithCronSchedule("0 * * ? * *"));

});
builder.Services.AddQuartzServer(options =>
{
    options.WaitForJobsToComplete = true;
});

builder.Services.AddSingleton<KafkaClientHandle>();
builder.Services.AddSingleton<KafkaDependantProducer<string, string>>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IEmailManager, EmailManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => 
    c.SwaggerEndpoint("/swagger/v1/swagger.json","Future Me")); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(); 