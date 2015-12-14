using System;

namespace GV.AspNet.Configuration.Contrib.ConfigurationManager.Samples.Host
{
	public class AppSettings
	{
		public string Key1 { get; set; }
		public string Key2 { get; set; }

		public Uri Host { get; set; }
		public ConnectionSettings Connection { get; set; }

		public override string ToString() => $"Key1: {Key1}; Key2: {Key2}; Host: {Host}; Connection: {Connection}";
	}
}