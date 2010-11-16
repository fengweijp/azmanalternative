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
		public override Guid Guid
		{
			get { return Instance.Guid; }
		}

        /// <summary>
        /// Gets or sets the task name
        /// </summary>
		public string Name
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

		public BizRuleLanguage BizRuleLanguage
		{
			get { return Instance.BizRuleLanguage; }
		}

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
        /// Adds a task to this task
        /// </summary>
        /// <param name="task">Task to add</param>
		public void AddTask(Task task)
		{
			CheckObjectIsValid(task);
			//TODO: Check task isn't already in Task
			Instance.AddTask(task);
		}

        /// <summary>
        /// Removes a task from this task
        /// </summary>
        /// <param name="task">Task to remove</param>
		public void RemoveTask(Task task)
		{
			CheckObjectIsValid(task);
			Instance.RemoveTask(task);
		}

        /// <summary>
        /// Adds an operation to this task
        /// </summary>
        /// <param name="operation">Operation to add</param>
		public void AddOperation(Operation operation)
		{
			CheckObjectIsValid(operation);
			//TODO: Check op isn't in already
			Instance.AddOperation(operation);
		}

        /// <summary>
        /// Removes an operation from this task
        /// </summary>
        /// <param name="operation">Operation to remove</param>
		public void RemoveOperation(Operation operation)
		{
			CheckObjectIsValid(operation);
			Instance.RemoveOperation(operation);
		}

		public void LoadBizRuleScript(string path, BizRuleLanguage language)
		{
			if (language == AzAlternative.BizRuleLanguage.Undefined)
				throw new AzException("Undefined cannot be set for the script language.");

			Instance.LoadBizRuleScript(path, language);
		}

		public void ClearBizRuleScript()
		{
			Instance.ClearBizRuleScript();
		}

	}
}
