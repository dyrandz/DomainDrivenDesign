using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyTrader.Domain.Models.Money
{
    public class Balance
    {
        public Balance(IList<Money> currencies)
        {
            Currencies = currencies;
        }

        public IList<Money> Currencies { get; private set; }

        public void SetCurrencies(IList<Money> currencies)
        {
            Currencies = currencies;
        }

        public void Exchange(Money money, Currency to)
        {
            decimal rationBetweenCurrencies = Math.Round(money.Currency.Ratio / to.Ratio, 2);
            money = new Money(to, Math.Round((money.Amount * rationBetweenCurrencies * 100) / 100));

            var currencies = new List<Money>();

            foreach (var currency in Currencies)
            {
                if (money.Currency.Name == currency.Currency.Name)
                {
                    currencies.Add(
                        new Money(
                            money.Currency, currency.Amount + money.Amount
                        ) 
                    );
                }

                if (money.Currency.Name == to.Name)
                {
                    currencies.Add(
                        new Money(
                            money.Currency, currency.Amount - currency.Amount
                        )
                    );
                }
            }

            Currencies = currencies;
        }
    }
}
