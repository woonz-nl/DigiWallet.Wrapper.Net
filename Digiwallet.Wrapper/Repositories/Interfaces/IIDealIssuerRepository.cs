using Digiwallet.Wrapper.Models.iDeal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Digiwallet.Wrapper.Repositories.Interfaces
{
    /// <summary>
    /// Interface that defines a repository that can return a list of iDeal issuers. 
    /// </summary>
    public interface IIDealIssuerRepository
    {
        Task<IEnumerable<IDealIssuerModel>> GetIssuers();
    }
}
