using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceClients.Domain.DTO;

namespace ServiceClients.Domain
{
    public interface IAmortizationCalculatorServiceClient
    {
        Task<Result<IEnumerable<MonthlyAmortizationDTO>>> CalculateMonthlyAmortizationAsync(LoanDTO loan);
    }
}