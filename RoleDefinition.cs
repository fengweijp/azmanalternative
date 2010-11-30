using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
    /// <summary>
    /// An application role. Used to define a role and check membership
    /// </summary>
	public class RoleDefinition : Task, Interfaces.IRoleDefinition
	{
        /// <summary>
        /// Gets the collection of roles added to the role
        /// </summary>
		public Collections.RoleDefinitionCollection Roles
		{
			get { return ((Interfaces.IRoleDefinition)Instance).Roles; }
		}

		internal RoleDefinition(Interfaces.IRoleDefinition role)
			: base(role)
		{
		}

		internal RoleDefinition(Interfaces.IRoleDefinition role, Application parent)
			: base(role, parent)
		{ }

        /// <summary>
        /// Adds a role to this role
        /// </summary>
        /// <param name="role">Role to add</param>
		public void AddRole(RoleDefinition role)
		{
			if (role == null)
				throw new ArgumentNullException("role");

            CheckObjectIsValid(role);
			if (Roles.ContainsGuid(role.Guid))
				return;

            ((Interfaces.IRoleDefinition)Instance).AddRole(role);
			Roles.AddValue(role);
		}

		public void AddRole(string name)
		{
			RoleDefinition r = Parent.Roles[name];
			AddRole(r);
		}

        /// <summary>
        /// Removes a role from this role
        /// </summary>
        /// <param name="role">Role to remove</param>
		public void RemoveRole(RoleDefinition role)
		{
			if (role == null)
				throw new ArgumentNullException("role");

            CheckObjectIsValid(role);
			if (!Roles.ContainsGuid(role.Guid))
				return;

			((Interfaces.IRoleDefinition)Instance).RemoveRole(role);
			Roles.RemoveValue(role.Guid);
		}

		public void RemoveRole(string name)
		{
			RoleDefinition r = Roles[name];
			RemoveRole(r);
		}

	}
}
