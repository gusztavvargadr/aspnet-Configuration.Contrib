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
				var source = new AppSettingsConfigurationProvider(appSettings, appSettingsKeyDelimiter);

				source.Load();

				string configurationValue;
				Assert.True(source.TryGet(configurationKey, out configurationValue));
				Assert.Equal(value, configurationValue);
			}

			[Theory]
			[InlineData("Parent.Key", "", "AppSettings:Parent.Key", "Value")]
			[InlineData("Parent.Key", ".", "AppSettings:Parent:Key", "Value")]
			public void ReplacesKeyDelimiter(string appSettingsKey, string appSettingsKeyDelimiter, string configurationKey, string value)
			{
				var appSettings = new NameValueCollection { { appSettingsKey, value } };
				var source = new AppSettingsConfigurationProvider(appSettings, appSettingsKeyDelimiter);

				source.Load();

				string configurationValue;
				Assert.True(source.TryGet(configurationKey, out configurationValue));
				Assert.Equal(value, configurationValue);
			}

			[Theory]
			[InlineData("ParentKey", "", "Parent", "AppSettings:Parent:Key", "Value")]
			public void ReplacesSectionPrefix(string appSettingsKey, string appSettingsKeyDelimiter, string appSettingsSectionPrefix, string configurationKey, string value)
			{
				var appSettings = new NameValueCollection { { appSettingsKey, value } };
				var source = new AppSettingsConfigurationProvider(appSettings, appSettingsKeyDelimiter, appSettingsSectionPrefix);

				source.Load();

				string configurationValue;
				Assert.True(source.TryGet(configurationKey, out configurationValue));
				Assert.Equal(value, configurationValue);
			}
		}
	}
}