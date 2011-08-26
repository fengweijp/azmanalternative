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
			get { throw new NotImplementedException(); }
		}

		public Collections.ApplicationGroupCollection Groups
		{
			get { throw new NotImplementedException(); }
		}

		public Collections.MemberCollection Members
		{
			get { throw new NotImplementedException(); }
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
