using DigiWalletAPIWrapper.Models.iDeal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigiWalletAPIWrapper.Repositories.Interfaces
{
    public interface IIDealIssuerRepository
    {
        Task<IEnumerable<IDealIssuerModel>> GetIssuers();
    }
}
