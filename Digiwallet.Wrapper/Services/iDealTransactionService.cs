using Digiwallet.Wrapper.Models.Responses;
using Digiwallet.Wrapper.Models.Transaction;
using Digiwallet.Wrapper.Services.Interfaces;
using Digiwallet.Wrapper.Util;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Digiwallet.Wrapper.Services
{
    public sealed class IDealTransactionService : DigiwalletApiBase, IIDealTransactionService
    {
        // private readonly DigiwalletSettings _settings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<IDealTransactionService> _logger;
        public IDealTransactionService(/*IOptions<DigiwalletSettings> digiwalletSettings, */ IHttpClientFactory clientFactory, ILogger<IDealTransactionService> logger) : base(clientFactory, logger)
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
            var response = await this.ApiPost(transaction.StartApi, new Dictionary<string, string>
            {
                { "rtlo", transaction.ShopID.ToString()},
                { "bank", transaction.Bank},
                { "amount", transaction.Amount.ToString()},
                { "description", transaction.Description},
                { "reporturl", transaction.ReportUrl},
                { "returnurl", transaction.ReturnUrl},
                { "cancelurl", transaction.CancelUrl},
                { "test", "1"}
            });

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
