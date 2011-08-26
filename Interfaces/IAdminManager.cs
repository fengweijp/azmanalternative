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
		string Key { get; }
		/// <summary>
		/// Gets a collection of application groups
		/// </summary>
		Collections.ApplicationGroupCollection Groups { get; }
		/// <summary>
		/// Gets a collection of applications
		/// </summary>
		Collections.ApplicationCollection Applications { get; }

		/// <summary>
		/// Saves changes to this class
		/// </summary>
		void Update();
	}
}
