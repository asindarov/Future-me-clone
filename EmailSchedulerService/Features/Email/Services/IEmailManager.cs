using Domain.Models;

namespace EmailSchedulerService.Features.Email.Services;

public interface IEmailManager
{
    // TODO: load only needed properties of email details for performance sake!!!
    Task<IList<EmailDetail>> GetAllNotSentEmailDetails();
    Task MarkAsSentAsync(EmailDetail emailDetail, bool save=true);

    Task SaveAsync();
}