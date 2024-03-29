﻿using Digiwallet.Wrapper.Repositories;
using Digiwallet.Wrapper.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Digiwallet.Wrapper.Extensions
{
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Adds the services required for the DigiWalletAPI library to function. For now, this is just a HTTPClient
        /// </summary>
        public static IServiceCollection AddDigiWalletServices(this IServiceCollection services)
        {
            services.AddHttpClient("digiwallet", c =>
            {
                c.BaseAddress = new System.Uri("https://transaction.digiwallet.nl/");
            });
            return services;
        }
    }
}
