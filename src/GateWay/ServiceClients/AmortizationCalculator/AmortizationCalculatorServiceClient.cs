using Grpc.Net.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmortizationCalculatorService;
using ServiceClients.Configuration.AmortizationCalculatorService;
using FluentResults;
using ServiceClients.Domain;
using ServiceClients.Domain.DTO;

namespace ServiceClients.AmortizationCalculator
{
    public class AmortizationCalculatorServiceClient : IAmortizationCalculatorServiceClient
    {
        private readonly IAmortizationCalculatorServiceConfiguration _serviceConfiguration;

        public AmortizationCalculatorServiceClient(IAmortizationCalculatorServiceConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }
        public async Task<Result<IEnumerable<MonthlyAmortizationDTO>>> CalculateMonthlyAmortizationAsync(LoanDTO loan)
        {
            using var channel = GrpcChannel.ForAddress(_serviceConfiguration.HostAddress);
            var client = new AmortizationCalculatorService.AmortizationCalculator.AmortizationCalculatorClient(channel);
            var calculationResult = await client.CalculateMonthlyAmortizationAsync(
                                new Loan
                                {
                                    Amount = loan.Amount,
                                    TermYears = loan.TermYears,
                                    MonthlyPayment = loan.MonthlyPayment
                                });

            if (!calculationResult.IsSuccess)
            {
                return Result.Fail(calculationResult.ErrorMessage);
            }

            var monthlyAmortization = calculationResult
                .Data
                .Select(a =>
                    new MonthlyAmortizationDTO(a.MonthNumber,
                        a.Payment,
                        a.Balance));

            return Result.Ok(monthlyAmortization);
        }
    }
}
