using AutoMapper;
using ServiceClients.Domain.DTO;
using WebApi.Models;

namespace WebApi.Mapping
{
    public class LoanInterestRateProfile : Profile
    {
        public LoanInterestRateProfile()
        {
            CreateMap<LoanInterestRateDTO, LoanInterestRateModel>();
        }
    }
}
