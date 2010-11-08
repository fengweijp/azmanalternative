using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Interfaces
{
	/// <summary>
	/// Represents the top level Authorisation store
	/// </summary>
	public interface IAdminManager
	{
		/// <summary>
		/// Gets the schema major version
		/// </summary>
		int MajorVersion { get; }
		/// <summary>
		/// Gets the schema minor version
		/// </summary>
		int MinorVersion { get; }
		/// <summary>
		/// Gets and sets the store description
		/// </summary>
		string Description { get; set; }
		/// <summary>
		/// Gets the object identifier
		/// </summary>
		Guid Guid { get; }
		/// <summary>
		/// Gets a collection of application groups
		/// </summary>
		System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups { get; }
		/// <summary>
		/// Gets a collection of applications
		/// </summary>
		//System.Collections.ObjectModel.ReadOnlyCollection<Application> Applications { get; }

		/// <summary>
		/// Add an application group to the store
		/// </summary>
		/// <param name="name">group name. Must not be null or empty</param>
		/// <param name="description">Group description</param>
		/// <param name="groupType">The type of group</param>
		ApplicationGroup CreateGroup(string name, string description, GroupType groupType);
		/// <summary>
		/// Removes an application group from the store
		/// </summary>
		/// <param name="group">The group to remove</param>
		void DeleteGroup(ApplicationGroup group);
		void UpdateGroup(ApplicationGroup group);

		/// <summary>
		/// Add an application to the store
		/// </summary>
		/// <param name="name">The application name. Must not be null or empty</param>
		/// <param name="description">Application description</param>
		/// <param name="versionInformation">Version information</param>
		Application CreateApplication(string name, string description, string versionInformation);
		/// <summary>
		/// Removes an application from the store
		/// </summary>
		/// <param name="application">The application to remove</param>
		void DeleteApplication(Application application);
		void UpdateApplication(Application application);

		void Update();
	}
}
