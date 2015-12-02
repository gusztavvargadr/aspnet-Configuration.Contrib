using System;
using System.Configuration;
using GV.AspNet.Configuration.ConfigurationManager;

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.Configuration
{
	public static class ConnectionStringsConfigurationExtensions
	{
		public static IConfigurationBuilder AddConnectionStrings(this IConfigurationBuilder configurationBuilder)
			=> configurationBuilder.AddConnectionStrings(ConfigurationManager.ConnectionStrings);

		public static IConfigurationBuilder AddConnectionStrings(this IConfigurationBuilder configurationBuilder, System.Configuration.Configuration configuration)
			=> configurationBuilder.AddConnectionStrings(configuration.ConnectionStrings.ConnectionStrings);

		public static IConfigurationBuilder AddConnectionStrings(this IConfigurationBuilder configurationBuilder, ConnectionStringSettingsCollection connectionStrings)
		{
			if (configurationBuilder == null)
			{
				throw new ArgumentNullException(nameof(configurationBuilder));
			}

			return configurationBuilder.Add(new ConnectionStringsConfigurationProvider(connectionStrings));
		}
	}
}