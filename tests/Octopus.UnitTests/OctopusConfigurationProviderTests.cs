using System;
using System.Collections.Specialized;
using Moq;
using Octostache;
using Xunit;

namespace GV.AspNet.Configuration.Contrib.Octopus.UnitTests
{
	public class OctopusConfigurationProviderTests
	{
		private OctopusConfigurationProviderOptions Options { get; set; }
		private Mock<IVariableDictionaryProvider> VariableDictionaryProvider { get; set; }
		private VariableDictionary VariableDictionary { get; set; }

		public OctopusConfigurationProviderTests()
		{
			Options = new OctopusConfigurationProviderOptions();
			VariableDictionaryProvider = new Mock<IVariableDictionaryProvider>();
			VariableDictionary = new VariableDictionary();
			VariableDictionaryProvider.Setup(value => value.Get(Options)).Returns(VariableDictionary);
		}

		public class Load : OctopusConfigurationProviderTests
		{
			[Theory]
			[InlineData("", "AppSettings:", "Value")]
			[InlineData("Key", "AppSettings:Key", "Value")]
			public void AddsVariables(string appSettingsKey, string configurationKey, string value)
			{
				VariableDictionary.Set(appSettingsKey, value);
				var appSettingsKeyDelimiter = string.Empty;
				var provider = new OctopusConfigurationProvider(Options, VariableDictionaryProvider.Object, appSettingsKeyDelimiter);

				provider.Load();

				string configurationValue;
				Assert.True(provider.TryGet(configurationKey, out configurationValue));
				Assert.Equal(value, configurationValue);
			}

			[Theory]
			[InlineData("Parent.Key", "", "AppSettings:Parent.Key", "Value")]
			[InlineData("Parent.Key", ".", "AppSettings:Parent:Key", "Value")]
			public void ReplacesKeyDelimiter(string appSettingsKey, string appSettingsKeyDelimiter, string configurationKey, string value)
			{
				VariableDictionary.Set(appSettingsKey, value);
				var provider = new OctopusConfigurationProvider(Options, VariableDictionaryProvider.Object, appSettingsKeyDelimiter);

				provider.Load();

				string configurationValue;
				Assert.True(provider.TryGet(configurationKey, out configurationValue));
				Assert.Equal(value, configurationValue);
			}

			[Theory]
			[InlineData("ParentKey", "", "Parent", "AppSettings:Parent:Key", "Value")]
			public void ReplacesSectionPrefix(string appSettingsKey, string appSettingsKeyDelimiter, string appSettingsSectionPrefix, string configurationKey, string value)
			{
				VariableDictionary.Set(appSettingsKey, value);
				var provider = new OctopusConfigurationProvider(Options, VariableDictionaryProvider.Object, appSettingsKeyDelimiter, appSettingsSectionPrefix);

				provider.Load();

				string configurationValue;
				Assert.True(provider.TryGet(configurationKey, out configurationValue));
				Assert.Equal(value, configurationValue);
			}
		}
	}
}