using System;

namespace GV.AspNet.Configuration.Contrib.ConfigurationManager.Samples.Host
{
	public class ConnectionSettings
	{
		public TimeSpan TimesOutIn { get; set; }
		public int RetryCount { get; set; }

		public override string ToString() => $"TimesoutIn: {TimesOutIn}; RetryCount: {RetryCount}";
	}
}