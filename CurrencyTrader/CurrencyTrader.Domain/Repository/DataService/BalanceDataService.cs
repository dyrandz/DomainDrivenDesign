using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTrader.Domain.Models.Money;
using CurrencyTrader.Domain.Repository.DataService.IDataService;
using CurrencyTrader.Domain.Repository.Models;
using Dapper;

namespace CurrencyTrader.Domain.Repository.DataService
{
    public class BalanceDataService : IBalanceDataService
    {
        private readonly IDbConnection _db;

        public BalanceDataService(IDbConnection db)
        {
            _db = db;
        }
        public async Task<IList<CurrencyDto>> GetCurrencyAsync()
        {
            const string sql = "SELECT CurrencyId, CurrencyName, Ratio FROM dbo.Currency";
            var result = await _db.QueryAsync<CurrencyDto>(sql);
            return result.ToList();
        }

        public async Task<IList<UserMoneyDto>> GetUserMoneyAsync(int userId)
        {
            var parameters = new DynamicParameters(new Dictionary<string, object>
            {
                { "@UserId", userId }
            });

            const string sql = "SELECT UserMoneyId, UserId, CurrencyId, Amount FROM dbo.UserMoney " +
                               "WHERE UserId = @UserId";
            var result = await _db.QueryAsync<UserMoneyDto>(sql, parameters);
            return result.ToList();
        }

        public async Task UpdateUserMoneyAmount(int userId, int currencyId, decimal amount)
        {
            var parameters = new DynamicParameters(new Dictionary<string, object>
            {
                { "@UserId", userId }, { "@CurrencyId", currencyId }, { "@Amount", amount }
            });
            const string sql =
                "UPDATE dbo.UserMoney SET Amount = @Amount WHERE CurrencyId = @CurrencyId AND UserId = @UserId";
            await _db.QueryAsync<UserMoneyDto>(sql, parameters);
        }
    }
}
