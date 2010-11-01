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
		private readonly Interfaces.IApplicationGroup _ApplicationGroup;

		/// <summary>
		/// Gets the group identifier
		/// </summary>
		public Guid Guid
		{
			get { return _ApplicationGroup.Guid; }
		}

		/// <summary>
		/// Gets or sets the name of the group
		/// </summary>
		public string Name
		{
			get { return _ApplicationGroup.Name; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("Name");
				_ApplicationGroup.Name = value;
			}
		}

		/// <summary>
		/// Gets or sets the group description
		/// </summary>
		public string Description
		{
			get { return _ApplicationGroup.Description; }
			set { _ApplicationGroup.Description = value; }
		}

        /// <summary>
        /// Gets the type of group
        /// </summary>
		public GroupType GroupType
		{
			get { return _ApplicationGroup.GroupType; }
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
        public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
        {
            get { return _ApplicationGroup.Groups; }
        }

        /// <summary>
        /// Gets the store this group belongs to
        /// </summary>
        public AdminManager Store
        {
            get;
            internal set;
        }

		internal ApplicationGroup(Interfaces.IApplicationGroup applicationGroup)
		{
			_ApplicationGroup = applicationGroup;
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
            _ApplicationGroup.AddGroup(group);
        }

        /// <summary>
        /// Removes a group from this group
        /// </summary>
        /// <param name="group">Group to remove</param>
        public void RemoveGroup(ApplicationGroup group)
        {
			//CheckGroupIsValid(group);
            _ApplicationGroup.RemoveGroup(group);
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
