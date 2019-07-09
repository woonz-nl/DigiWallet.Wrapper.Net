using Digiwallet.Wrapper.Models.iDeal;
using Digiwallet.Wrapper.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Digiwallet.Wrapper.Repositories
{
    /// <summary>
    /// Implementation of the issuer repository. 
    /// For now, it consumes a HttpClientFactory itself (See HttpClient defined in <see cref="Extensions.IServiceCollectionExtension"/>)
    /// </summary>
    public class IDealIssuerRepository : IIDealIssuerRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<IDealIssuerRepository> _logger;

        public IDealIssuerRepository(IHttpClientFactory clientFactory, ILogger<IDealIssuerRepository> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Method returns iDeal issuers as supported by Digiwallet. 
        /// Issuers are fetched every time the function is called. May be changed to a cached value later on. 
        /// </summary>
        /// <returns>List of issuers</returns>
        public async Task<IEnumerable<IDealIssuerModel>> GetIssuers()
        {
            // TODO: Use a URI builder instead
            var request = new HttpRequestMessage(HttpMethod.Get, "ideal/getissuers?ver=4&format=xml");
           
            var client = _clientFactory.CreateClient("digiwallet");
            var response = await client.SendAsync(request);

            // TODO: Give some feedback if this fails. Maybe a warning, or a 'TryGetIssuers' function.
            if (response.IsSuccessStatusCode)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(IDealIssuersModel));
                var content = await response.Content.ReadAsStreamAsync();
                var result = (IDealIssuersModel) serializer.Deserialize(content);

                return result.Issuers;
            }

            _logger.LogWarning(string.Format("API Returned statuscode other than succes. ({0}, {1})", response.StatusCode, response.ReasonPhrase));
            return new List<IDealIssuerModel>() { new IDealIssuerModel() };
        }
    }
}
