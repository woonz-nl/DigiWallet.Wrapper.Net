using Digiwallet.Wrapper.Models.Responses;
using Digiwallet.Wrapper.Models.Transaction;
using Digiwallet.Wrapper.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Digiwallet.Wrapper.Tests.TransactionServices
{
    [TestClass]
    public class IDealTransactionServiceTest
    {
        [TestMethod]
        public async Task StartTransactionAsync()
        {
            // Arrange
            var clientHandlerStub = new DelegatingHandlerStub();
            var client = new HttpClient(clientHandlerStub)
            {
                BaseAddress = new System.Uri("https://transaction.digiwallet.nl/")
            };
            var mockFactory = new Mock<IHttpClientFactory>();
            // Always return our mock client
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            IHttpClientFactory mockHttpFactory = mockFactory.Object;

            IDealTransactionService iDealTransactionservice = new IDealTransactionService(mockHttpFactory, Mock.Of<ILogger<IDealTransactionService>>());
            IDealTransaction transaction = new IDealTransaction()
            {
                ShopID = 149631,
                Amount = 2000,
                Bank = "ABNAL2A",
                Description = "Testing 1. 2.",
                CancelUrl = "http://development.woonz.nl/DigiWallet/cancel",
                ReturnUrl = "http://development.woonz.nl/DigiWallet/return",
                ReportUrl = "http://development.woonz.nl/DigiWallet/report"
            };

            // Act
            StartTransactionResponse startModel = await iDealTransactionservice.StartTransaction(transaction);

            // Assert
            Assert.AreEqual(startModel.TransactionNr, 103084, "Didn't parse transaction ID correctly");
            Assert.AreEqual(startModel.StatusCode, "000000", "Didn't parse statuscode correctly");
            Assert.AreEqual(startModel.Status, StartTransactionResponseCodes.Started, "Didn't interpret success status correctly");
            Assert.AreEqual(startModel.OutboundUrl, "https://pay.digiwallet.nl/consumer/ideal/launch/103084/da85a5e0-b29e-11e8-9332-ecf4cbbfde30/0", "Didn't read outbound URL correctly");
            Assert.IsNull(startModel.ResponseBody, "Set responsebody when it shouldn't have been set");
        }
    }

    public class DelegatingHandlerStub : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
        public DelegatingHandlerStub()
        {
            _handlerFunc = (request, cancellationToken) => 
            {
                var response = request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent("000000 103084|https://pay.digiwallet.nl/consumer/ideal/launch/103084/da85a5e0-b29e-11e8-9332-ecf4cbbfde30/0");
                return Task.FromResult(response);
            };
        }

        public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }
    }
}
