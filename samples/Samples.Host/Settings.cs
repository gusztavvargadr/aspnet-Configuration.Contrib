using System;

namespace GV.AspNet.Configuration.ConfigurationManager.Samples.Host
{
	public class AppSettings
	{
		public string Key1 { get; set; }
		public string Key2 { get; set; }

		public Uri Host { get; set; }
		public ConnectionSettings Connection { get; set; }

		public override string ToString() => $"Key1: {Key1}; Key2: {Key2}; Host: {Host}; Connection: {Connection}";
	}

	public class ConnectionSettings
	{
		public TimeSpan TimesOutIn { get; set; }
		public int RetryCount { get; set; }

		public override string ToString() => $"TimesoutIn: {TimesOutIn}; RetryCount: {RetryCount}";
	}
}