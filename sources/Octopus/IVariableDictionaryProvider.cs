using System;
using Octostache;

namespace GV.AspNet.Configuration.Contrib.Octopus
{
	public interface IVariableDictionaryProvider
	{
		VariableDictionary Get(OctopusConfigurationProviderOptions options);
	}
}