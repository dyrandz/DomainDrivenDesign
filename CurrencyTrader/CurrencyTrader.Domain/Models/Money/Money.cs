using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyTrader.Domain.Models.Money
{
    public class Money
    {
        public Money(Currency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }
        public Currency Currency { get; }
        public decimal Amount { get; private set; }

        public void SetAmount(decimal amount)
        {
            Amount = amount;
        }
    }
}
