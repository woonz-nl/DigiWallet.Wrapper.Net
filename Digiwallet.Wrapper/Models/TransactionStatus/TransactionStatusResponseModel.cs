using System.Linq;

namespace Digiwallet.Wrapper.Models.TransactionStatus
{
    public sealed class TransactionStatusResponseModel
    {
        /// <summary>
        /// Status code as returned by the API. See Digiwallet site for further documentation. 
        /// </summary>
        public readonly string StatusCode;
        /// <summary>
        /// Status code as mapped to an Enum. Please use this for readability. 
        /// </summary>
        public readonly TransactionStatusResponseCodes Status;
        /// <summary>
        /// The raw response body as returned by the API. Holds a JSON error array in case of a failure, a return URL in case of succes. 
        /// Please use only for logging / development. 
        /// </summary>
        public readonly string ResponseBody;

        public TransactionStatusResponseModel(string response) {
            var splitResponse = response.Split('|');
            var responseStatus = splitResponse[0];
            var splitStatus = responseStatus.Split(' ');

            this.StatusCode = splitStatus[0];
            this.Status = this.ResponseFromString(splitStatus[0]);

            if (splitResponse.Length > 1)
            {
                this.ResponseBody = splitResponse[1];
            }
            else
            {
                this.ResponseBody = string.Join(" ", splitStatus.Skip(1));
            }
        }

        private TransactionStatusResponseCodes ResponseFromString(string responseCode)
        {
            switch (responseCode)
            {
                case "000000":
                    return TransactionStatusResponseCodes.Ok;
                case "DW_SE_0020":
                    return TransactionStatusResponseCodes.NotCompleted;
                case "DW_SE_0021":
                    return TransactionStatusResponseCodes.Cancelled;
                case "DW_SE_0022":
                    return TransactionStatusResponseCodes.Expired;
                case "DW_SE_0023":
                    return TransactionStatusResponseCodes.UnableToProces;
                case "DW_SE_0028":
                    return TransactionStatusResponseCodes.AlreadyChecked;
                case "DW_XE_0003":
                    return TransactionStatusResponseCodes.ValidationFailed;
                case "DW_IE_0002":
                    return TransactionStatusResponseCodes.MaxRetriesExceeded;
                case "DW_IE_0006":
                    return TransactionStatusResponseCodes.SystemBusy;
            }

            return TransactionStatusResponseCodes.UnknownError;
        }
    }
}
