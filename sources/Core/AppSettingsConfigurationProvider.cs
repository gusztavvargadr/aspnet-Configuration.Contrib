using System;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

namespace GV.AspNet.Configuration.ConfigurationManager
{
	public class AppSettingsConfigurationProvider : ConfigurationProvider
	{
		private static readonly string ConfigurationKeyPrefix = string.Concat("AppSettings", Constants.KeyDelimiter);

		public AppSettingsConfigurationProvider(NameValueCollection appSettings, string keyDelimiter, string keyPrefix)
		{
			if (appSettings == null)
			{
				throw new ArgumentNullException(nameof(appSettings));
			}

			AppSettings = appSettings;
			KeyDelimiter = keyDelimiter;
		}

		private NameValueCollection AppSettings { get; }
		private string KeyDelimiter { get; }

		public override void Load()
		{
			foreach (var appSettingKey in AppSettings.AllKeys)
			{
				var configurationKey = GetConfigurationKey(appSettingKey);
				var value = GetValue(appSettingKey);
				Data[configurationKey] = value;
			}
		}

		private string GetConfigurationKey(string appSettingsKey)
		{
			var configurationKey = appSettingsKey;
			if (!string.IsNullOrEmpty(KeyDelimiter))
			{
				configurationKey = configurationKey.Replace(KeyDelimiter, Constants.KeyDelimiter);
			}
			configurationKey = string.Concat(ConfigurationKeyPrefix, configurationKey.Trim());
			return configurationKey;
		}

		private string GetValue(string appSettingsKey) => AppSettings[appSettingsKey];
	}
}