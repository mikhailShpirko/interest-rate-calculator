using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorModel))]
    public class BaseApiController : ControllerBase
    {
        protected IActionResult BadRequest(IEnumerable<IReason> reasons)
        {
            var response = new BadRequestModel
            {
                ErrorMessages = reasons.Select(x => x.Message)
            };

            return BadRequest(response);
        }
    }
}
