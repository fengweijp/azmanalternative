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
        /// Gets or sets the rule script language
        /// </summary>
        BizRuleLanguage BizRuleLanguage { get; set; }
        /// <summary>
        /// Gets the rule script
        /// </summary>
        string BizRule { get; }
        /// <summary>
		/// Gets a collection of tasks assigned to the task
		/// </summary>
		System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks { get; }
		/// <summary>
		/// Gets a collection of operations assigned to the task
		/// </summary>
		System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations { get; }

        /// <summary>
        /// Adds a task to this task
        /// </summary>
        /// <param name="task">Task to add</param>
		void AddTask(Task task);
        /// <summary>
        /// Removes a task from this task
        /// </summary>
        /// <param name="task">Task to remove</param>
		void RemoveTask(Task task);

        /// <summary>
        /// Adds an operation to this task
        /// </summary>
        /// <param name="operation">Operation to add</param>
		void AddOperation(Operation operation);
        /// <summary>
        /// Removes an operation from this task
        /// </summary>
        /// <param name="operation">Operation to remove</param>
		void RemoveOperation(Operation operation);
	}
}
