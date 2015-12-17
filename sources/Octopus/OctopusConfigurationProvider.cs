using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace GV.AspNet.Configuration.Contrib.Octopus
{
	public class OctopusConfigurationProvider : ConfigurationProvider
	{
		private const string ConfigurationKeyPrefix = "AppSettings";
		private static readonly string ConfigurationKeyDelimiter = Constants.KeyDelimiter;

		public OctopusConfigurationProvider(
			OctopusConfigurationProviderOptions options,
			IVariableDictionaryProvider variableDictionaryProvider,
			string appSettingsKeyDelimiter,
			params string[] appSettingsSectionPrefixes)
		{
			if (options == null)
			{
				throw new ArgumentNullException(nameof(options));
			}
			if (variableDictionaryProvider == null)
			{
				throw new ArgumentNullException(nameof(variableDictionaryProvider));
			}

			Options = options;
			VariableDictionaryProvider = variableDictionaryProvider;
			AppSettingsKeyDelimiter = appSettingsKeyDelimiter;
			AppSettingsSectionPrefixes = appSettingsSectionPrefixes;
		}

		private OctopusConfigurationProviderOptions Options { get; }
		private IVariableDictionaryProvider VariableDictionaryProvider { get; }
		private string AppSettingsKeyDelimiter { get; }
		private IEnumerable<string> AppSettingsSectionPrefixes { get; }

		public override void Load()
		{
			var variableDictionary = VariableDictionaryProvider.Get(Options);

			foreach (var name in variableDictionary.GetNames())
			{
				var key = GetConfigurationKey(name);
				Data[key] = variableDictionary.Get(name);
			}
		}

		private string GetConfigurationKey(string appSettingsKey)
		{
			var configurationKey = appSettingsKey;

			if (!string.IsNullOrEmpty(AppSettingsKeyDelimiter))
			{
				configurationKey = configurationKey.Replace(AppSettingsKeyDelimiter, ConfigurationKeyDelimiter);
			}

			foreach (var appSettingsSectionPrefix in AppSettingsSectionPrefixes)
			{
				if (!configurationKey.StartsWith(appSettingsSectionPrefix, StringComparison.CurrentCultureIgnoreCase))
				{
					continue;
				}

				configurationKey = string.Concat(appSettingsSectionPrefix, ConfigurationKeyDelimiter, configurationKey.Substring(appSettingsSectionPrefix.Length));
				break;
			}

			configurationKey = string.Concat(ConfigurationKeyPrefix, ConfigurationKeyDelimiter, configurationKey.Trim());
			return configurationKey;
		}
	}
}