using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace GV.AspNet.Configuration.ConfigurationManager
{
	public class ConnectionStringsConfigurationProvider : ConfigurationProvider
	{
		public ConnectionStringsConfigurationProvider(ConnectionStringSettingsCollection connectionStrings)
		{
			if (connectionStrings == null)
			{
				throw new ArgumentNullException(nameof(connectionStrings));
			}

			ConnectionStrings = connectionStrings;
		}

		private ConnectionStringSettingsCollection ConnectionStrings { get; set; }

		public override void Load()
		{
			foreach (ConnectionStringSettings connectionString in ConnectionStrings)
			{
				Data[$"Data:{connectionString.Name}:ConnectionString"] = connectionString.ConnectionString;
				Data[$"Data:{connectionString.Name}:ProviderName"] = connectionString.ProviderName;
			}
		}
	}
}