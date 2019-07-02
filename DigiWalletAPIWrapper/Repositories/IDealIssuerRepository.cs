using DigiWalletAPIWrapper.Models.iDeal;
using DigiWalletAPIWrapper.Repositories.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigiWalletAPIWrapper.Repositories
{
    public class IDealIssuerRepository : IIDealIssuerRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public IDealIssuerRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<IDealIssuerModel>> GetIssuers()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "ideal/getissuers?ver=4&format=xml");
           
            var client = _clientFactory.CreateClient("digiwallet");
            var response = await client.SendAsync(request);

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
