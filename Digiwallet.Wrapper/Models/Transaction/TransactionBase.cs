
namespace Digiwallet.Wrapper.Models.Transaction
{
    public abstract class TransactionBase
    {
        #region Generic API params
        public int Version { get; } = 4;
        public int ShopID { get; set; }
        public bool Test { get; set; }
        #endregion

        #region Payment method definition
        /// <summary>
        /// The name of the payment method (IE Ideal, creditcard etc) 
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// The internal three letter name of the payment method
        /// </summary>
        public abstract string Method { get; }
        /// <summary>
        /// Minimum amount of this method in cents
        /// </summary>
        public virtual int MinimumAmount => 84;
        /// <summary>
        /// Maximum amount of this method in cents
        /// </summary>
        public virtual int MaximumAmount => 1000000;
        /// <summary>
        /// Currencies available for this payment method. 
        /// </summary>
        public virtual string[] Currencies => new[] { "EUR" };
        /// <summary>
        /// Languages available for payment method. 
        /// </summary>
        public virtual string[] Languages => new[] { "nl" };
        #endregion

        #region Payment details
        private int? _amount = null;
        /// <summary>
        /// Amount of the transaction in cents
        /// </summary>
        public int Amount
        {
            get
            {
                // TODO: Maybe raise warning / exception
                return _amount ?? 0;
            }
            set {
                if (value < this.MinimumAmount) {
                    // Throw exception
                }
                if (value > this.MaximumAmount)
                {
                    // Throw exception
                }
                this._amount = value;
            }
        }
        /// <summary>
        /// Description of what's being paid for. 
        /// TODO: Define max length. 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Currency used for transaction. 
        /// </summary>
        public string Currency { get; set; } = "EUR";
        /// <summary>
        /// Language used in transaction screens for DigiWallet API
        /// </summary>
        public string Language { get; set; } = "nl";
        #endregion

        #region Endpoint definitions
        /// <summary>
        /// The API method to start a transaction
        /// </summary>
        public abstract string StartApi { get; }
        /// <summary>
        /// The API method to check transaction status
        /// </summary>
        public abstract string CheckApi { get; }
        /// <summary>
        /// URL location where to return to after processing the transaction
        /// </summary>
        public abstract string ReturnUrl { get; set; }
        /// <summary>
        /// URL location where to return to after cancelling the transaction
        /// </summary>
        public abstract string CancelUrl { get; set; }
        /// <summary>
        /// URL location where to send server-to-server callbacks about the transaction statuses
        /// </summary>
        public abstract string ReportUrl { get; set; }
        #endregion
    }
}
