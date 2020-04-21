using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTrader.Domain.Repository.Models;

namespace CurrencyTrader.Domain.Repository.DataService.IDataService
{
    public interface IBalanceDataService
    {
        Task<IList<CurrencyDto>> GetCurrencyAsync();
        Task<IList<UserMoneyDto>> GetUserMoneyAsync(int userId);
        Task UpdateUserMoneyAmount(int userId, int currencyId, decimal amount);
    }
}
