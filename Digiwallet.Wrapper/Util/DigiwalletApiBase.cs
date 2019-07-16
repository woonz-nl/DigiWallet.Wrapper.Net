using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Digiwallet.Wrapper.Util
{
    public abstract class DigiwalletApiBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<DigiwalletApiBase> _logger;
        public DigiwalletApiBase(IHttpClientFactory clientFactory, ILogger<DigiwalletApiBase> logger)
        {
            this._clientFactory = clientFactory;
            this._logger = logger;
        }

        /// <summary>
        /// Does an API post with the specified params. These are posted as post body. 
        /// Takes away some of the boilerplate http client stuff. 
        /// </summary>
        /// <param name="apiMethod">API Post (/provider/start) </param>
        /// <param name="values">Key-value dictionary of values</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ApiPost(string apiMethod, IDictionary<string, string> values)
        {
            var client = this._clientFactory.CreateClient("digiwallet");

            var builder = new UriBuilder(client.BaseAddress)
            {
                Path = apiMethod
            };

            var requestParams = new Dictionary<string, string>
            {
                { "ver", "4" },
                { "app_id", "woonz.digiwalletwrapper in-dev"}, // Maybe get release version from NuGet build later on. 
                // Maybe set test param here.
            };

            foreach (var param in values)
                if (!requestParams.TryAdd(param.Key, param.Value))
                    this._logger.LogWarning(string.Format("Key {0} with value {1} already exists in params", param.Key, param.Value));
            
            var content = new FormUrlEncodedContent(requestParams);
            var request = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
            
            return await client.PostAsync(request.RequestUri, content);
        }
    }
}
