using Domain.Models;
using EmailSchedulerService.Data;
using Microsoft.EntityFrameworkCore;

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
        return await _appDbContext.EmailDetails.Where(ed => !ed.IsSent).ToListAsync();
    }

    public async Task MarkAsSent(EmailDetail emailDetail)
    {
        emailDetail.IsSent = true;
        
        await _appDbContext.SaveChangesAsync();
    }
}