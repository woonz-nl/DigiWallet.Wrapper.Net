namespace Digiwallet.Wrapper.Models.TransactionStatus
{
    public sealed class TransactionStatusRequestModel
    {
        /// <summary>
        /// Check API endpoint. See <see cref="Models.Transaction.TransactionBase.CheckApi"/> implementations. 
        /// </summary>
        public string ApiEndpoint { get; set; }
        /// <summary>
        /// rtlo, outlet ID. 
        /// </summary>
        public int ShopID { get; set; }
        /// <summary>
        /// Transaction ID (<see cref="TransactionBase.TransactionId"/>)
        /// </summary>
        public int TransactionID { get; set; }
        /// <summary>
        /// Wether the API should be in test-mode. Should be the same value as when you started the transaction, otherwise the transaction won't be found. 
        /// If your entire outlet is set to testmode, this will always be set to true by the API
        /// </summary>
        public bool TestMode { get; set; }
        /// <summary>
        /// Restricts the API to only reply with a Transaction OK state once. If you call the API again, it'll responde with 'DW_SE_0028 Transaction already checked'.
        /// Useful to prevent race conditions where you may send the client data/products on the transaction OK event. 
        /// </summary>
        public bool RestrictResponseCount { get; set; }
    }
}
