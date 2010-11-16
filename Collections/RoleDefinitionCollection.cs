using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public class RoleDefinitionCollection : CollectionBase<RoleDefinition>
	{

		internal RoleDefinitionCollection(ServiceBase service, Dictionary<string, Guid> values)
			: base(service, values)
		{
			ItemLoader = Service.GetRoleDefinition;
		}

		internal RoleDefinitionCollection(ServiceBase service)
			: this(service, new Dictionary<string, Guid>())
		{ }

		public override IEnumerator<RoleDefinition> GetEnumerator()
		{
			return Service.GetRoleDefinitions(Guids.Values, Application);
		}
	}
}
