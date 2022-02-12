using FluentResults;
using System.Threading.Tasks;
using ServiceClients.Domain.DTO;

namespace ServiceClients.Domain
{
    public interface IInterestRateCalculatorServiceClient
    {
        Task<Result<LoanInterestRateDTO>> CalculateLoanInterestRateAsync(LoanDTO loan);
    }
}