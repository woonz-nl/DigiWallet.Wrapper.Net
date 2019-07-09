using Digiwallet.Wrapper.Models.TransactionStatus;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Digiwallet.Wrapper.Tests.ApiResponseModels
{
    [TestClass]
    public class TransactionStatusResponseModelTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void OkResponse()
        {
            var input = "000000 OK";
            var model = new TransactionStatusResponseModel(input);

            Assert.AreEqual(TransactionStatusResponseCodes.Ok, model.Status, "Didn't find Ok statuscode");
            Assert.AreEqual("000000", model.StatusCode, "Didn't correctly map API statuscode to string");
            Assert.AreEqual("OK", model.ResponseBody, "Didn't parse API response text correctly.");
        }

        [TestMethod]
        public void NotCompletedResponse()
        {
            var input = "DW_SE_0020 Transaction has not been completed, try again later";
            var model = new TransactionStatusResponseModel(input);

            Assert.AreEqual(TransactionStatusResponseCodes.NotCompleted, model.Status, "Didn't find NotCompleted statuscode");
            Assert.AreEqual("DW_SE_0020", model.StatusCode, "Didn't correctly map API statuscode to string");
            Assert.AreEqual("Transaction has not been completed, try again later", model.ResponseBody, "Didn't parse API response text correctly.");
        }

        [TestMethod]
        public void ValidationFailedResponse()
        {
            var input = "DW_XE_0003 Validation failed, details: {\"outletID\":[\"Outlets above 62239 cannot be even.\"]}";
            var model = new TransactionStatusResponseModel(input);

            Assert.AreEqual(TransactionStatusResponseCodes.ValidationFailed, model.Status, "Didn't find ValidationFailed statuscode");
            Assert.AreEqual("DW_XE_0003", model.StatusCode, "Didn't correctly map API statuscode to string");
        }
    }
}
