using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyTrader.Domain.Config
{
    public static class SqlConnectionConfig
    {
        public static void RegisterSqlConnection(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));
        }
    }
}
