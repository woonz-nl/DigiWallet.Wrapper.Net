using Digiwallet.Wrapper.Models.TransactionStatus;
using System.Threading.Tasks;

namespace Digiwallet.Wrapper.Services.Interfaces
{
    public interface ITransactionStatusService
    {
        Task<TransactionStatusResponseModel> CheckTransaction(TransactionStatusRequestModel requestModel);
    }
}
