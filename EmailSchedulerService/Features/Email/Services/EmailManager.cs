using Domain.Models;
using EmailSchedulerService.Data;
using Microsoft.EntityFrameworkCore;
using Shared.EmailDetailsExtensions;

namespace EmailSchedulerService.Features.Email.Services;

public class EmailManager : IEmailManager
{
    private readonly AppDbContext _appDbContext;

    public EmailManager(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<IList<EmailDetail>> GetAllNotSentEmailDetails()
    {
        return await _appDbContext.EmailDetails.Where(ed => !ed.IsSent && ed.DeliveryDate.Date == DateTime.Today).Take(EmailsDetailConstants.DEFAULT_NUMBER_OF_EMAIL_DETAILS_TAKEN_FROM_DB).ToListAsync();
    }

    public async Task MarkAsSentAsync(EmailDetail emailDetail, bool save=true)
    {
        emailDetail.IsSent = true;
        
        if (save)
        {
            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task SaveAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
}