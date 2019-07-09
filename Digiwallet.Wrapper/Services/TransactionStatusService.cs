using Digiwallet.Wrapper.Models.TransactionStatus;
using Digiwallet.Wrapper.Services.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TransactionStatusService> _logger; 
        public TransactionStatusService(/*IOptions<DigiwalletSettings> digiwalletSettings, */ IHttpClientFactory clientFactory, ILogger<TransactionStatusService> logger)
        {
            // this._settings = digiwalletSettings.Value;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Checks transaction state with DigiWallet. This functionality is the same for every transaction provider. 
        /// Currently, you need to set the API endpoints manually in the <see cref="TransactionStatusRequestModel"/>. 
        /// Make sure they match the values in the <see cref="Models.Transaction.TransactionBase.CheckApi"/> implementation value. 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
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
                { "app_id", "woonz.digiwalletwrapper in-dev"}, // Maybe get release version from NuGet build later on. 
                { "test", requestModel.TestMode ? "1" : "0"},
                { "once", requestModel.RestrictResponseCount ? "1" : "0"},
            });

            var request = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
            var response = await client.PostAsync(request.RequestUri, content);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();

                _logger.LogInformation(string.Format("Got API response: {0}", apiResponse));

                return new TransactionStatusResponseModel(apiResponse);
            }

            _logger.LogWarning(string.Format("API Returned statuscode other than succes. ({0}, {1})", response.StatusCode, response.ReasonPhrase));

            // Custom errors here: 
            throw new Exception("Failed to get response from API");
        }
    }
}
