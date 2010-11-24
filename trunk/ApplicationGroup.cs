using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
    /// <summary>
    /// A group of users in the application or store
    /// </summary>
	public class ApplicationGroup : ContainerBase, Interfaces.IApplicationGroup
	{
		internal readonly Interfaces.IApplicationGroup Instance;
		private AdminManager _Store;

		public override Application Parent
		{
			get { return BaseApplication; }
			internal set
			{
				BaseApplication = value;
				Groups.Application = value;
			}
		}

		/// <summary>
		/// Gets the group identifier
		/// </summary>
		public override Guid Guid
		{
			get { return Instance.Guid; }
		}

		/// <summary>
		/// Gets or sets the name of the group
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
		/// Gets or sets the group description
		/// </summary>
		public string Description
		{
			get { return Instance.Description; }
			set { Instance.Description = value; }
		}

        /// <summary>
        /// Gets the type of group
        /// </summary>
		public GroupType GroupType
		{
			get { return Instance.GroupType; }
		}

		public bool IsGlobalGroup
		{
			get { return Instance.IsGlobalGroup; }
		}

        /// <summary>
        /// Gets the collection of members in the group
        /// </summary>
		public Collections.MemberCollection Members
		{
			get { return Instance.Members; }
		}

		public Collections.MemberCollection Exclusions
		{
			get { return Instance.Exclusions; }
		}

        /// <summary>
        /// Gets a collection of groups in this group
        /// </summary>
        public Collections.ApplicationGroupCollection Groups
        {
            get { return Instance.Groups; }
        }

        /// <summary>
        /// Gets the store this group belongs to
        /// </summary>
        public AdminManager Store
        {
			get { return _Store; }
			internal set
			{
				_Store = value;
				Groups.Store = value;
			}
        }

		internal ApplicationGroup(Interfaces.IApplicationGroup applicationGroup)
		{
			Instance = applicationGroup;
		}

        /// <summary>
        /// Add a member to the group
        /// </summary>
        /// <param name="member">Member to add</param>
		public void AddMember(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			Instance.AddMember(name);
		}

		public void AddMember(string name, string domain)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if (string.IsNullOrEmpty(domain))
				throw new ArgumentNullException("domain");

			AddMember(domain + "\\" + name);
		}

        /// <summary>
        /// Removes a member from the group
        /// </summary>
        /// <param name="member">Member to remove in the form domain\name</param>
		public void RemoveMember(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			Instance.RemoveMember(name);
		}

        /// <summary>
        /// Adds a group to this group
        /// </summary>
        /// <param name="group">Group to add</param>
        public void AddGroup(ApplicationGroup group)
        {
			CheckGroupIsValid(group);
			if (Groups.ContainsGuid(group.Guid))
				return;

            Instance.AddGroup(group);
			Groups.AddValue(group);
        }

        /// <summary>
        /// Removes a group from this group
        /// </summary>
        /// <param name="group">Group to remove</param>
        public void RemoveGroup(ApplicationGroup group)
        {
			CheckGroupIsValid(group);
			if (!Groups.ContainsGuid(group.Guid))
				return;

            Instance.RemoveGroup(group);
			Groups.RemoveValue(group.Guid);
        }

		public void RemoveGroup(string name)
		{
			ApplicationGroup g = Groups[name];
			if (g == null)
				return;

			RemoveGroup(g);
		}

		private void CheckGroupIsValid(ApplicationGroup group)
		{
			if (group == null)
				throw new ArgumentNullException("group");

			if (IsGlobalGroup)
			{
				if (!group.IsGlobalGroup || group.Store.Guid != this.Store.Guid)
					throw new AzException("The group to add is not defined in this store or is not a global group");
			}

			if (this.Parent != null)
			{
				CheckObjectIsValid(group);
				if (group.IsGlobalGroup && group.Store.Guid != this.Parent.Store.Guid)
					throw new AzException("The group to add is not defined in this store.");
			}
		}
	}
}
