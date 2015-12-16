using System;
using Microsoft.Extensions.Configuration;

namespace GV.AspNet.Configuration.Contrib.Octopus
{
	public class OctopusConfigurationProvider : ConfigurationProvider
	{
		public OctopusConfigurationProvider(OctopusConfigurationProviderOptions options, IVariableDictionaryProvider variableDictionaryProvider)
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
		}

		private OctopusConfigurationProviderOptions Options { get; }
		private IVariableDictionaryProvider VariableDictionaryProvider { get; }

		public override void Load()
		{
			var variableDictionary = VariableDictionaryProvider.Get(Options);

			foreach (var name in variableDictionary.GetNames())
			{
				Data[name] = variableDictionary.Get(name);
			}
		}
	}
}