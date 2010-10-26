using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Interfaces
{
	/// <summary>
	/// A task
	/// </summary>
	public interface ITask
	{
		/// <summary>
		/// Gets the task identifier
		/// </summary>
		Guid Guid { get; }
		/// <summary>
		/// Gets or sets the task name
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the task description
		/// </summary>
		string Description { get; set; }
		/// <summary>
		/// Gets or sets the rule script path
		/// </summary>
		string BizRuleImportedPath { get; set; }
		/// <summary>
		/// Gets a collection of tasks assigned to the task
		/// </summary>
		System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks { get; }
		/// <summary>
		/// Gets a collection of operations assigned to the task
		/// </summary>
		System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations { get; }

		void AddTask(Task task);
		void RemoveTask(Task task);

		void AddOperation(Operation operation);
		void RemoveOperation(Operation operation);
	}
}
