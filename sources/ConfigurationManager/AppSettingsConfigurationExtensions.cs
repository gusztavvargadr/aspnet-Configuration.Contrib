using System;
using System.Collections.Specialized;
using System.Configuration;
using GV.AspNet.Configuration.Contrib.ConfigurationManager;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration
{
	public static class AppSettingsConfigurationExtensions
	{
		private static readonly string DefaultAppSettingsKeyDelimiter = string.Empty;

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder) => configurationBuilder.AddAppSettings(DefaultAppSettingsKeyDelimiter);

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder, string appSettingsKeyDelimiter)
			=> configurationBuilder.AddAppSettings(appSettingsKeyDelimiter, new string[] { });

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder, string appSettingsKeyDelimiter, params string[] appSettingsSectionPrefixes)
			=> configurationBuilder.AddAppSettings(ConfigurationManager.AppSettings, appSettingsKeyDelimiter, appSettingsSectionPrefixes);

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder, System.Configuration.Configuration configuration)
			=> configurationBuilder.AddAppSettings(configuration, DefaultAppSettingsKeyDelimiter);

		public static IConfigurationBuilder AddAppSettings(
			this IConfigurationBuilder configurationBuilder,
			System.Configuration.Configuration configuration,
			string appSettingsKeyDelimiter) => configurationBuilder.AddAppSettings(configuration, appSettingsKeyDelimiter, new string[] { });

		public static IConfigurationBuilder AddAppSettings(
			this IConfigurationBuilder configurationBuilder,
			System.Configuration.Configuration configuration,
			string appSettingsKeyDelimiter,
			params string[] appSettingsSectionPrefixes)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			var appSettings = new NameValueCollection();
			foreach (KeyValueConfigurationElement appSetting in configuration.AppSettings.Settings)
			{
				appSettings.Add(appSetting.Key, appSetting.Value);
			}
			return configurationBuilder.AddAppSettings(appSettings, appSettingsKeyDelimiter, appSettingsSectionPrefixes);
		}

		public static IConfigurationBuilder AddAppSettings(
			this IConfigurationBuilder configurationBuilder,
			NameValueCollection appSettings,
			string appSettingsKeyDelimiter,
			params string[] appSettingsSectionPrefixes)
		{
			if (configurationBuilder == null)
			{
				throw new ArgumentNullException(nameof(configurationBuilder));
			}

			return configurationBuilder.Add(new AppSettingsConfigurationProvider(appSettings, appSettingsKeyDelimiter, appSettingsSectionPrefixes));
		}
	}
}