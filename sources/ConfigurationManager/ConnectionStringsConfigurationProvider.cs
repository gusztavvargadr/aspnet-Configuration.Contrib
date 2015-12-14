using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace GV.AspNet.Configuration.Contrib.ConfigurationManager
{
	public class ConnectionStringsConfigurationProvider : ConfigurationProvider
	{
		private const string ConfigurationKeyPrefix = "Data";
		private const string ConnectionStringConfigurationKeyPostfix = "ConnectionString";
		private const string ProviderNameConfigurationKeyPostfix = "ProviderName";
		private static readonly string ConfigurationKeyDelimiter = Constants.KeyDelimiter;

		public ConnectionStringsConfigurationProvider(ConnectionStringSettingsCollection connectionStrings)
		{
			if (connectionStrings == null)
			{
				throw new ArgumentNullException(nameof(connectionStrings));
			}

			ConnectionStrings = connectionStrings;
		}

		private ConnectionStringSettingsCollection ConnectionStrings { get; }

		public override void Load()
		{
			foreach (ConnectionStringSettings connectionString in ConnectionStrings)
			{
				var connectionStringConfigurationKey = GetConnectionStringConfigurationKey(connectionString);
				var connectionStringValue = GetConnectionStringValue(connectionString);
				Data[connectionStringConfigurationKey] = connectionStringValue;

				var providerNameConfigurationKey = GetProviderNameConfigurationKey(connectionString);
				var providerNameValue = GetProviderNameValue(connectionString);
				if (string.IsNullOrEmpty(providerNameValue))
				{
					continue;
				}
				Data[providerNameConfigurationKey] = providerNameValue;
			}
		}

		private static string GetConnectionStringConfigurationKey(ConnectionStringSettings connectionString)
			=> string.Concat(ConfigurationKeyPrefix, ConfigurationKeyDelimiter, connectionString.Name, ConfigurationKeyDelimiter, ConnectionStringConfigurationKeyPostfix);

		private static string GetConnectionStringValue(ConnectionStringSettings connectionString) => connectionString.ConnectionString;

		private static string GetProviderNameConfigurationKey(ConnectionStringSettings connectionString)
			=> string.Concat(ConfigurationKeyPrefix, ConfigurationKeyDelimiter, connectionString.Name, ConfigurationKeyDelimiter, ProviderNameConfigurationKeyPostfix);

		private static string GetProviderNameValue(ConnectionStringSettings connectionString) => connectionString.ProviderName;
	}
}