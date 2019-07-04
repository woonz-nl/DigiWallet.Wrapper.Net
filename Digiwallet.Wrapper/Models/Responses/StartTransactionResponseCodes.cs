
namespace Digiwallet.Wrapper.Models.Responses
{
    /// <summary>
    /// Enum of possible responses from Digiwallet start transaction API's. 
    /// </summary>
    public enum StartTransactionResponseCodes
    {
        /// <summary>
        /// Transaction has succesfully been started. See <see cref="Models.Responses.StartTransactionResponse.OutboundUrl"/> for the outbound URL
        /// </summary>
        Started,
        /// <summary>
        /// Validation of one or multiple fields has failed. See <see cref="Models.Responses.StartTransactionResponse.ResponseBody"/> for a JSON encoded array of errors.
        /// </summary>
        ValidationFailed,
        /// <summary>
        /// The acquiring system has failed to respond within the set amount of tries. Start a new transaction on a later moment.
        /// </summary>
        NoAcquirerResponse, 
        /// <summary>
        /// The DigiWallet system is too busy handling requests. Please try again later. 
        /// </summary>
        SystemBusy, 
        /// <summary>
        /// The DigiWallet service has returned an unknown error. Please contact support.
        /// </summary>
        UnkownError
    }
}
