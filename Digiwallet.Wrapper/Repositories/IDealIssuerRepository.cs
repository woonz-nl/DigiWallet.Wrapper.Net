using Digiwallet.Wrapper.Models.iDeal;
using Digiwallet.Wrapper.Repositories.Interfaces;
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

        public IDealIssuerRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
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

            return new List<IDealIssuerModel>() { new IDealIssuerModel() };
        }
    }
}
