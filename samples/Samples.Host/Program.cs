using System;
using Microsoft.Extensions.Configuration;

namespace GV.AspNet.Configuration.ConfigurationManager.Samples.Host
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddAppSettings();
			//configurationBuilder.AddAppSettings(
			//	System.Configuration.ConfigurationManager.OpenExeConfiguration(
			//		Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GV.AspNet.Configuration.ConfigurationManager.Samples.Host.exe")));
			var configuration = configurationBuilder.Build();

			var value1 = configuration["Key1"];
			var value2 = configuration["Key2"];
		}
	}
}