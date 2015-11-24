using System;
using System.Collections.Specialized;
using Microsoft.Framework.Configuration;

namespace GV.AspNet.Configuration.ConfigurationManager
{
	public class AppSettingsConfigurationSource : ConfigurationSource
	{
		public AppSettingsConfigurationSource(NameValueCollection appSettings, string keyDelimiter, string keyPrefix)
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
				var key = GetKey(appSettingKey);
				var value = GetValue(appSettingKey);
				Data[key] = value;
			}
		}

		private string GetKey(string appSettingsKey)
		{
			var key = appSettingsKey;
			if (!string.IsNullOrEmpty(KeyDelimiter))
			{
				key = key.Replace(KeyDelimiter, Constants.KeyDelimiter);
			}
			return key.Trim();
		}

		private string GetValue(string appSettingsKey) => AppSettings[appSettingsKey];
	}
}