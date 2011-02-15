using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.ActiveDirectory
{
	internal class AdRoleDefinition : AdTask, Interfaces.IRoleDefinition
	{
		private const string ROLE = "msDS-TasksForAzRoleBL";
		//msDS-AzTaskIsRoleDefinition

		public Collections.RoleDefinitionCollection Roles
		{
			get { throw new NotImplementedException(); }
		}

		public AdRoleDefinition(AdService service)
			: base(service)
		{}

		public void AddRole(RoleDefinition role)
		{
			throw new NotImplementedException();
		}

		public void RemoveRole(RoleDefinition role)
		{
			throw new NotImplementedException();
		}
	}
}
