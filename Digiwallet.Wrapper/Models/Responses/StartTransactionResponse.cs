using System;
using System.Linq;

namespace Digiwallet.Wrapper.Models.Responses
{
    /// <summary>
    /// This class models the API response that follows the starting of a transaction. 
    /// </summary>
    public sealed class StartTransactionResponse
    {
        /// <summary>
        /// Status code as returned by the API. See Digiwallet site for further documentation. 
        /// </summary>
        public readonly string StatusCode;
        /// <summary>
        /// Status code as mapped to an Enum. Please use this for readability. 
        /// </summary>
        public readonly StartTransactionResponseCodes Status;
        /// <summary>
        /// Transaction nr. Save this to a database for futurue refference. 
        /// </summary>
        public readonly int TransactionNr;
        /// <summary>
        /// If set, outbound URL for a transaction. Send the user here. 
        /// </summary>
        public readonly string OutboundUrl;
        /// <summary>
        /// The raw response body as returned by the API. Holds a JSON error array in case of a failure, a return URL in case of succes. 
        /// Please use only for logging / development. 
        /// </summary>
        public readonly string ResponseBody;

        public StartTransactionResponse(string response)
        {
            var splitResponse = response.Split('|');
            var responseStatus = splitResponse[0];
            var splitStatus = responseStatus.Split(' ');

            this.StatusCode = splitStatus[0];
            this.Status = this.ResponseFromString(splitStatus[0]);
            if (this.Status == StartTransactionResponseCodes.Started)
            {
                this.TransactionNr = Int32.Parse(splitStatus[1]);
            }
            else
            {
                this.ResponseBody = string.Join(" ", splitStatus.Skip(1));
            }
        }

        private StartTransactionResponseCodes ResponseFromString(string responseCode)
        {
            switch (responseCode)
            {
                case "000000":
                    return StartTransactionResponseCodes.Started;
                case "DW_XE_0003":
                    return StartTransactionResponseCodes.ValidationFailed;
                case "DW_IE_0002":
                    return StartTransactionResponseCodes.NoAcquirerResponse;
                case "DW_IE_0006":
                    return StartTransactionResponseCodes.SystemBusy;
            }

            return StartTransactionResponseCodes.UnkownError;
        }
    }
}
