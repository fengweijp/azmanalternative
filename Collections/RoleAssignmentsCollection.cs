using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public class RoleAssignmentsCollection : CollectionBase<RoleAssignments>
	{

		internal RoleAssignmentsCollection(ServiceBase service, Dictionary<string, Guid> values)
			: base(service, values)
		{
			ItemLoader = Service.GetRoleAssignments;
		}

		internal RoleAssignmentsCollection(ServiceBase service)
			: this(service, new Dictionary<string, Guid>())
		{ }

		public override IEnumerator<RoleAssignments> GetEnumerator()
		{
			return Service.GetRoleAssignmentsCollection(Guids.Values, Application);
		}
	}
}
