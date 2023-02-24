using Domain.Models;
using EmailSchedulerService.Data;
using EmailSchedulerService.Features.Email.Dtos;
using EmailSchedulerService.Features.Email.Requests.CreateEmail;
using EmailSchedulerService.Features.Email.Requests.GetEmails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailSchedulerService.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController
{
    private readonly IMediator _mediator;
    private readonly AppDbContext _appDbContext;

    public EmailController(IMediator mediator, AppDbContext appDbContext)
    {
        _mediator = mediator;
        _appDbContext = appDbContext;
    }

    [HttpPost("AddEmail")]
    public async Task<EmailDto> CreateEmailDetail([FromBody] EmailInputDto payload)
    {
        return await _mediator.Send(new CreateEmailRequest(payload));
    }

    [HttpGet(Name = "GetAllEmails")]
    public async Task<IQueryable<EmailDto>> GetAllAsync()
    {
        return await _mediator.Send(new GetEmailsRequest());
    }

    [HttpGet("count")]
    public Task<int> CountAllEmailsAsync()
    {
        return Task.FromResult(_appDbContext.EmailDetails.Count());
    }

    [HttpGet("Emails/{id:int}")]
    public async Task<EmailDetail> GetAsync(int id)
    {
        return await _appDbContext.EmailDetails.FirstOrDefaultAsync(e => e.Id == id);
    }
}