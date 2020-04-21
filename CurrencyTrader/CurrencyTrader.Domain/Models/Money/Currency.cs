using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CurrencyTrader.Domain.Models.Money
{
    public class Currency
    {
        public Currency(string name, decimal ratio)
        {
            Name = name;
            Ratio = ratio;
        }

        public string Name { get; }
        public decimal Ratio { get; }
    }
}
