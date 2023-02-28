using EmailSenderService;
using EmailSenderService.Configurations;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<EmailSenderKafkaConsumerConfiguration>(
            context.Configuration.GetSection(nameof(EmailSenderKafkaConsumerConfiguration))
        );
        
        services.AddHostedService<EmailDetailsConsumerHostedService>();
    })
    .Build();
Console.WriteLine("Hello world!");

host.Run();