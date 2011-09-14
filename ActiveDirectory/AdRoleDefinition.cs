using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	internal class AdRoleDefinition : AdTask, Interfaces.IRoleDefinition
	{
		private const string ISROLE = "msDS-AzTaskIsRoleDefinition";

		public Collections.RoleDefinitionCollection Roles
		{
			get;
			set;
		}

		public void AddRole(RoleDefinition role)
		{
			Service.UpdateListAttribute(Key, TASKS, role.Key, DirectoryAttributeOperation.Add);
		}

		public void RemoveRole(RoleDefinition role)
		{
			Service.UpdateListAttribute(Key, TASKS, role.Key, DirectoryAttributeOperation.Delete);
		}

		protected override System.DirectoryServices.Protocols.AddRequest CreateNewThis()
		{
			AddRequest ar = base.CreateNewThis();
			ar.Attributes.Add(CreateAttribute(ISROLE, "TRUE"));

			return ar;
		}

		public override void Load(System.DirectoryServices.Protocols.SearchResultEntry entry)
		{
			base.Load(entry);

			Roles = GetRoles(Key, true);
		}

		public static Collections.RoleDefinitionCollection GetRoles(string key, bool isChildList)
		{
			string searchbase = key;
			string filter = "(&(ObjectClass=" + CLASSNAME + ")(msDS-AzTaskIsRoleDefinition=TRUE))";
			if (isChildList)
			{
				searchbase = key.Substring(key.IndexOf(",") + 1);
				filter = "(&(msDS-TasksForAzTaskBL=" + key + ")(msDS-AzTaskIsRoleDefinition=TRUE))";
			}

			var results = ((AdService)Locator.Service).Load(searchbase, filter);
			var q = from i in results.Cast<SearchResultEntry>() select i;
			return new Collections.RoleDefinitionCollection(q.ToDictionary(x => x.Attributes["name"][0].ToString(), x => x.DistinguishedName), isChildList);
		}
	}
}
