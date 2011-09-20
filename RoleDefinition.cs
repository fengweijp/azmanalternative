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

		public override void Delete()
		{
			Locator.Factory.DeleteRole((Interfaces.IRoleDefinition)Instance);
			Parent.Roles.RemoveValue(Key);
			IsDeleted = true;
		}

		public override void Save()
		{
			Parent.Roles.CheckName(this);
			Parent.Roles.UpdateValue(this);
			Locator.Factory.UpdateRole((Interfaces.IRoleDefinition)Instance);
			Parent.Roles.UpdateKey(this);
		}

		/// <summary>
		/// Adds a role to the Roles collection
		/// </summary>
		/// <param name="role">Role to add</param>
		public void AddRole(RoleDefinition role)
		{
			if (role == null)
				throw new ArgumentNullException("role");

			CheckObjectIsValid(role);
			if (Roles.ContainsKey(role.Key))
				return;

			((Interfaces.IRoleDefinition)Instance).AddRole(role);
			Roles.AddValue(role);
		}

		/// <summary>
		/// Adds a role to the Roles collection
		/// </summary>
		/// <param name="name">name of the role to add</param>
		public void AddRole(string name)
		{
			RoleDefinition r = Parent.Roles[name];
			AddRole(r);
		}

		/// <summary>
		/// Removes a role from the Roles collection
		/// </summary>
		/// <param name="role">Role to remove</param>
		public void RemoveRole(RoleDefinition role)
		{
			if (role == null)
				throw new ArgumentNullException("role");

			CheckObjectIsValid(role);
			if (!Roles.ContainsKey(role.Key))
				return;

			((Interfaces.IRoleDefinition)Instance).RemoveRole(role);
			Roles.RemoveValue(role.Key);
		}

		/// <summary>
		/// Removes a role from the Roles collection
		/// </summary>
		/// <param name="name">name of the role to remove</param>
		public void RemoveRole(string name)
		{
			RoleDefinition r = Roles[name];
			RemoveRole(r);
		}

	}
}
