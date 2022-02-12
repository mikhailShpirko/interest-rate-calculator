using Microsoft.AspNetCore.Mvc;
using IAmortizationCalculator = 
    ServiceClients.Domain.IAmortizationCalculatorServiceClient;
using IInterestRateCalculator = 
    ServiceClients.Domain.IInterestRateCalculatorServiceClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApi.Models;
using AutoMapper;
using ServiceClients.Domain.DTO;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanInterestRateController : BaseApiController
    {

        private readonly IAmortizationCalculator _amortizationCalculator;
        private readonly IInterestRateCalculator _interestRateCalculator;
        private readonly IMapper _mapper;

        public LoanInterestRateController(IAmortizationCalculator amortizationCalculator,
            IInterestRateCalculator interestRateCalculator,
            IMapper mapper)
        {
            _amortizationCalculator = amortizationCalculator;
            _interestRateCalculator = interestRateCalculator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoanModel))]        
        public async Task<IActionResult> CalculateAsync([FromQuery] LoanModel loanModel)
        {
            try
            {
                var loanDTO = _mapper.Map<LoanDTO>(loanModel);
                var interestRateCalculationResult = await _interestRateCalculator
                    .CalculateLoanInterestRateAsync(loanDTO);

                if(!interestRateCalculationResult.IsSuccess)
                {
                    return BadRequest(interestRateCalculationResult.Reasons);
                }

                var loanInterestRate = _mapper
                    .Map<LoanInterestRateModel>(interestRateCalculationResult.Value);

                var monthlyAmortizationCalculationResult = await _amortizationCalculator
                    .CalculateMonthlyAmortizationAsync(loanDTO);

                if (!monthlyAmortizationCalculationResult.IsSuccess)
                {
                    return BadRequest(monthlyAmortizationCalculationResult.Reasons);
                }

                loanInterestRate.MonthlyAmortization = _mapper
                    .Map<IEnumerable<MonthlyAmortizationModel>>(monthlyAmortizationCalculationResult.Value);

                return Ok(loanInterestRate);
            }
            catch
            {
                return StatusCode(500, new InternalServerErrorModel());
            }
        }
    }
}
