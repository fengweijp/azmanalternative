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
		string Key { get; }
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
		string BizRuleImportedPath { get; }
		/// <summary>
		/// Gets or sets the rule script language
		/// </summary>
		BizRuleLanguage BizRuleLanguage { get; }
		/// <summary>
		/// Gets the rule script
		/// </summary>
		string BizRule { get; }
		/// <summary>
		/// Gets a collection of tasks assigned to the task
		/// </summary>
		Collections.TaskCollection Tasks { get; }
		/// <summary>
		/// Gets a collection of operations assigned to the task
		/// </summary>
		Collections.OperationCollection Operations { get; }

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

		/// <summary>
		/// Adds a biz rule to the task
		/// </summary>
		/// <param name="path">path to the file to load</param>
		/// <param name="language">language the bizrule script is written in</param>
		void LoadBizRuleScript(string path, BizRuleLanguage language);
		/// <summary>
		/// Removes the biz rule from the task
		/// </summary>
		void ClearBizRuleScript();
	}
}
