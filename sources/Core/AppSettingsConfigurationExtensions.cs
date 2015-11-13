using System;
using System.Configuration;
using GV.AspNet.Configuration.ConfigurationManager;

// ReSharper disable once CheckNamespace
namespace Microsoft.Framework.Configuration
{
	public static class AppSettingsConfigurationExtensions
	{
		private const string AppSettingsSectionName = "appSettings";
		private const string DefaultKeyDelimiter = ".";

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configuration)
			=> configuration.AddAppSettings(ConfigurationManager.GetSection(AppSettingsSectionName) as AppSettingsSection);

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configuration, AppSettingsSection appSettingsSection)
			=> configuration.AddAppSettings(appSettingsSection, DefaultKeyDelimiter);

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configuration, AppSettingsSection appSettingsSection, string keyDelimiter)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			return configuration.Add(new AppSettingsConfigurationSource(appSettingsSection, keyDelimiter));
		}
	}
}