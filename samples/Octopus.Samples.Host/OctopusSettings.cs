using System;

namespace GV.AspNet.Configuration.Contrib.Octopus.Samples.Host
{
	public class OctopusSettings
	{
		public bool PrintVariables { get; set; }
		public bool PrintEvaluatedVariables { get; set; }

		public override string ToString() => $"PrintVariables: {PrintVariables}; PrintEvaluatedVariables: {PrintEvaluatedVariables}";
	}
}