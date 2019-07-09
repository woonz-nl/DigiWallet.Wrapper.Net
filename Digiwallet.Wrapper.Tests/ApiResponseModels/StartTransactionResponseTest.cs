using Digiwallet.Wrapper.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digiwallet.Wrapper.Tests.ApiResponseModels
{
    [TestClass]
    public class StartTransactionResponseTest
    {
        [TestMethod]
        public void StartedResponse()
        {
            var input = "000000 183521787|https://pay.digiwallet.nl/test-transaction?transactionID=183521787&paymethod=IDE&hash=c74ed1eef8ddae675ad2d27d554e6bee491f7ddb2b349f48296983e7c484cd7b";
            var model = new StartTransactionResponse(input);

            Assert.AreEqual(StartTransactionResponseCodes.Started, model.Status, "Didn't find Started statuscode");
            Assert.AreEqual(183521787, model.TransactionNr, "Didn't parse transactionnr correctly");
            Assert.AreEqual("000000", model.StatusCode, "Didn't correctly map API statuscode to string");
            Assert.IsNull(model.ResponseBody, "Responsebody should not be set in success case.");
        }

        [TestMethod]
        public void ValidationFailedResponse()
        {
            var input = "DW_XE_0003 Validation failed, details: {\"outletID\":[\"This outlet does not exist.\"],\"amount\":[\"This outlet does not exist.\",\"Outlet missing, cannot validate.\",\"Pricing agreement not found, cannot validate.\"]}";
            var model = new StartTransactionResponse(input);

            Assert.AreEqual(StartTransactionResponseCodes.ValidationFailed, model.Status, "Didn't find ValidationFailed statuscode");
            Assert.AreEqual("DW_XE_0003", model.StatusCode, "Didn't correctly map API statuscode to string");
            Assert.IsNull(model.OutboundUrl, "Outbound URL should not be set in failure case.");
        }
    }
}
