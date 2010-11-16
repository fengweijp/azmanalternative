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
		public string Name
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
		public List<Interfaces.IMember> Members
		{
			get
			{
				throw new NotImplementedException();
			}
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
        /// Validates the properties of the group
        /// </summary>
        /// <returns>returns true if the group is valid</returns>
		internal bool ValidateGroup()
		{
			if (string.IsNullOrEmpty(Name))
				return false;

			return true;
		}

        /// <summary>
        /// Add a member to the group
        /// </summary>
        /// <param name="member">Member to add</param>
		public void AddMember(Interfaces.IMember member)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Removes a member from the group
        /// </summary>
        /// <param name="member">Member to remove</param>
		public void RemoveMember(Interfaces.IMember member)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Adds a group to this group
        /// </summary>
        /// <param name="group">Group to add</param>
        public void AddGroup(ApplicationGroup group)
        {
			//CheckGroupIsValid(group);
            Instance.AddGroup(group);
        }

        /// <summary>
        /// Removes a group from this group
        /// </summary>
        /// <param name="group">Group to remove</param>
        public void RemoveGroup(ApplicationGroup group)
        {
			//CheckGroupIsValid(group);
            Instance.RemoveGroup(group);
        }

		//private void CheckGroupIsValid(ApplicationGroup group)
		//{
		//    if (this.Store != null)
		//    {
		//        if (group.Store == null || group.Store.Guid != this.Store.Guid)
		//            throw new AzException("The group to add is not defined in this store or is not a global group");
		//    }
		//    if (this.Application != null)
		//    {
		//        CheckObjectIsValid(group);
		//        if (group.Store != null && group.Store.Guid != this.Application.Store.Guid)
		//            throw new AzException("The group to add is not defined in this store.");
		//    }
		//}

	}
}
