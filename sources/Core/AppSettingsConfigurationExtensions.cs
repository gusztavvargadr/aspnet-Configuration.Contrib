using System;
using System.Collections.Specialized;
using System.Configuration;
using GV.AspNet.Configuration.ConfigurationManager;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration
{
	public static class AppSettingsConfigurationExtensions
	{
		private static readonly string DefaultKeyDelimiter = Constants.KeyDelimiter;
		private static readonly string DefaultKeyPrefix = string.Empty;

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder) => configurationBuilder.AddAppSettings(ConfigurationManager.AppSettings);

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder, NameValueCollection appSettings)
			=> configurationBuilder.AddAppSettings(appSettings, DefaultKeyDelimiter);

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder, NameValueCollection appSettings, string keyDelimiter)
			=> configurationBuilder.AddAppSettings(appSettings, keyDelimiter, DefaultKeyPrefix);

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder, NameValueCollection appSettings, string keyDelimiter, string keyPrefix)
		{
			if (configurationBuilder == null)
			{
				throw new ArgumentNullException(nameof(configurationBuilder));
			}

			return configurationBuilder.Add(new AppSettingsConfigurationProvider(appSettings, keyDelimiter, keyPrefix));
		}

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder, System.Configuration.Configuration configuration)
			=> configurationBuilder.AddAppSettings(configuration, DefaultKeyDelimiter);

		public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configurationBuilder, System.Configuration.Configuration configuration, string keyDelimiter)
			=> configurationBuilder.AddAppSettings(configuration, keyDelimiter, DefaultKeyPrefix);

		public static IConfigurationBuilder AddAppSettings(
			this IConfigurationBuilder configurationBuilder,
			System.Configuration.Configuration configuration,
			string keyDelimiter,
			string keyPrefix)
		{
			var appSettings = new NameValueCollection();
			foreach (KeyValueConfigurationElement appSetting in configuration.AppSettings.Settings)
			{
				appSettings.Add(appSetting.Key, appSetting.Value);
			}
			return configurationBuilder.AddAppSettings(appSettings, keyDelimiter, keyPrefix);
		}
	}
}