using Digiwallet.Wrapper.Models.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Digiwallet.Wrapper.Tests.ApiRequestModels
{
    [TestClass]
    public class IDealTransactionTest
    {
        /// <summary>
        /// Due to SEPA transaction description limitations, one cannot use any characters other than alphabetic and numeric. 
        /// This test is very well suited to be converted to a xUnit Theory instead, with multiple inputs / outputs. 
        /// </summary>
        [TestMethod]
        public void InvalidInput_Filter_ToValidString()
        {
            // Arrange
            const string inputString = "You# (@Sho))(uld b(*!@#e ab__~++~_le )(2!(*$#)(*)) r@@@@@@@@ea@@@@@d ______th_____is______ 1*&)(&2)(*()$@!&(*3)*($)!@*(@";
            const string expectedOutput = "You Should be able 2 read this 1";

            // Act (calls setter)
            IDealTransaction iDealTransaction = new IDealTransaction()
            {
                Description = inputString
            };

            // Assert
            Assert.AreEqual(expectedOutput, iDealTransaction.Description, "Didn't filter input correctly.");
            Assert.AreEqual(32, iDealTransaction.Description.Length, "Failed to limit charcount to 32.");
        }

        [TestMethod]
        public void CharFilter_SetInvalidDescription_Exception()
        {
            // Arrange 
            IDealTransaction iDealTransaction = new IDealTransaction();
            int lowerLimit = iDealTransaction.MinimumAmount;
            int upperLimit = iDealTransaction.MaximumAmount;

            // Act 
            Action setTooHigh = () => iDealTransaction.Amount = upperLimit + 1;
            Action setTooLow = () => iDealTransaction.Amount = lowerLimit - 1;

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(setTooHigh, "Transaction model shouldn't accept value above upper limit");
            Assert.ThrowsException<ArgumentOutOfRangeException>(setTooLow, "Transaction model shouldn't accept value below lower limit");
        }
    }
}
