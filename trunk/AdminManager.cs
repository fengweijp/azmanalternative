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

		/// <summary>
		/// Gets the connection string for the current instance
		/// </summary>
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
			get 
			{
				if (Instance == null)
					throw new AzException("A store has not been opened.");
				return Instance.MajorVersion; 
			}
		}

		/// <summary>
		/// Gets the schema minor version
		/// </summary>
		public int MinorVersion
		{
			get
			{
				if (Instance == null)
					throw new AzException("A store has not been opened.");
				return Instance.MinorVersion;
			}
		}

		/// <summary>
		/// Gets and sets the store description
		/// </summary>
		public string Description
		{
			get
			{
				if (Instance == null)
					throw new AzException("A store has not been opened.");
				return Instance.Description;
			}
			set
			{
				if (Instance == null)
					throw new AzException("A store has not been opened.");
				Instance.Description = value;
			}
		}

		/// <summary>
		/// Gets the object identifier
		/// </summary>
		public string Key
		{
			get
			{
				if (Instance == null)
					throw new AzException("A store has not been opened.");
				return Instance.Key;
			}
		}

		/// <summary>
		/// Gets a collection of application groups
		/// </summary>
		public Collections.ApplicationGroupCollection Groups
		{
			get
			{
				if (Instance == null)
					throw new AzException("A store has not been opened.");
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
				if (Instance == null)
					throw new AzException("A store has not been opened.");
 
				return Instance.Applications;
			}
		}

		/// <summary>
		/// Initialises a new instance of the Admin Manager
		/// </summary>
		/// <param name="connectionString">connection string to use with this instance</param>
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
		/// <param name="name">The name of the group to delete.</param>
		public void DeleteGroup(string name)
		{
			ApplicationGroup g = Groups[name];
			if (g == null)
				return;

			g.Delete();
		}

		/// <summary>
		/// Add an application group to the store
		/// </summary>
		/// <param name="name">group name. Must not be null or empty</param>
		/// <param name="description">Group description</param>
		/// <param name="groupType">The type of group</param>
		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			if (Instance == null)
				throw new AzException("A store has not been opened.");

			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name", "Name cannot be blank when adding a group.");
			Groups.CheckName(name);

			ApplicationGroup g = Locator.Factory.CreateGroup(null, name, description, groupType, true);
			g.Store = this;
			Groups.AddValue(g);

			return g;
		}

		/// <summary>
		/// Add an application to the store
		/// </summary>
		/// <param name="name">The application name. Must not be null or empty</param>
		/// <param name="description">Application description</param>
		/// <param name="versionInformation">Version information</param>
		public Application CreateApplication(string name, string description, string versionInformation)
		{
			if (Instance == null)
				throw new AzException("A store has not been opened.");

			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name", "A name must be specified when adding an application");
			Applications.CheckName(name);

			Application a = Locator.Factory.CreateApplication(Key, name, description, versionInformation);
			a.Store = this;
			Applications.AddValue(a);

			return a;
		}

		/// <summary>
		/// Removes an application from the store
		/// </summary>
		/// <param name="name">Name of the application to delete</param>
		public void DeleteApplication(string name)
		{
			Application a = Applications[name];
			if (a == null)
				return;

			a.Delete();
		}

		private bool CheckGroupIsValid(ApplicationGroup group)
		{
			if (group == null)
				throw new ArgumentNullException("group");

			if (group.Store == null || group.Store.Key != this.Key)
				throw new AzException("The group is not defined in the store, or is not a global group.");

			return true;
		}

		/// <summary>
		/// Saves changes to the store. Only changes to the store are saved.
		/// </summary>
		public void Update()
		{
			if (Instance == null)
				throw new AzException("A store has not been opened.");

			Instance.Update();
		}

		/// <summary>
		/// Open the connection to the store
		/// </summary>
		public void Open()
		{
			if (Instance != null)
				throw new AzException("The store is already open.");

			if (ConnectionString.StartsWith("msxml"))
			{
				Xml.XmlService f = new Xml.XmlService(ConnectionString.Substring(8));
				Locator.SetService(f, new Xml.XmlFactory(f));

				Instance = f.GetAdminManager();
			}
			else if (ConnectionString.StartsWith("msldap"))
			{
				ActiveDirectory.AdService s = new ActiveDirectory.AdService(ConnectionString.Substring(9));
				Instance = s.GetAdminManager();
			}
			else
				throw new AzException("Unsupported connectionstring specified.");

			Instance.Groups.Store = this;
			Instance.Applications.AdminManager = this;
		}
	}
}
