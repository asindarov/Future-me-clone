using AutoMapper;
using Domain.Models;
using EmailSchedulerService.Data;
using EmailSchedulerService.Features.Email.Dtos;
using MediatR;

namespace EmailSchedulerService.Features.Email.Requests.CreateEmail;

public class CreateEmailRequest : IRequest<EmailDto>
{
    public CreateEmailRequest(EmailInputDto payload)
    {
        Payload = payload;
    }

    public EmailInputDto Payload { get; }
}

public class CreateEmailHandler : IRequestHandler<CreateEmailRequest, EmailDto>
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CreateEmailHandler(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    
    public async Task<EmailDto> Handle(CreateEmailRequest request, CancellationToken cancellationToken)
    {
        var emailDetail = _mapper.Map<EmailDetail>(request.Payload);
        var createdEmail = await _appDbContext.AddAsync(emailDetail, cancellationToken);

        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        var createdEmailDto = _mapper.Map<EmailDto>(createdEmail.Entity);
        
        return createdEmailDto;
    }
}
