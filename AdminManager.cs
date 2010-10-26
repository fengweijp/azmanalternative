using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
    /// <summary>
    /// Represents the top level Authorisation store
    /// </summary>
    public class AdminManager : Interfaces.IAdminManager
	{
		private readonly Interfaces.IAdminManager _AdminManager;

        /// <summary>
        /// Gets the schema major version
        /// </summary>
        public int MajorVersion
		{
			get { return _AdminManager.MajorVersion; }
		}

        /// <summary>
        /// Gets the schema minor version
        /// </summary>
        public int MinorVersion
		{
			get { return _AdminManager.MinorVersion; }
		}

        /// <summary>
        /// Gets and sets the store description
        /// </summary>
        public string Description
		{
			get { return _AdminManager.Description; }
			set { _AdminManager.Description = value; }
		}

        /// <summary>
        /// Gets the object identifier
        /// </summary>
        public Guid Guid
		{
			get { return _AdminManager.Guid; }
		}

        /// <summary>
        /// Gets a collection of application groups
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		{
			get
			{
				return _AdminManager.Groups;
			}
		}

        /// <summary>
        /// Gets a collection of applications
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<Application> Applications
		{
			get { return _AdminManager.Applications; }
		}

		internal AdminManager(Interfaces.IAdminManager adminManager)
		{
			_AdminManager = adminManager;
		}

        /// <summary>
        /// Removes an application group from the store
        /// </summary>
        /// <param name="group">The group to remove</param>
        public void DeleteGroup(ApplicationGroup group)
		{
			if (group.Guid == Guid.Empty)
				throw new AzException("The group has not been added to the store.");

			_AdminManager.DeleteGroup(group);
		}

        /// <summary>
        /// Add an application group to the store
        /// </summary>
        /// <param name="name">group name. Must not be null or empty</param>
        /// <param name="description">Group description</param>
        /// <param name="groupType">The type of group</param>
        public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name", "Name cannot be blank when adding a group.");

			return _AdminManager.CreateGroup(name, description, groupType);
		}

        /// <summary>
        /// Add an application to the store
        /// </summary>
        /// <param name="name">The application name. Must not be null or empty</param>
        /// <param name="description">Application description</param>
        /// <param name="versionInformation">Version information</param>
        public Application CreateApplication(string name, string description, string versionInformation)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name", "A name must be specified when adding an application");

			return _AdminManager.CreateApplication(name, description, versionInformation);
		}

        /// <summary>
        /// Removes an application from the store
        /// </summary>
        /// <param name="application">The application to remove</param>
        public void DeleteApplication(Application application)
		{
			if (application.Guid == Guid.Empty)
				throw new AzException("The application has not been added to the store.");

			_AdminManager.DeleteApplication(application);
		}
	}
}
