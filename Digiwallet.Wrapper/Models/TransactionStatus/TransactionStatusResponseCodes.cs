namespace Digiwallet.Wrapper.Models.TransactionStatus
{
    public enum TransactionStatusResponseCodes
    {
        /// <summary>
        /// The client successfully paid for the transaction
        /// </summary>
        Ok,
        /// <summary>
        /// There's no definitive transaction state yet. Try again later.
        /// </summary>
        NotCompleted, 
        /// <summary>
        /// The transaction has been cancelled
        /// </summary>
        Cancelled, 
        /// <summary>
        /// The client took too long to finalise the request (10 minutes).
        /// </summary>
        Expired, 
        /// <summary>
        /// The transaction could not be processed
        /// </summary>
        UnableToProces,
        /// <summary>
        /// The transaction has already been checked before <see cref="TransactionStatusRequestModel.RestrictResponseCount"/>
        /// </summary>
        AlreadyChecked,
        /// <summary>
        /// The validation of one or multiple fields failed. See the resonse body <see cref="TransactionStatusResponseModel.ResponseBody"/> for details (json encoded array).
        /// </summary>
        ValidationFailed, 
        /// <summary>
        /// The aqcuirer system didn't respond in time. Please try again later. 
        /// </summary>
        MaxRetriesExceeded,
        /// <summary>
        /// The Digiwallet system is busy. Try again later. 
        /// </summary>
        SystemBusy, 
        /// <summary>
        /// An unknown error occured. Please contact Digiwallet support. 
        /// </summary>
        UnknownError
    }
}
