using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.ActiveDirectory
{
	internal class AdRoleAssignments : AdBaseObject, Interfaces.IRoleAssignment
	{
		protected override string ObjectClass
		{
			get { return "msDS-AzRole"; }
		}

		public RoleDefinition Definition
		{
			get;
			set;
		}

		public Collections.ApplicationGroupCollection Groups
		{
			get;
			set;
		}

		public Collections.MemberCollection Members
		{
			get;
			set;
		}

		public void AddGroup(ApplicationGroup group)
		{
			throw new NotImplementedException();
		}

		public void RemoveGroup(ApplicationGroup group)
		{
			throw new NotImplementedException();
		}

		public void AddMember(string name)
		{
			throw new NotImplementedException();
		}

		public void RemoveMember(string name)
		{
			throw new NotImplementedException();
		}
	}
}
