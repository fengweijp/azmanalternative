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
		Collections.RoleDefinitionCollection Interfaces.IRoleDefinition.Roles
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
            CheckObjectIsValid(role);
            ((Interfaces.IRoleDefinition)Instance).AddRole(role);
		}

        /// <summary>
        /// Removes a role from this role
        /// </summary>
        /// <param name="role">Role to remove</param>
		public void RemoveRole(RoleDefinition role)
		{
            CheckObjectIsValid(role);

			((Interfaces.IRoleDefinition)Instance).RemoveRole(role);
		}


	}
}
