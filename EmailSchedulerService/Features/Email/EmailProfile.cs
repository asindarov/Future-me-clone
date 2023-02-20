using AutoMapper;
using Domain.Models;
using EmailSchedulerService.Features.Email.Dtos;

namespace EmailSchedulerService.Features.Email;

public class EmailProfile : Profile
{
    public EmailProfile()
    {
        CreateMap<EmailDetail, EmailDto>().ReverseMap();
        CreateMap<EmailInputDto, EmailDetail>();
    }
}