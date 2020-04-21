using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTrader.Domain.Repository.DataService;
using CurrencyTrader.Domain.Repository.DataService.IDataService;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyTrader.Domain.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IBalanceDataService, BalanceDataService>();

            return services;
        }
    }
}
