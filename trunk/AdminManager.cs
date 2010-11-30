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
		private Interfaces.IAdminManager Instance;

		public string ConnectionString
		{
			get;
			private set;
		}

        /// <summary>
        /// Gets the schema major version
        /// </summary>
        public int MajorVersion
		{
			get { return Instance.MajorVersion; }
		}

        /// <summary>
        /// Gets the schema minor version
        /// </summary>
        public int MinorVersion
		{
			get { return Instance.MinorVersion; }
		}

        /// <summary>
        /// Gets and sets the store description
        /// </summary>
        public string Description
		{
			get { return Instance.Description; }
			set { Instance.Description = value; }
		}

        /// <summary>
        /// Gets the object identifier
        /// </summary>
        public Guid Guid
		{
			get { return Instance.Guid; }
		}

		/// <summary>
		/// Gets a collection of application groups
		/// </summary>
		public Collections.ApplicationGroupCollection Groups
		{
			get
			{
                return Instance.Groups;
			}
		}

        /// <summary>
        /// Gets a collection of applications
        /// </summary>
        public Collections.ApplicationCollection Applications
        {
            get 
            { 
                return Instance.Applications;
            }
        }

		public AdminManager(string connectionString)
		{
			ConnectionString = connectionString;
		}

		//internal AdminManager(Interfaces.IAdminManager adminManager)
		//{
		//    Instance = adminManager;
		//    Instance.Groups.Store = this;
		//    Instance.Applications.AdminManager = this;
		//}

		/// <summary>
		/// Removes an application group from the store
		/// </summary>
		/// <param name="group">The group to remove</param>
		public void DeleteGroup(ApplicationGroup group)
		{
			CheckGroupIsValid(group);

			Instance.DeleteGroup(group);
			Groups.RemoveValue(group.Guid);
		}

		public void DeleteGroup(string name)
		{
			ApplicationGroup g = Groups[name];
			if (g == null)
				return;

			DeleteGroup(g);
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
			Groups.CheckName(name);

			ApplicationGroup g = Instance.CreateGroup(name, description, groupType);
			g.Store = this;
			Groups.AddValue(g);

			return g;
		}

		public void UpdateGroup(ApplicationGroup group)
		{
			CheckGroupIsValid(group);

			Groups.CheckName(group);
			Instance.UpdateGroup(group);
			Groups.UpdateValue(group);
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
			Applications.CheckName(name);

			Application a = Instance.CreateApplication(name, description, versionInformation);
			a.Store = this;
			Applications.AddValue(a);

			return a;
		}

		/// <summary>
		/// Removes an application from the store
		/// </summary>
		/// <param name="application">The application to remove</param>
		public void DeleteApplication(Application application)
		{
			CheckApplicationIsValid(application);

			Instance.DeleteApplication(application);
			Applications.RemoveValue(application.Guid);
		}

		public void DeleteApplication(string name)
		{
			Application a = Applications[name];
			if (a == null)
				return;

			DeleteApplication(a);
		}

		public void UpdateApplication(Application application)
		{
			CheckApplicationIsValid(application);

			Applications.CheckName(application);
			Instance.UpdateApplication(application);
			Applications.UpdateValue(application);
		}

		private bool CheckGroupIsValid(ApplicationGroup group)
		{
			if (group == null)
				throw new ArgumentNullException("group");

			if (group.Store == null || group.Store.Guid != this.Guid)
				throw new AzException("The group is not defined in the store, or is not a global group.");

			return true;
		}

		private bool CheckApplicationIsValid(Application application)
		{
			if (application == null)
				throw new ArgumentNullException("application");
			if (application.Store == null || application.Store.Guid != this.Guid)
				throw new AzException("The application is not defined in the store.");

			return true;
		}

		public void Update()
		{
			Instance.Update();
		}

		public void Open()
		{
			if (ConnectionString.StartsWith("msxml"))
			{
				Xml.XmlService f = new Xml.XmlService(ConnectionString.Substring(8));

				Instance = f.GetAdminManager();
			}
			else
				throw new AzException("Unsupported connectionstring specified.");

			Instance.Groups.Store = this;
			Instance.Applications.AdminManager = this;
		}
	}
}
