using EmailSchedulerService.Features.Email.Dtos;
using EmailSchedulerService.Features.Email.Requests.CreateEmail;
using EmailSchedulerService.Features.Email.Requests.GetEmails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmailSchedulerService.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController
{
    private readonly IMediator _mediator;

    public EmailController(IMediator mediator)
    {
        _mediator = mediator;
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

}