using System;
using System.Collections.Specialized;
using Xunit;

namespace GV.AspNet.Configuration.ConfigurationManager.UnitTests
{
	public class AppSettingsConfigurationProviderTests
	{
		public class Load
		{
			[Theory]
			[InlineData("", "AppSettings:", "Value")]
			[InlineData("Key", "AppSettings:Key", "Value")]
			public void AddsAppSettings(string appSettingsKey, string configurationKey, string value)
			{
				var appSettings = new NameValueCollection { { appSettingsKey, value } };
				var appSettingsKeyDelimiter = string.Empty;
				var provider = new AppSettingsConfigurationProvider(appSettings, appSettingsKeyDelimiter);

				provider.Load();

				string configurationValue;
				Assert.True(provider.TryGet(configurationKey, out configurationValue));
				Assert.Equal(value, configurationValue);
			}

			[Theory]
			[InlineData("Parent.Key", "", "AppSettings:Parent.Key", "Value")]
			[InlineData("Parent.Key", ".", "AppSettings:Parent:Key", "Value")]
			public void ReplacesKeyDelimiter(string appSettingsKey, string appSettingsKeyDelimiter, string configurationKey, string value)
			{
				var appSettings = new NameValueCollection { { appSettingsKey, value } };
				var provider = new AppSettingsConfigurationProvider(appSettings, appSettingsKeyDelimiter);

				provider.Load();

				string configurationValue;
				Assert.True(provider.TryGet(configurationKey, out configurationValue));
				Assert.Equal(value, configurationValue);
			}

			[Theory]
			[InlineData("ParentKey", "", "Parent", "AppSettings:Parent:Key", "Value")]
			public void ReplacesSectionPrefix(string appSettingsKey, string appSettingsKeyDelimiter, string appSettingsSectionPrefix, string configurationKey, string value)
			{
				var appSettings = new NameValueCollection { { appSettingsKey, value } };
				var provider = new AppSettingsConfigurationProvider(appSettings, appSettingsKeyDelimiter, appSettingsSectionPrefix);

				provider.Load();

				string configurationValue;
				Assert.True(provider.TryGet(configurationKey, out configurationValue));
				Assert.Equal(value, configurationValue);
			}
		}
	}
}