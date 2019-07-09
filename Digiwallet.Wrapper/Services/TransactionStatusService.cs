﻿using Digiwallet.Wrapper.Models.TransactionStatus;
using Digiwallet.Wrapper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Digiwallet.Wrapper.Services
{
    public class TransactionStatusService : ITransactionStatusService
    {
        private readonly IHttpClientFactory _clientFactory;
        public TransactionStatusService(/*IOptions<DigiwalletSettings> digiwalletSettings, */ IHttpClientFactory clientFactory)
        {
            // this._settings = digiwalletSettings.Value;
            _clientFactory = clientFactory;
        }

        public async Task<TransactionStatusResponseModel> CheckTransaction(TransactionStatusRequestModel requestModel)
        {
            var client = this._clientFactory.CreateClient("digiwallet");

            var builder = new UriBuilder(client.BaseAddress)
            {
                Path = requestModel.ApiEndpoint
            };

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "ver", "4" },
                { "rtlo", requestModel.ShopID.ToString()},
                { "trxid", requestModel.TransactionID.ToString()},
                { "test", requestModel.TestMode ? "1" : "0"},
                { "once", requestModel.RestrictResponseCount ? "1" : "0"},
            });

            var request = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
            var response = await client.PostAsync(request.RequestUri, content);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                return new TransactionStatusResponseModel(apiResponse);
            }

            // Custom errors here: 
            throw new Exception("Failed to get response from API");
        }
    }
}