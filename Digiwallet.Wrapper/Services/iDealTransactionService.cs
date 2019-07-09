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

namespace Digiwallet.Wrapper.Services
{
    public class IDealTransactionService : IIDealTransactionService
    {
        // private readonly DigiwalletSettings _settings;
        private readonly IHttpClientFactory _clientFactory;
        public IDealTransactionService(/*IOptions<DigiwalletSettings> digiwalletSettings, */ IHttpClientFactory clientFactory)
        {
            // this._settings = digiwalletSettings.Value;
            _clientFactory = clientFactory;
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

                return new StartTransactionResponse(apiResponse);
            }

            // Custom errors here: 
            throw new Exception("Failed to get response from API");
        }
    }
}
