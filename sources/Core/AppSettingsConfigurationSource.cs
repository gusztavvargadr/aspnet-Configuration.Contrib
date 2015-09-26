using System;
using System.Configuration;

namespace Microsoft.Framework.Configuration.Contrib.GV.AppSettings
{
	public class AppSettingsConfigurationSource : ConfigurationSource
	{
		public AppSettingsConfigurationSource(AppSettingsSection appSettingsSection, string keyDelimiter)
		{
			if (appSettingsSection == null)
			{
				throw new ArgumentNullException(nameof(appSettingsSection));
			}

			AppSettingsSection = appSettingsSection;
			KeyDelimiter = keyDelimiter;
		}

		private AppSettingsSection AppSettingsSection { get; }
		private string KeyDelimiter { get; }

		public override void Load()
		{
			foreach (var appSettingKey in AppSettingsSection.Settings.AllKeys)
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

		private string GetValue(string appSettingsKey) => AppSettingsSection.Settings[appSettingsKey].Value;
	}
}