using System;
using System.Collections.Specialized;
using Xunit;

namespace GV.AspNet.Configuration.ConfigurationManager.UnitTests
{
	public class AppSettingsConfigurationProviderTests
	{
		[Theory]
		[InlineData("Key1", "Value1")]
		[InlineData("Key2", "Value2")]
		public void LoadsKeyValuePairsFromAppSettings(string key, string value)
		{
			var appSettings = new NameValueCollection { { key, value } };
			var source = new AppSettingsConfigurationProvider(appSettings, ":", string.Empty);

			source.Load();

			string outValue;
			Assert.True(source.TryGet(key, out outValue));
			Assert.Equal(value, outValue);
		}
	}
}