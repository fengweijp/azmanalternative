using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	public class AdminManager : Interfaces.IAdminManager
	{
		private readonly Interfaces.IAdminManager _AdminManager;

		public int MajorVersion
		{
			get { return _AdminManager.MajorVersion; }
		}

		public int MinorVersion
		{
			get { return _AdminManager.MinorVersion; }
		}

		public string Description
		{
			get { return _AdminManager.Description; }
			set { _AdminManager.Description = value; }
		}

		public Guid Guid
		{
			get { return _AdminManager.Guid; }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		{
			get
			{
				return _AdminManager.Groups;
			}
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Application> Applications
		{
			get { return _AdminManager.Applications; }
		}

		internal AdminManager(Interfaces.IAdminManager adminManager)
		{
			_AdminManager = adminManager;
		}

		public void DeleteGroup(ApplicationGroup group)
		{
			if (group.Guid == Guid.Empty)
				throw new AzException("The group has not been added to the store.");

			_AdminManager.DeleteGroup(group);
		}

		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name", "Name cannot be blank when adding a group.");

			return _AdminManager.CreateGroup(name, description, groupType);
		}

		public Application CreateApplication(string name, string description, string versionInformation)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name", "A name must be specified when adding an application");

			return _AdminManager.CreateApplication(name, description, versionInformation);
		}

		public void DeleteApplication(Application application)
		{
			if (application.Guid == Guid.Empty)
				throw new AzException("The application has not been added to the store.");

			_AdminManager.DeleteApplication(application);
		}
	}
}
