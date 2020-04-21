using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyTrader.Domain.Repository.Models
{
    public class CurrencyDto
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public decimal Ratio { get; set; }
    }
}
