﻿using Digiwallet.Wrapper.Extensions;
using Digiwallet.Wrapper.Models.Responses;
using Digiwallet.Wrapper.Models.TransactionStatus;
using Digiwallet.Wrapper.Repositories;
using Digiwallet.Wrapper.Repositories.Interfaces;
using Digiwallet.Wrapper.Services;
using Digiwallet.Wrapper.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Digiwallet.Wrapper.Cmd
{
    class Program
    {
        public static void Main(string[] args)
        {
            AsyncMain().Wait();
        }

        public static async Task AsyncMain()
        {
            var configuration = GetConfigurationBuilder();
            var serviceProvider = GetServiceProvider(configuration);

            var issuerRepo = serviceProvider.GetService<IIDealIssuerRepository>();
            var iDealTransactionService = serviceProvider.GetService<IIDealTransactionService>();
            var transactionStatusService = serviceProvider.GetService<ITransactionStatusService>();
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogDebug("GETTING ISSUERS");
            var issuers = await issuerRepo.GetIssuers();

            logger.LogDebug(string.Format("Found {0} issuers", issuers.Count()));
            logger.LogDebug(string.Join("\n", issuers.Select(issuer => string.Format("ID: {0}, Name: {1}", issuer.Id, issuer.BankName))));

            var result = await iDealTransactionService.StartTransaction(new Models.Transaction.IDealTransaction()
            {
                ShopID = 149631,
                Amount = 2000,
                Bank = "ABNAL2A",
                Description = "Testing 1. 2.",
                CancelUrl = "http://development.woonz.nl/DigiWallet/cancel",
                ReturnUrl = "http://development.woonz.nl/DigiWallet/return",
                ReportUrl = "http://development.woonz.nl/DigiWallet/report"
            });

            logger.LogDebug(string.Format("== Transaction response ===\nStatuscode: {0}\nStatus enum: {1}\nResponse body {2}", result.StatusCode, result.Status, result.ResponseBody));
            logger.LogDebug($"Outbound URL: {result.OutboundUrl}");

            if (result.Status == StartTransactionResponseCodes.Started)
            {
                var checkModel = new TransactionStatusRequestModel() {
                    ApiEndpoint = "ideal/check",
                    ShopID = 149631,
                    TransactionID = result.TransactionNr,
                    RestrictResponseCount = true, 
                    TestMode = true
                };

                logger.LogDebug("Started transaction check loop, press ctrl + c to quit");

                while (true)
                {
                    var currentStatus = await transactionStatusService.CheckTransaction(checkModel);
                    logger.LogInformation(string.Format("== Current Status ===\nStatuscode: {0}\nStatus enum: {1}\nResponse body {2}", currentStatus.StatusCode, currentStatus.Status, currentStatus.ResponseBody));
                    Thread.Sleep(10000);
                }
            }
            else
            {
                logger.LogWarning("Failed to start transaction");
            }
        }

        public static IConfigurationRoot GetConfigurationBuilder()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // Currently not used, but can hold the Digiwallet settings 'salt' and 'api key'. 
                // Those params aren't used in Api v3.
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            return configuration;
        }

        public static ServiceProvider GetServiceProvider(IConfigurationRoot configuration)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IIDealIssuerRepository, IDealIssuerRepository>()
                .AddSingleton<ITransactionStatusService, TransactionStatusService>()
                .AddDigiWalletServices()
                .AddOptions()
                .AddSingleton<IIDealTransactionService, IDealTransactionService>()
                .AddLogging(builder => builder
                    .AddConsole()
                    .AddFilter(level => level >= LogLevel.Debug)
                )
                // Maps the Digiwallet config options to the settings model, and adds it for injection. 
                // Since none of the settings are currently in use, this has been commented out.
                //.Configure<DigiwalletSettings>(options => {
                //    configuration.Bind("Digiwallet", options);
                //})
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
