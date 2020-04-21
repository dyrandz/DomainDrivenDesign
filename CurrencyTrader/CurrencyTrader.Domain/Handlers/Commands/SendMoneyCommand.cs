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
    public class SendMoneyCommand : IRequest
    {
        public SendMoneyCommand(int userId, Money money, int receiverUserId)
        {
            UserId = userId;
            Money = money;
            ReceiverUserId = receiverUserId;
        }

        public int UserId { get; }
        public Money Money { get; }
        public int ReceiverUserId { get; }

        internal sealed class SendMoneyCommandHandler : IRequestHandler<SendMoneyCommand>
        {
            private readonly IBalanceDataService _balance;

            public SendMoneyCommandHandler(IBalanceDataService balance)
            {
                _balance = balance;
            }

            public async Task<Unit> Handle(SendMoneyCommand command, CancellationToken token)
            {
                var currency = await _balance.GetCurrencyAsync();

                var currencyId = currency.First(c => c.CurrencyName == command.Money.Currency.Name).CurrencyId;

                await _balance.UpdateUserMoneyAmount(command.ReceiverUserId, currencyId, command.Money.Amount);

                return Unit.Value;
            }
        }
    }
}
