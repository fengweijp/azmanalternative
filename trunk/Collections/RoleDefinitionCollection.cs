using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public class RoleDefinitionCollection : CollectionBase<RoleDefinition>
	{

		internal RoleDefinitionCollection(ServiceBase service, Dictionary<string, string> values, bool linked)
			: base(service, values, linked)
		{ }

		internal RoleDefinitionCollection(ServiceBase service, bool linked)
			: this(service, new Dictionary<string, string>(), linked)
		{ }

		public override IEnumerator<RoleDefinition> GetEnumerator()
		{
			return Service.GetRoleDefinitions(Guids.Values, Application);
		}

		internal override void CheckName(RoleDefinition entry)
		{
			CheckName(entry, "role");
		}

		internal override void CheckName(string name)
		{
			CheckName(name, "role");
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
