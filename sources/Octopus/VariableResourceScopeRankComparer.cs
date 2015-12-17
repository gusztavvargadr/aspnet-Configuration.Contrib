using System;
using System.Collections.Generic;
using Octopus.Client.Model;

namespace GV.AspNet.Configuration.Contrib.Octopus
{
	internal class VariableResourceScopeRankComparer : IComparer<VariableResource>
	{
		public int Compare(VariableResource x, VariableResource y) => y.Scope.Rank().CompareTo(x.Scope.Rank());
	}
}