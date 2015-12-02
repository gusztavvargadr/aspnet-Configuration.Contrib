using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace GV.AspNet.Configuration.ConfigurationManager.Samples.Host
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			PrintConfiguration(GetDefaultConfiguration());
			PrintConfiguration(GetExeConfiguration());
		}

		private static IConfiguration GetDefaultConfiguration()
		{
			var configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddAppSettings(".", "Parent3");
			configurationBuilder.AddConnectionStrings();
			var configuration = configurationBuilder.Build();
			return configuration;
		}

		private static IConfiguration GetExeConfiguration()
		{
			var configurationBuilder = new ConfigurationBuilder();
			var exeConfiguration =
				System.Configuration.ConfigurationManager.OpenExeConfiguration(
					Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GV.AspNet.Configuration.ConfigurationManager.Samples.Host.exe"));
			configurationBuilder.AddAppSettings(exeConfiguration, ".", "Parent3");
			configurationBuilder.AddConnectionStrings(exeConfiguration);
			var configuration = configurationBuilder.Build();
			return configuration;
		}

		private static void PrintConfiguration(IConfiguration configuration)
		{
			PrintConfigurationSections(configuration.GetChildren());
			PrintTypedSettings(configuration);
			PrintConnectionStrings(configuration);
		}

		private static void PrintConfigurationSections(IEnumerable<IConfigurationSection> parents)
		{
			foreach (var parent in parents)
			{
				PrintConfigurationSection("root", parent);
			}
		}

		private static void PrintConfigurationSection(string root, IConfigurationSection parent)
		{
			Console.WriteLine($"{root}:{parent.Key} - {parent.Value}");
			foreach (var child in parent.GetChildren())
			{
				PrintConfigurationSection(string.Concat(root, Constants.KeyDelimiter, parent.Key), child);
			}
		}

		private static void PrintTypedSettings(IConfiguration configuration)
		{
			var appSettings = configuration.Get<AppSettings>("AppSettings");
			Console.WriteLine(appSettings);
		}

		private static void PrintConnectionStrings(IConfiguration configuration)
		{
			var connectionString = configuration["Data:DefaultConnection:ConnectionString"];
			var providerName = configuration["Data:DefaultConnection:ProviderName"];
			Console.WriteLine($"{connectionString} - {providerName}");
		}
	}
}