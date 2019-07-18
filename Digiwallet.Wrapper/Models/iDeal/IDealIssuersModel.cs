using System.Collections.Generic;
using System.Xml.Serialization;

namespace Digiwallet.Wrapper.Models.iDeal
{
    /// <summary>
    /// Model to hold an iDeal issuer. 
    /// </summary>
    [XmlType("issuer")]
    public sealed class IDealIssuerModel
    {
        /// <summary>
        /// This ID is the ID used by DigiWallet. 
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }
        /// <summary>
        /// This is the bank's name, as specified by DigiWallet. 
        /// </summary>
        [XmlText]
        public string BankName { get; set; }
    }

    /// <summary>
    /// This model just holds a collection of iDeal issuers. It's main purpose is XML parsing.
    /// </summary>
    [XmlRoot("issuers")]
    public sealed class IDealIssuersModel
    {
        public IDealIssuersModel()
        {
            this.Issuers = new List<IDealIssuerModel>();
        }

        [XmlElement("issuer")]
        public List<IDealIssuerModel> Issuers { get; set; }
    }
}
