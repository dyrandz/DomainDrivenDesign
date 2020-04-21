using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrencyTrader.Domain.Models.Money;
using CurrencyTrader.Domain.Repository.DataService.IDataService;
using MediatR;

namespace CurrencyTrader.Domain.Handlers.Commands
{
    public class ExchangeCommand : IRequest
    {
        public ExchangeCommand(Money money, Currency to, int userId)
        {
            Money = money;
            To = to;
            UserId = userId;
        }

        public int UserId { get; }
        public Money Money { get; }
        public Currency To { get; }

        internal sealed class ExchangeCommandHandler : IRequestHandler<ExchangeCommand>
        {
            private readonly IBalanceDataService _balance;

            public ExchangeCommandHandler(IBalanceDataService balance)
            {
                _balance = balance;
            }

            public async Task<Unit> Handle(ExchangeCommand command, CancellationToken token)
            {
                var currency = await _balance.GetCurrencyAsync();

                var result = await _balance.GetUserMoneyAsync(command.UserId);

                var currencies = result.Select(money =>
                    new Money(
                        new Currency(
                            currency.First(c => c.CurrencyId == money.CurrencyId).CurrencyName,
                            currency.First(c => c.CurrencyId == money.CurrencyId).Ratio
                        )
                        , money.Amount
                    )
                ).ToList();

                var balance = new Balance(currencies);
                balance.Exchange(command.Money, command.To);

                foreach (var balanceCurrency in balance.Currencies)
                {
                    var currencyId = currency.First(c => c.CurrencyName == balanceCurrency.Currency.Name).CurrencyId;
                    await _balance.UpdateUserMoneyAmount(command.UserId, currencyId, balanceCurrency.Amount);
                }
                return Unit.Value;
            }
        }
    }
}
