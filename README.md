# DigiWallet.Wrapper.Net
.Net Wrapper for DigiWallet API

## Setup
To use this library you need to add it's required services to your Startup.cs

To make this easier, the project includes an extension method at `Digiwallet.Wrapper.Extensions`. Use it like so; 
```C#
public void ConfigureServices(IServiceCollection services)
{
	services.AddDigiWalletServices()
	...
}
```

## Configuration
The Digiwallet.Wrapper.Cmd project includes a appsettings.json.example file which holds the API key. 
For now, this API key isn't used. 

## Starting a transaction
In order to start a transaction you need to add services you wish to use to your startup.cs.
```C#
public void ConfigureServices(IServiceCollection services)
{
	services.AddTransient<IIDealTransactionService, IDealTransactionService>()
	...
}
```
The service allows a user to start a transaction like so; 
```C#
var result = await iDealTransactionService.StartTransaction(new Models.Transaction.IDealTransaction()
{
    ShopID = 12345,
    Amount = 2000,
    Bank = "ABNAL2A",
    Description = "Testing 1. 2.",
    CancelUrl = "http://example.com/DigiWallet/cancel",
    ReturnUrl = "http://example.comnl/DigiWallet/return",
    ReportUrl = "http://example.com/DigiWallet/report"
});
```
The function returns a [StartTransactionResponse](/Digiwallet.Wrapper/Models/Responses/StartTransactionResponse.cs) model. See it's documentation for usage. 

## Transaction status
In order to check the transaction status you need to add the status service your startup.cs.
```C#
public void ConfigureServices(IServiceCollection services)
{
	services.AddTransient<ITransactionStatusService, TransactionStatusService>()
	...
}
```
To check on a transaction you need a few details;
```C#
var checkModel = new TransactionStatusRequestModel() {
    ApiEndpoint = "ideal/check",
    ShopID = 12345,
    TransactionID = 6789,
    RestrictResponseCount = true, 
    TestMode = true
};
```
The API endpoint is documented in the Digiwallet API documentation but will be part of the code in the future. Your codebase needs to provide the Shop ID and Transaction ID (returned as part of `StartTransactionResponse`).

`RestrictResponseCount` restricts the amound of times the API will return a success code to one. This can be used if your code does something on success status. 

`TestMode` sets the API to testmode. You can also define an entire outlet (ShopID) as test outlet. 

Call for a transaction status using; 

```C#
var currentStatus = await transactionStatusService.CheckTransaction(checkModel);
```

This returns a [TransacionStatusResponseModel](/Digiwallet.Wrapper/Models/TransactionStatus/TransactionStatusResponseModel.cs). See it's documentation for further use. 
