using System;

namespace GV.AspNet.Configuration.Contrib.Octopus.Samples.Host
{
	public class DatabaseSettings
	{
		public string Name { get; set; }
		public string Server { get; set; }

		public override string ToString() => $"Name: {Name}; Server: {Server}";
	}
}