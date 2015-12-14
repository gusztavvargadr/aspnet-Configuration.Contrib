# aspnet-Configuration.Contrib

**Contents** [Overview][Overview] | [Getting started][GettingStarted] | [Usage][Usage] | [Resources][Resources]

[![Build][BuildBadge]][Build] [![NuGet][NuGetBadge]][NuGet]

This repository contains additional providers for the [ASP.NET 5][aspnet] [configuration system][Configuration].

[System.Configuration.ConfigurationManager][SystemConfigurationConfigurationManager]

## Overview

In case you like the direction of the new [ASP.NET 5][aspnet] [configuration system][Configuration], and you want to try it for .NET 4.5(.1) too, but first without touching your existing configuration files, these components might be of help. Independently of how weird it is to lock an advanced abstraction to its legacy roots, by using these providers you can easily reuse your current `app.config` or `Web.config` files to wire up the application settings and connection strings in the new system.

## Getting started

Just reference the [NuGet package][NuGet] in your project, which works with the configuration values, to get started. This will add the dependencies to the required core [Configuration][Configuration] packages too.

## Usage

Import the standard `Microsoft.Extensions.Configuration` namespace and add your existing settings:

```csharp
var configurationBuilder = new ConfigurationBuilder();
configurationBuilder.AddAppSettings();
var configuration = configurationBuilder.Build();
```

Given that you have an application setting like:

```xml
<appSettings>
  <add key="Key" value="Value" />
</appSettings>
```

You can get the actual value as:

```csharp
var value = configuration["AppSettings:Key"];
```

One of the real strengths of the new framework is having a [configuration binder][ConfigurationBinder] supporting typed mapping even to complex hierarchies. Given the sample settings of:

```xml
<appSettings>
  <add key="Host" value="http://localhost" />
  <add key="Connection:TimesOutIn" value="00:01:00" />
  <add key="Connection:RetryCount" value="5" />
</appSettings>
```

And the target like:

```csharp
public class AppSettings
{
  public Uri Host { get; set; }
  public ConnectionSettings Connection { get; set; }
}

public class ConnectionSettings
{
  public TimeSpan TimesOutIn { get; set; }
  public int RetryCount { get; set; }
}
```

Reading the values is as simples as:

```csharp
var appSettings = configuration.Get<AppSettings>("AppSettings");
```

In most cases, the legacy configuration keys are not set up with the required hierarchy delimiter of `:`. To support getting these values easier, in a configuration like:

```xml
<appSettings>
  <add key="Host" value="http://localhost" />
  <add key="Connection.TimesoutIn" value="00:01:00" />
  <add key="ConnectionRetryCount" value="5" />
</appSettings>
```

You can specify the custom separators and prefixes to use when loading the values:

```csharp
configurationBuilder.AddAppSettings(".", "Connection");
```

You can now add your existing connection strings too:

```csharp
var configurationBuilder = new ConfigurationBuilder();
configurationBuilder.AddConnectionStrings();
var configuration = configurationBuilder.Build();
```

Having the usual setup like:

```xml
<connectionStrings>
  <add name="DefaultConnection" connectionString="data source=.\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true" providerName="System.Data.SqlClient" />
</connectionStrings>
```

Reading the connection information is simply:

```csharp
var connectionString = configuration["Data:DefaultConnection:ConnectionString"];
var providerName = configuration["Data:DefaultConnection:ProviderName"];
```

Besides the default application configuration, you can load your settings from any custom file too:

```csharp
var configurationBuilder = new ConfigurationBuilder();
configurationBuilder.AddAppSettings(
  System.Configuration.ConfigurationManager.OpenExeConfiguration(
    "<configuration file path>"));
var configuration = configurationBuilder.Build();
```

This version, of course, also supports the custom key delimiters and prefixes for application settings and the connection strings setup, too.

If you like the capabilities of the new configuration subsystem, don't forget to actually migrate your settings to the new, more flexible providers, supporting, for example, working with JSON or loading multiple layers of configuration providers. These libraries are intended to be used only as the very first step of that process.

See the [samples][Samples] project for a complete example of all the various options and the [official documentation][Documentation] for more usage scenarios.

## Resources

* [Samples][Samples]
* [Official Documentation][Documentation]

[Overview]: #overview
[GettingStarted]: #getting-started
[Usage]: #usage
[Resources]: #resources
[BuildBadge]: https://img.shields.io/appveyor/ci/gusztavvargadr/aspnet-configuration-contrib.svg
[Build]: https://ci.appveyor.com/project/gusztavvargadr/aspnet-configuration-contrib
[NuGetBadge]: https://img.shields.io/nuget/v/Microsoft.Extensions.Configuration.Contrib.GV.ConfigurationManager.svg
[NuGet]: https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Contrib.GV.ConfigurationManager
[SystemConfigurationConfigurationManager]: https://msdn.microsoft.com/en-us/library/system.configuration.configurationmanager(v=vs.110).aspx
[aspnet]: https://github.com/aspnet
[Configuration]: https://github.com/aspnet/Configuration
[ConfigurationBinder]: https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Binder
[Samples]: samples/Samples.Host
[Documentation]: https://docs.asp.net/en/latest/fundamentals/configuration.html
