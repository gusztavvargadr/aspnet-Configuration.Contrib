using System;
using Microsoft.Framework.Configuration;
using Xunit;

namespace GV.AspNet.Configuration.ConfigurationManager.UnitTests
{
	public class AppSettingsConfigurationSourceTests
	{
		[Theory]
		[InlineData("Key1", "Value1")]
		[InlineData("Key2", "Value2")]
		public void LoadsKeyValuePairsFromAppSettings(string key, string value)
		{
			var exeConfiguration = System.Configuration.ConfigurationManager.OpenExeConfiguration("GV.AspNet.Configuration.ConfigurationManager.Core.UnitTests.dll");

			var configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddAppSettings(exeConfiguration.AppSettings);
			var configuration = configurationBuilder.Build();

			var configurationValue = configuration[key];
			Assert.Equal(value, configurationValue);
		}
	}
}