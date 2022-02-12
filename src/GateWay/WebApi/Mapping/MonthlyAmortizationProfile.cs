using AutoMapper;
using ServiceClients.Domain.DTO;
using WebApi.Models;

namespace WebApi.Mapping
{
    public class MonthlyAmortizationProfile : Profile
    {
        public MonthlyAmortizationProfile()
        {
            CreateMap<MonthlyAmortizationDTO, MonthlyAmortizationModel>();
        }
    }
}
