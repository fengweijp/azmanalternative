using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	/// <summary>
	/// A collection of role definitions
	/// </summary>
	public class RoleDefinitionCollection : CollectionBase<RoleDefinition>
	{
		protected override string ErrorObjectName
		{
			get { return "role"; }
		}
		internal RoleDefinitionCollection(Dictionary<string, string> values, bool isChildList)
			: base(values, isChildList)
		{ }

		internal RoleDefinitionCollection(bool isChildList)
			: this(new Dictionary<string, string>(), isChildList)
		{ }

		public override IEnumerator<RoleDefinition> GetEnumerator()
		{
			return Service.GetRoleDefinitions(Guids.Values, Application);
		}

		protected override ContainerBase LinkedItemLoader(string name)
		{
			return Application.Roles[name];
		}

		protected override RoleDefinition ItemLoader(string uniqueName)
		{
			return Service.GetRoleDefinition(uniqueName);
		}
	}
}
