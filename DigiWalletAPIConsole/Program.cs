using DigiWalletAPIWrapper.Extensions;
using DigiWalletAPIWrapper.Repositories;
using DigiWalletAPIWrapper.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DigiWalletAPIConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IIDealIssuerRepository, IDealIssuerRepository>()
                .AddDigiWalletServices()
                .BuildServiceProvider();

            var issuerRepo = serviceProvider.GetService<IIDealIssuerRepository>();

            var issuers = issuerRepo.GetIssuers().GetAwaiter().GetResult();

            foreach (var issuer in issuers)
            {
                Console.WriteLine(string.Format("ID: {0}, Name: {1}", issuer.Id, issuer.BankName));
            }
        }
    }
}
