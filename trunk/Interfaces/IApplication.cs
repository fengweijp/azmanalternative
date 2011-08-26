using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Interfaces
{
	/// <summary>
	/// An application
	/// </summary>
	public interface IApplication
	{
		/// <summary>
		/// Gets the application identifier
		/// </summary>
		string Key { get; }
		/// <summary>
		/// Gets or sets the application name
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the description
		/// </summary>
		string Description { get; set; }
		/// <summary>
		/// Gets or sets the application version
		/// </summary>
		string ApplicationVersion { get; set; }
		/// <summary>
		/// Gets the collection of roles defined in the application
		/// </summary>
		Collections.RoleDefinitionCollection Roles { get; }
		Collections.RoleAssignmentsCollection RoleAssignments { get; }
		/// <summary>
		/// Gets the collection of groups defined in the application
		/// </summary>
		Collections.ApplicationGroupCollection Groups { get; }
		/// <summary>
		/// Gets the collection of operations defined in the application
		/// </summary>
		Collections.OperationCollection Operations { get; }
		/// <summary>
		/// Gets the collection of tasks defined in the application
		/// </summary>
		Collections.TaskCollection Tasks { get; }
	}
}
