using System;
using System.Configuration;
using Xunit;

namespace Microsoft.Framework.Configuration.Contrib.GV.AppSettings.UnitTests
{
	public class AppSettingsConfigurationSourceTests
	{
		[Fact]
		public void LoadsKeyValuePairsFromAppSettings()
		{
			var exeConfiguration = ConfigurationManager.OpenExeConfiguration("Microsoft.Framework.Configuration.Contrib.GV.AppSettings.Core.UnitTests.dll");

			var configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddAppSettings(exeConfiguration.AppSettings);
			var configuration = configurationBuilder.Build();

			Assert.Equal("Value1", configuration["Key1"]);
			Assert.Equal("Value2", configuration["Key2"]);
		}
	}
}