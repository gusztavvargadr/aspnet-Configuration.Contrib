using System;
using System.Configuration;
using Xunit;

namespace GV.AspNet.Configuration.ConfigurationManager.UnitTests
{
	public class ConnectionStringsConfigurationProviderTests
	{
		public class Load
		{
			[Theory]
			[InlineData("DefaultConnection", "ConnectionString", "ProviderName")]
			public void AddsConnectionStrings(string name, string connectionString, string providerName)
			{
				var connectionStrings = new ConnectionStringSettingsCollection
				{
					new ConnectionStringSettings { Name = name, ConnectionString = connectionString, ProviderName = providerName }
				};
				var provider = new ConnectionStringsConfigurationProvider(connectionStrings);

				provider.Load();

				string configurationConnectionString;
				Assert.True(provider.TryGet($"Data:{name}:ConnectionString", out configurationConnectionString));
				Assert.Equal(connectionString, configurationConnectionString);

				string configurationProviderName;
				Assert.True(provider.TryGet($"Data:{name}:ProviderName", out configurationProviderName));
				Assert.Equal(providerName, configurationProviderName);
			}
		}
	}
}