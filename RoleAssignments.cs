using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	/// <summary>
	/// Represents an assignment of groups and users to a role
	/// </summary>
	public class RoleAssignments: ContainerBase, Interfaces.IRoleAssignment
	{
		private readonly Interfaces.IRoleAssignment Instance;

		/// <summary>
		/// Gets the owning application for this group, if defined
		/// </summary>
		public override Application Parent
		{
			get { return BaseApplication; }
			internal set
			{
				BaseApplication = value;
				
			}
		}

		/// <summary>
		/// Gets the role identifier
		/// </summary>
		public override string Key
		{
			get { return Instance.Key; }
		}

		/// <summary>
		/// Gets or sets the role name
		/// </summary>
		public override string Name
		{
			get { return Instance.Name; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("Name");
				Instance.Name = value;
			}
		}

		/// <summary>
		/// Gets or sets the role description
		/// </summary>
		public string Description
		{
			get { return Instance.Description; }
			set { Instance.Description = value; }
		}

		/// <summary>
		/// Gets the role this assignment is for
		/// </summary>
		public RoleDefinition Definition
		{
			get { return Instance.Definition; }
		}

		/// <summary>
		/// Gets the collection of groups in the role
		/// </summary>
		public Collections.ApplicationGroupCollection Groups
		{
			get { return Instance.Groups; }
		}

		/// <summary>
		/// Gets the collection of members assigned to the role
		/// </summary>
		public Collections.MemberCollection Members
		{
			get { return Instance.Members; }
		}

		internal RoleAssignments(Interfaces.IRoleAssignment role)
			: base()
		{
			Instance = role;
		}

		internal RoleAssignments(Interfaces.IRoleAssignment role, Application parent)
			: this(role)
		{
			Parent = parent;
		}

		public void Delete()
		{
			Locator.Factory.DeleteRoleAssignments(Instance);
			Parent.RoleAssignments.RemoveValue(Key);
			IsDeleted = true;
		}

		public void Save()
		{
			Parent.RoleAssignments.CheckName(this);
			Parent.RoleAssignments.UpdateValue(this);
			Locator.Factory.UpdateRoleAssignments(Instance);
			Parent.RoleAssignments.UpdateKey(this);
		}

		/// <summary>
		/// Adds a group to the role
		/// </summary>
		/// <param name="group">Group to add</param>
		public void AddGroup(ApplicationGroup group)
		{
			CheckObjectIsValid(group);
			if (Groups.ContainsKey(group.Key))
				return;

			Instance.AddGroup(group);
			Groups.AddValue(group);
		}

		/// <summary>
		/// Removes a group from the role
		/// </summary>
		/// <param name="group">group to remove</param>
		public void RemoveGroup(ApplicationGroup group)
		{
			CheckObjectIsValid(group);
			if (!Groups.ContainsKey(group.Key))
				return;

			Instance.RemoveGroup(group);
			Groups.RemoveValue(group.Key);
		}

		/// <summary>
		/// Adds a member to the role
		/// </summary>
		/// <param name="member">Member to add in the form domainname\username</param>
		public void AddMember(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			Instance.AddMember(name);
		}

		/// <summary>
		/// Adds a member to the role
		/// </summary>
		/// <param name="name">username</param>
		/// <param name="domain">domain name</param>
		public void AddMember(string name, string domain)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if (string.IsNullOrEmpty(domain))
				throw new ArgumentNullException("domain");

			AddMember(domain + "\\" + name);
		}

		/// <summary>
		/// Removes a member from the role
		/// </summary>
		/// <param name="member">Member to remove in the form domain\name</param>
		public void RemoveMember(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			Instance.RemoveMember(name);
		}

		protected override bool CheckObjectIsValid(ContainerBase o)
		{
			if (o == null)
				throw new ArgumentNullException("group");

			ApplicationGroup g = (ApplicationGroup)o;

			if (g.IsGlobalGroup)
			{
				if (g.Store.Key != this.Parent.Store.Key)
					throw new AzException("The group is not defined in the current store.");
				return true;
			}
			else
				return base.CheckObjectIsValid(o);
		}
	}
}
