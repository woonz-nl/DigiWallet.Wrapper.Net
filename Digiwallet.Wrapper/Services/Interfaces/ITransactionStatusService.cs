using Digiwallet.Wrapper.Models.TransactionStatus;
using System.Threading.Tasks;

namespace Digiwallet.Wrapper.Services.Interfaces
{
    /// <summary>
    /// Interface that defines the behaviour of the TransactionStatus Service. 
    /// </summary>
    public interface ITransactionStatusService
    {
        Task<TransactionStatusResponseModel> CheckTransaction(TransactionStatusRequestModel requestModel);
    }
}
