using FluentResults;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using ServiceClients.Configuration.InterestRateCalculatorService;
using ServiceClients.Domain.DTO;
using ServiceClients.Domain;

namespace ServiceClients.InterestRateCalculator
{
    public class InterestRateCalculatorServiceClient : IInterestRateCalculatorServiceClient
    {
        private readonly IInterestRateCalculatorServiceConfiguration _serviceConfiguration;

        private const string _loanInterestRateCalculatorUrl = "LoanInterestRateCalculator";

        public InterestRateCalculatorServiceClient(IInterestRateCalculatorServiceConfiguration serviceConfiguration)
        {
            _serviceConfiguration = serviceConfiguration;
        }
        public async Task<Result<LoanInterestRateDTO>> CalculateLoanInterestRateAsync(LoanDTO loan)
        {
            using var client = new HttpClient();

            var endpointUriBuilder = new UriBuilder(_serviceConfiguration.HostAddress);
            var uriQuery = HttpUtility.ParseQueryString(endpointUriBuilder.Query);
            uriQuery["amout"] = loan.Amount.ToString();
            uriQuery["termYears"] = loan.TermYears.ToString();
            uriQuery["montlyPayment"] = loan.MonthlyPayment.ToString();
            endpointUriBuilder.Query = uriQuery.ToString();
            endpointUriBuilder.Path = _loanInterestRateCalculatorUrl;

            var response = await client.GetAsync(endpointUriBuilder.Uri);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return Result.Fail("Error during request to calculate Loan Interest Rate");
            }

            var calculationResult = JsonConvert.DeserializeObject<InterestRateCalculation>(content);

            if (!calculationResult.IsSuccess)
            {
                return Result.Fail(calculationResult.ErrorMessage);
            }
            var loanInterestRate = new LoanInterestRateDTO(calculationResult.Value.TotalPayment,
                calculationResult.Value.TotalInterestPaid,
                calculationResult.Value.InterestRate);

            return Result.Ok(loanInterestRate);
        }
    }
}
