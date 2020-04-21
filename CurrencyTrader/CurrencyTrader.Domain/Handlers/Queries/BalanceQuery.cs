using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrencyTrader.Domain.Models.Money;
using CurrencyTrader.Domain.Repository.DataService.IDataService;
using MediatR;
using Microsoft.Extensions.Primitives;

namespace CurrencyTrader.Domain.Handlers.Queries
{
    public sealed class BalanceQuery : IRequest<Balance>
    {
        public BalanceQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }

        internal sealed class BalanceQueryHandler : IRequestHandler<BalanceQuery, Balance>
        {
            private readonly IBalanceDataService _balance;

            public BalanceQueryHandler(IBalanceDataService balance)
            {
                _balance = balance;
            }

            public async Task<Balance> Handle(BalanceQuery query, CancellationToken token)
            {
                var currency = await _balance.GetCurrencyAsync();
                var balance = await _balance.GetUserMoneyAsync(query.UserId);

                var currencies = balance.Select(money => 
                    new Money(
                        new Currency(
                            currency.First(c => c.CurrencyId == money.CurrencyId).CurrencyName,
                            currency.First(c => c.CurrencyId == money.CurrencyId).Ratio
                        )
                        , money.Amount
                    )
                ).ToList();

                return new Balance(currencies);
            }
        }
    }
}
