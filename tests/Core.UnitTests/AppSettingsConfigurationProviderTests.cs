using System;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace GV.AspNet.Configuration.ConfigurationManager.UnitTests
{
	public class AppSettingsConfigurationProviderTests
	{
		public class Load
		{
			[Theory]
			[InlineData("", "Value")]
			[InlineData("Key", "Value")]
			public void AddsAppSettings(string key, string value)
			{
				var appSettings = new NameValueCollection { { key, value } };
				var keyDelimiter = Constants.KeyDelimiter;
				var keyPrefix = string.Empty;
				var source = new AppSettingsConfigurationProvider(appSettings, keyDelimiter, keyPrefix);

				source.Load();

				string configurationValue;
				Assert.True(source.TryGet(key, out configurationValue));
				Assert.Equal(value, configurationValue);
			}

			[Theory]
			[InlineData("Parent.Key", "", "Parent.Key", "Value")]
			[InlineData("Parent.Key", ".", "Parent:Key", "Value")]
			public void ReplacesKeyDelimiter(string appSettingsKey, string keyDelimiter, string configurationKey, string value)
			{
				var appSettings = new NameValueCollection { { appSettingsKey, value } };
				var keyPrefix = string.Empty;
				var source = new AppSettingsConfigurationProvider(appSettings, keyDelimiter, keyPrefix);

				source.Load();

				string configurationValue;
				Assert.True(source.TryGet(configurationKey, out configurationValue));
				Assert.Equal(value, configurationValue);
			}
		}
	}
}