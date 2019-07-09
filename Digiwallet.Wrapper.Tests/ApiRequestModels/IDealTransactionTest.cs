using Digiwallet.Wrapper.Models.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digiwallet.Wrapper.Tests.ApiRequestModels
{
    [TestClass]
    public class IDealTransactionTest
    {
        [TestMethod]
        public void DescriptionLimitationsTest()
        {
            var inputString = "You# (@Sho))(uld b(*!@#e ab__~++~_le )(2!(*$#)(*)) r@@@@@@@@ea@@@@@d ______th_____is______ 1*&)(&2)(*()$@!&(*3)*($)!@*(@";

            var iDealTransaction = new IDealTransaction() {
                Description = inputString
            };

            Assert.AreEqual("You Should be able 2 read this 1", iDealTransaction.Description, "Didn't filter input correctly.");
            Assert.AreEqual(32, iDealTransaction.Description.Length, "Failed to limit charcount to 32.");
        }
    }
}
