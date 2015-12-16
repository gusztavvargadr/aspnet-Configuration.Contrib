using System;
using GV.AspNet.Configuration.Contrib.Octopus;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration
{
	public static class OctopusConfigurationExtensions
	{
		private static readonly string DefaultAppSettingsKeyDelimiter = string.Empty;

		public static IConfigurationBuilder AddOctopus(this IConfigurationBuilder configuration, OctopusConfigurationProviderOptions options)
			=> configuration.AddOctopus(options, DefaultAppSettingsKeyDelimiter);

		public static IConfigurationBuilder AddOctopus(this IConfigurationBuilder configuration, OctopusConfigurationProviderOptions options, string appSettingsKeyDelimiter)
			=> configuration.AddOctopus(options, appSettingsKeyDelimiter, new string[] { });

		public static IConfigurationBuilder AddOctopus(
			this IConfigurationBuilder configuration,
			OctopusConfigurationProviderOptions options,
			string appSettingsKeyDelimiter,
			params string[] appSettingsSectionPrefixes)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			return configuration.Add(new OctopusConfigurationProvider(options, new OctopusClientVariableDictionaryProvider(), appSettingsKeyDelimiter, appSettingsSectionPrefixes));
		}
	}
}