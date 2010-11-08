using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Interfaces
{
	public interface IRoleDefinition : ITask
	{
		///// <summary>
		///// Gets the role identifier
		///// </summary>
		//Guid Guid { get; }
		///// <summary>
		///// Gets or sets the role name
		///// </summary>
		//string Name { get; set; }
		///// <summary>
		///// Gets or sets the role description
		///// </summary>
		//string Description { get; set; }
		//System.Collections.ObjectModel.ReadOnlyCollection<Role> Roles { get; }
		//System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations { get; }
		//System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks { get; }

		//void AddGroup(ApplicationGroup group);
		//void RemoveGroup(ApplicationGroup group);

		void AddRole(RoleDefinition role);
		void RemoveRole(RoleDefinition role);

		//void AddOperation(Operation operation);
		//void RemoveOperation(Operation operation);

		//void AddTask(Task task);
		//void RemoveTask(Task task);
	}
}
