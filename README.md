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

Todo: Further documentation on usage. 

