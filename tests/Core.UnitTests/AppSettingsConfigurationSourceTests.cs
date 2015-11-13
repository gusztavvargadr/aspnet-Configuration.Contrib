using System;
using Microsoft.Framework.Configuration;
using Xunit;

namespace GV.AspNet.Configuration.ConfigurationManager.UnitTests
{
	public class AppSettingsConfigurationSourceTests
	{
		[Fact]
		public void LoadsKeyValuePairsFromAppSettings()
		{
			var exeConfiguration = System.Configuration.ConfigurationManager.OpenExeConfiguration("GV.AspNet.Configuration.ConfigurationManager.Core.UnitTests.dll");

			var configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddAppSettings(exeConfiguration.AppSettings);
			var configuration = configurationBuilder.Build();

			Assert.Equal("Value1", configuration["Key1"]);
			Assert.Equal("Value2", configuration["Key2"]);
		}
	}
}