using Digiwallet.Wrapper.Models;
using Digiwallet.Wrapper.Models.Responses;
using Digiwallet.Wrapper.Models.Transaction;
using Digiwallet.Wrapper.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Digiwallet.Wrapper.Services
{
    public class IDealTransactionService : IIDealTransactionService
    {
        // private readonly DigiwalletSettings _settings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<IDealTransactionService> _logger;
        public IDealTransactionService(/*IOptions<DigiwalletSettings> digiwalletSettings, */ IHttpClientFactory clientFactory, ILogger<IDealTransactionService> logger)
        {
            // this._settings = digiwalletSettings.Value;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Starts an iDeal transaction. 
        /// </summary>
        /// <param name="transaction">IDeal transaction detail model.</param>
        /// <returns>Model holding details on start of transaction. </returns>
        public async Task<StartTransactionResponse> StartTransaction(IDealTransaction transaction)
        {
            var client = this._clientFactory.CreateClient("digiwallet");

            var builder = new UriBuilder(client.BaseAddress)
            {
                Path = transaction.StartApi
            };

            var content = new FormUrlEncodedContent( new Dictionary<string, string>
            {
                { "ver", "4" },
                { "rtlo", transaction.ShopID.ToString()},
                { "bank", transaction.Bank},
                { "app_id", "woonz.digiwalletwrapper in-dev"}, // Maybe get release version from NuGet build later on. 
                { "amount", transaction.Amount.ToString()},
                { "description", transaction.Description},
                { "reporturl", transaction.ReportUrl},
                { "returnurl", transaction.ReturnUrl},
                { "cancelurl", transaction.CancelUrl},
                { "test", "1"}
            });

            var request = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
            var response = await client.PostAsync(request.RequestUri, content);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                _logger.LogInformation(string.Format("Got API response: {0}", apiResponse));

                return new StartTransactionResponse(apiResponse);
            }

            _logger.LogWarning(string.Format("API Returned statuscode other than succes. ({0}, {1})", response.StatusCode, response.ReasonPhrase));

            // Custom errors here: 
            throw new Exception("Failed to get response from API");
        }
    }
}
