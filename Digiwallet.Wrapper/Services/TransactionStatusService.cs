using Digiwallet.Wrapper.Models.TransactionStatus;
using Digiwallet.Wrapper.Services.Interfaces;
using Digiwallet.Wrapper.Util;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Digiwallet.Wrapper.Services
{
    public class TransactionStatusService : DigiwalletApiBase, ITransactionStatusService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<TransactionStatusService> _logger; 
        public TransactionStatusService(/*IOptions<DigiwalletSettings> digiwalletSettings, */ IHttpClientFactory clientFactory, ILogger<TransactionStatusService> logger) : base(clientFactory, logger)
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
            var response = await this.ApiPost(requestModel.ApiEndpoint, new Dictionary<string, string>
            {
                { "rtlo", requestModel.ShopID.ToString()},
                { "trxid", requestModel.TransactionID.ToString()},
                { "test", requestModel.TestMode ? "1" : "0"},
                { "once", requestModel.RestrictResponseCount ? "1" : "0"}
            });

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
