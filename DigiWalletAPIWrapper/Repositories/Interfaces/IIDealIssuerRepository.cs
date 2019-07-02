using DigiWalletAPIWrapper.Models.iDeal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigiWalletAPIWrapper.Repositories.Interfaces
{
    /// <summary>
    /// Interface that defines a repository that can return a list of iDeal issuers. 
    /// </summary>
    public interface IIDealIssuerRepository
    {
        Task<IEnumerable<IDealIssuerModel>> GetIssuers();
    }
}
