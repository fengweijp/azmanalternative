using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	/// <summary>
	/// A task
	/// </summary>
	public class Task : ContainerBase, Interfaces.ITask
	{
		internal readonly Interfaces.ITask Instance;

		/// <summary>
		/// Gets the owning application for this group, if defined
		/// </summary>
		public override Application Parent
		{
			get { return BaseApplication; }
			internal set
			{
				BaseApplication = value;
				Operations.Application = Parent;
				Tasks.Application = Parent;
			}
		}

		/// <summary>
		/// Gets the task identifier
		/// </summary>
		public override string Key
		{
			get { return Instance.Key; }
		}

		/// <summary>
		/// Gets or sets the task name
		/// </summary>
		public override string Name
		{
			get { return Instance.Name; }
			set 
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("Name");
				Instance.Name = value;
			}
		}

		/// <summary>
		/// Gets or sets the task description
		/// </summary>
		public string Description
		{
			get { return Instance.Description; }
			set { Instance.Description = value; }
		}

		/// <summary>
		/// Gets or sets the path to the biz rule script
		/// </summary>
		public string BizRuleImportedPath
		{
			get { return Instance.BizRuleImportedPath; }
		}

		/// <summary>
		/// Gets the biz rule language for the rule, if defined. Returns <c>Undefined</c> if a rule is not set
		/// </summary>
		public BizRuleLanguage BizRuleLanguage
		{
			get { return Instance.BizRuleLanguage; }
		}

		/// <summary>
		/// Gets the biz rule body
		/// </summary>
		public string BizRule
		{
			get { return Instance.BizRule; }
		}

		/// <summary>
		/// Gets a collection of tasks directly added to this task
		/// </summary>
		public Collections.TaskCollection Tasks
		{
			get { return Instance.Tasks; }
		}

		/// <summary>
		/// Gets a collection of operations directly added to this task
		/// </summary>
		public Collections.OperationCollection Operations
		{
			get { return Instance.Operations; }
		}

		internal Task(Interfaces.ITask task)
		{
			Instance = task;
		}

		internal Task(Interfaces.ITask task, Application parent)
			: this(task)
		{
			Parent = parent;
		}

		/// <summary>
		/// Adds a task to the Tasks collection
		/// </summary>
		/// <param name="task">Task to add</param>
		public void AddTask(Task task)
		{
			if (task == null)
				throw new ArgumentNullException("task");

			CheckObjectIsValid(task);
			if (Tasks.ContainsKey(task.Key))
				return;

			Instance.AddTask(task);
			Tasks.AddValue(task);
		}

		/// <summary>
		/// Adds a task to the Tasks collection
		/// </summary>
		/// <param name="name">Name of the task to add</param>
		public void AddTask(string name)
		{
			Task t = Parent.Tasks[name];
			AddTask(t);
		}

		/// <summary>
		/// Removes a task from the Tasks collection
		/// </summary>
		/// <param name="task">Task to remove</param>
		public void RemoveTask(Task task)
		{
			if (task == null)
				throw new ArgumentNullException("task");

			CheckObjectIsValid(task);
			if (!Tasks.ContainsKey(task.Key))
				return;

			Instance.RemoveTask(task);
			Tasks.RemoveValue(task.Key);
		}

		/// <summary>
		/// Removes a task from the Tasks collection
		/// </summary>
		/// <param name="name">Name of the task to remove</param>
		public void RemoveTask(string name)
		{
			Task t = Tasks[name];
			RemoveTask(t);
		}

		/// <summary>
		/// Adds an operation to the Operations collection
		/// </summary>
		/// <param name="operation">Operation to add</param>
		public void AddOperation(Operation operation)
		{
			if (operation == null)
				throw new ArgumentNullException("operation");

			CheckObjectIsValid(operation);
			if (Operations.ContainsKey(operation.Key))
				return;

			Instance.AddOperation(operation);
			Operations.AddValue(operation);
		}

		/// <summary>
		/// Adds an operation to the Operations collection
		/// </summary>
		/// <param name="name">Name of the operation to add</param>
		public void AddOperation(string name)
		{
			Operation o = Parent.Operations[name];
			AddOperation(o);
		}

		/// <summary>
		/// Removes an operation from the Operations collection
		/// </summary>
		/// <param name="operation">Operation to remove</param>
		public void RemoveOperation(Operation operation)
		{
			if (operation == null)
				throw new ArgumentNullException("operation");

			CheckObjectIsValid(operation);
			if (!Operations.ContainsKey(operation.Key))
				return;

			Instance.RemoveOperation(operation);
			Operations.RemoveValue(operation.Key);
		}

		/// <summary>
		/// Removes an operation from the Operations collection
		/// </summary>
		/// <param name="name">Name of the operation to remove</param>
		public void RemoveOperation(string name)
		{
			Operation o = Operations[name];
			RemoveOperation(o);
		}

		/// <summary>
		/// Loads a biz rule from a file
		/// </summary>
		/// <param name="path">path of the script to load</param>
		/// <param name="language">language to use for the rule</param>
		public void LoadBizRuleScript(string path, BizRuleLanguage language)
		{
			if (language == AzAlternative.BizRuleLanguage.Undefined)
				throw new AzException("Undefined cannot be set for the script language.");

			Instance.LoadBizRuleScript(path, language);
		}

		/// <summary>
		/// Clears the biz rule
		/// </summary>
		public void ClearBizRuleScript()
		{
			Instance.ClearBizRuleScript();
		}

	}
}
