using System;

namespace GV.AspNet.Configuration.Contrib.Octopus
{
	public class OctopusConfigurationProviderOptions
	{
		public Uri ServerAddress { get; set; }
		public string ApiKey { get; set; }
		public string ProjectName { get; set; }
		public string EnvironmentName { get; set; }
		public string MachineName { get; set; }
	}
}