
namespace Digiwallet.Wrapper.Models.Transaction
{
    /// <summary>
    /// Model that holds all details required to start a DigiWallet IDeal transaction.
    /// </summary>
    public sealed class IDealTransaction : TransactionBase
    {
        #region Provider properties
        public override string Name { get => "iDeal"; }
        public override string Method { get => "IDE"; }
        public override string StartApi { get => "ideal/start"; }
        public override string CheckApi { get => "ideal/check"; }
        #endregion

        /// <summary>
        /// The bank ID as in <see cref="Models.iDeal.IDealIssuerModel.Id"/>
        /// </summary>
        public string Bank { get; set; }
        public override string ReturnUrl { get; set; }
        public override string CancelUrl { get; set; }
        public override string ReportUrl { get; set; }
    }
}
