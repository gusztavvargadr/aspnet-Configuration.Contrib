using System;
using System.Collections.Generic;
using System.Linq;
using Octopus.Client;
using Octopus.Client.Model;
using Octostache;

namespace GV.AspNet.Configuration.Contrib.Octopus
{
	public class OctopusClientVariableDictionaryProvider : IVariableDictionaryProvider
	{
		public VariableDictionary Get(OctopusConfigurationProviderOptions options)
		{
			var variableDictionary = new VariableDictionary();

			var endpoint = new OctopusServerEndpoint(options.ServerAddress.ToString(), options.ApiKey);
			var repository = new OctopusRepository(endpoint);

			var project = repository.Projects.FindByName(options.ProjectName);
			if (project == null)
			{
				return variableDictionary;
			}

			var scopes = new Dictionary<ScopeField, string>();
			if (!string.IsNullOrEmpty(options.EnvironmentName))
			{
				scopes[ScopeField.Environment] = options.EnvironmentName;
			}
			if (!string.IsNullOrEmpty(options.MachineName))
			{
				scopes[ScopeField.Machine] = options.MachineName;
			}

			var variableSetIds = new List<string> { project.VariableSetId };
			variableSetIds.AddRange(project.IncludedLibraryVariableSetIds.Select(id => repository.LibraryVariableSets.Get(id).VariableSetId));

			var variables = new Dictionary<string, SortedSet<VariableResource>>();
			foreach (var variableSetId in variableSetIds)
			{
				var variableSet = repository.VariableSets.Get(variableSetId);
				AddVariableSet(variables, variableSet, scopes);
			}

			foreach (var variableName in variables.Keys)
			{
				variableDictionary.Set(variableName, variables[variableName].First().Value);
			}

			return variableDictionary;
		}

		private void AddVariableSet(IDictionary<string, SortedSet<VariableResource>> variables, VariableSetResource variableSet, Dictionary<ScopeField, string> scopes)
		{
			var variableScopes = GetVariableScopes(variableSet, scopes);

			foreach (var variable in variableSet.Variables)
			{
				var variableName = variable.Name;

				if (variable.IsSensitive)
				{
					continue;
				}

				if (!variable.Scope.IsApplicableTo(variableScopes))
				{
					continue;
				}

				if (!variables.ContainsKey(variableName))
				{
					variables[variableName] = new SortedSet<VariableResource>(new VariableResourceScopeRankComparer());
				}
				variables[variableName].Add(variable);
			}
		}

		private Dictionary<ScopeField, ScopeValue> GetVariableScopes(VariableSetResource variableSet, Dictionary<ScopeField, string> scopes)
		{
			var variableScopes = new Dictionary<ScopeField, ScopeValue>();

			foreach (var scopeField in scopes.Keys)
			{
				var scopeValues = GetScopeValues(variableSet.ScopeValues, scopeField);
				var scopeValueName = scopes[scopeField];
				var scopeValue = scopeValues.FirstOrDefault(item => item.Name == scopeValueName);
				if (scopeValue == null)
				{
					continue;
				}

				variableScopes.Add(scopeField, new ScopeValue(scopeValue.Id));
			}

			return variableScopes;
		}

		private static IEnumerable<ReferenceDataItem> GetScopeValues(VariableScopeValues scopeValues, ScopeField scopeField)
		{
			switch (scopeField)
			{
				case ScopeField.Environment:
					return scopeValues.Environments;

				case ScopeField.Machine:
					return scopeValues.Machines;

				default:
					return new ReferenceDataItem[] { };
			}
		}
	}
}