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
    }
}
