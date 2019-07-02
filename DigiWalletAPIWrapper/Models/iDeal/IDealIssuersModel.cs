using System.Collections.Generic;
using System.Xml.Serialization;

namespace DigiWalletAPIWrapper.Models.iDeal
{
    [XmlType("issuer")]
    public class IDealIssuerModel
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlText]
        public string BankName { get; set; }
    }

    [XmlRoot("issuers")]
    public class IDealIssuersModel
    {
        public IDealIssuersModel()
        {
            this.Issuers = new List<IDealIssuerModel>();
        }
        [XmlElement("issuer")]
        public List<IDealIssuerModel> Issuers { get; set; }
    }
}
