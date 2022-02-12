using AutoMapper;
using ServiceClients.Domain.DTO;
using WebApi.Models;

namespace WebApi.Mapping
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<LoanModel, LoanDTO>()
                 .ConstructUsing(x => new LoanDTO(x.Amount, 
                                            x.MonthlyPayment, 
                                            x.TermYears));
        }
    }
}
