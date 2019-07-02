using DigiWalletAPIWrapper.Repositories;
using DigiWalletAPIWrapper.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DigiWalletAPIWrapper.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDigiWalletServices(this IServiceCollection services)
        {
            services.AddSingleton<IIDealIssuerRepository, IDealIssuerRepository>();
            services.AddHttpClient("digiwallet", c =>
            {
                c.BaseAddress = new System.Uri("https://transaction.digiwallet.nl/");
            });
            return services;
        }
    }
}
