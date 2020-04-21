using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTrader.Domain.Handlers.Commands;
using CurrencyTrader.Domain.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyTrader.Domain.Controllers
{
    [Route("money/user")]
    public class MoneyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoneyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}/balance")]
        public async Task<IActionResult> GetBalance(int userId)
        {
            var response = await _mediator.Send(new BalanceQuery(userId));
            return Ok(response);
        }

        [HttpPost("{userId}/exchange")]
        public async Task<IActionResult> Exchange([FromRoute]int userId, [FromBody] ExchangeCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("{userId}/send")]
        public async Task<IActionResult> SendMoney([FromRoute] int userId, [FromBody] SendMoneyCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
