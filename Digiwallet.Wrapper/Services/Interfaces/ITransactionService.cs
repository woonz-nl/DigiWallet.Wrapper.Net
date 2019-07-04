using Digiwallet.Wrapper.Models.Responses;
using System.Threading.Tasks;

namespace Digiwallet.Wrapper.Services
{
    public interface ITransactionService<T>
    {
        Task<StartTransactionResponse> StartTransaction(T transaction);
    }
}
