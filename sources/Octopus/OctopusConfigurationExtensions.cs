using System;
using GV.AspNet.Configuration.Contrib.Octopus;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration
{
	public static class OctopusConfigurationExtensions
	{
		public static IConfigurationBuilder AddOctopus(this IConfigurationBuilder configuration, OctopusConfigurationProviderOptions options)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			return configuration.Add(new OctopusConfigurationProvider(options, new OctopusClientVariableDictionaryProvider()));
		}
	}
}