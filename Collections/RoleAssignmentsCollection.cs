using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	/// <summary>
	/// A collection of role assignments
	/// </summary>
	public class RoleAssignmentsCollection : CollectionBase<RoleAssignments>
	{
		protected override string ErrorObjectName
		{
			get { return "role assignment"; }
		}

		internal RoleAssignmentsCollection(ServiceBase service, Dictionary<string, string> values)
			: base(service, values)
		{ }

		internal RoleAssignmentsCollection(ServiceBase service)
			: this(service, new Dictionary<string, string>())
		{ }

		public override IEnumerator<RoleAssignments> GetEnumerator()
		{
			return Service.GetRoleAssignmentsCollection(Guids.Values, Application);
		}

		internal string MakeNameUnique(string name)
		{
			if (!Guids.ContainsKey(name))
				return name;

			string result;
			int count = 1;
			do
			{
				result = string.Format("{0}({1})", name, count);
				count++;
			} while (Guids.ContainsKey(result));

			return result;
		}

		protected override ContainerBase LinkedItemLoader(string name)
		{
			throw new NotSupportedException();
		}

		protected override RoleAssignments ItemLoader(string uniqueName)
		{
			return Service.GetRoleAssignments(uniqueName);
		}
	}
}
