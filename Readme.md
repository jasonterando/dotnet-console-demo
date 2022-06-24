# Sample Console Application

This demonstration project stands up a console app that implements [IHostBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihostbuilder?view=dotnet-plat-ext-6.0) to implement dependency injection.  It also demonstrates usage of the [CommandLineParser](https://github.com/commandlineparser/commandline) library for handling command line argument parsing.

> In this demonstration project, IHostBuilder is overkill for what the functionality is.  However, IHostBuilder can be useful if you are using code that is implemented via [service extensions](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions?view=dotnet-plat-ext-6.0) or if you are building a larger application in which dependency is required to make the code more readable/testable/etc.

## License / Usage Rights

Use and distribute freely with our without attribution.  No warranty or claims made as to functionality, bugs, fitness to purpose or any other intent.

## Design Objectives

* [Flexible command line argument processing](https://github.com/commandlineparser/commandline)
* [Dependency injection](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihostbuilder?view=dotnet-plat-ext-6.0)
* [Ability to output to Console and ILogger independently](https://serilog.net/)

## Requirements

* [Microsoft .NET 6 Core SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Demonstrated Functionality

This command line utilities performs two functions:

* Calculation (default):  Evaluate the passed parameters and return the results.
* Statistics: Accepts a list of numbers and returns statistic information

By default, the return format is "plain text".  You can optionally return data in JSON format by specifying "-f json"

### Examples:

*Executing via **dotnet** from project directory:*

* `dotnet run -- 100 + 200 / 2` (Returns **200**)
* `dotnet run -- "(100 + 200) / 2"` (Returns **150**, you'll need the quotes for parenthesis and asterisks)
* `dotnet run -- calc 500 + 2` (Returns **502**, the "calc" is unnecessary but shown for completeness)
* `dotnet run -- -f json 100/2` (Returns **{Value":50}**)
* `dotnet run -- stats 100 200 300 400 500` (Returns **Count=5, Mean=300, Variance=25000, StandardDeviation=158.11388300841898, Skewness=0, Kurtosis=-1.2000000000000002, Maximum=500, Minimum=100**)
* `dotnet run -- stats 100 200 300 400 500` (Returns **Count=5, Mean=300, Variance=25000, StandardDeviation=158.11388300841898, Skewness=0, Kurtosis=-1.2000000000000002, Maximum=500, Minimum=100**)
* `dotnet run -- stats -f json 100 200 300 400 500`: (Returns **{"Count":5,"Mean":300,"Variance":25000,"StandardDeviation":158.11388300841898,"Skewness":0,"Kurtosis":-1.2000000000000002,"Maximum":500,"Minimum":100}**)

## Basic Recipe

1. In IHostBuilder.ConfigureServices, define injections for any global services and call the default command argument parser's ParseArguments
2. For parsed options, define verb-specific services, including implementing ITaskFactory (which either implements what you want to do or throws an exception)
3. Returns zero on happy path or otherwise -1

> This example executes some functionality and exits.  Therefore, it does not implement [IHostLifetime](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.ihostlifetime?view=dotnet-plat-ext-6.0).  If you are writing a service/daemon that is going to run persistently, you will want to implement [IHostLifetime](https://andrewlock.net/introducing-ihostlifetime-and-untangling-the-generic-host-startup-interactions/)