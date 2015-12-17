using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace GV.AspNet.Configuration.Contrib.Octopus.Samples.Host
{
	internal class Program
	{
		private static void Main(string[] args) => PrintConfiguration(GetOctopusConfiguration());

		private static IConfiguration GetOctopusConfiguration()
		{
			var configurationBuilder = new ConfigurationBuilder();

			var octopusOptions = GetOctopusOptions();
			configurationBuilder.AddOctopus(octopusOptions, ".", "AWS", "Database", "Octopus");

			var configuration = configurationBuilder.Build();
			return configuration;
		}

		private static OctopusConfigurationProviderOptions GetOctopusOptions()
		{
			var configurationBuilder = new ConfigurationBuilder();

			configurationBuilder.AddAppSettings();
			var configuration = configurationBuilder.Build();

			var octopusOptions = configuration.Get<OctopusConfigurationProviderOptions>("AppSettings:Octopus");
			return octopusOptions;
		}

		private static void PrintConfiguration(IConfiguration configuration)
		{
			PrintConfigurationSections(configuration.GetChildren());
			PrintTypedSettings(configuration);
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
			var variables = configuration.Get<Variables>();
			Console.WriteLine(variables);
		}
	}
}