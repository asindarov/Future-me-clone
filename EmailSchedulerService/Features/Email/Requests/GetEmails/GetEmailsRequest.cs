using AutoMapper;
using EmailSchedulerService.Data;
using EmailSchedulerService.Features.Email.Dtos;
using MediatR;

namespace EmailSchedulerService.Features.Email.Requests.GetEmails;

public class GetEmailsRequest : IRequest<IQueryable<EmailDto>>
{ }

public class GetEmailsHandler : IRequestHandler<GetEmailsRequest, IQueryable<EmailDto>>
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public GetEmailsHandler(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    
    public Task<IQueryable<EmailDto>> Handle(GetEmailsRequest request, CancellationToken cancellationToken)
    {
        var emailDetails = _appDbContext.EmailDetails.AsQueryable();

        var emailDtos = _mapper.ProjectTo<EmailDto>(emailDetails);

        return Task.FromResult(emailDtos);
    }
}