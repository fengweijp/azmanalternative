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
		Guid Guid { get; }
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
		///// <summary>
		///// Gets the collection of roles defined in the application
		///// </summary>
		//System.Collections.ObjectModel.ReadOnlyCollection<Role> Roles { get; }
		///// <summary>
		///// Gets the collection of groups defined in the application
		///// </summary>
		//System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups { get; }
		///// <summary>
		///// Gets the collection of operations defined in the application
		///// </summary>
		//System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations { get; }
		///// <summary>
		///// Gets the collection of tasks defined in the application
		///// </summary>
		//System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks { get; }

		//ApplicationGroup CreateGroup(string name, string description, GroupType groupType);
		//void DeleteGroup(ApplicationGroup group);

		//Role CreateRole(string name, string description);
		//void DeleteRole(Role role);

		//Operation CreateOperation(string name, string description, int operationId);
		//void DeleteOperation(Operation operation);

		//Task CreateTask(string name, string description);
		//void DeleteTask(Task task);
	}
}
