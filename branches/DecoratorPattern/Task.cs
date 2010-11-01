using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
    /// <summary>
    /// A task
    /// </summary>
	public class Task : BaseObject, Interfaces.ITask
	{
		private readonly Interfaces.ITask _Task;

        /// <summary>
        /// Gets the task identifier
        /// </summary>
		public Guid Guid
		{
			get { return _Task.Guid; }
		}

        /// <summary>
        /// Gets or sets the task name
        /// </summary>
		public string Name
		{
			get { return _Task.Name; }
			set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Name");
                _Task.Name = value;
            }
		}

        /// <summary>
        /// Gets or sets the task description
        /// </summary>
		public string Description
		{
			get { return _Task.Description; }
			set { _Task.Description = value; }
		}

        /// <summary>
        /// Gets or sets the path to the biz rule script
        /// </summary>
		public string BizRuleImportedPath
		{
			get { return _Task.BizRuleImportedPath; }
			set { _Task.BizRuleImportedPath = value; }
		}

        /// <summary>
        /// Gets a collection of tasks directly added to this task
        /// </summary>
		public System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks
		{
			get { return _Task.Tasks; }
		}

        /// <summary>
        /// Gets a collection of operations directly added to this task
        /// </summary>
		public System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations
		{
			get { return _Task.Operations; }
		}

        internal Task(Interfaces.ITask task)
        {
            _Task = task;
        }

        /// <summary>
        /// Adds a task to this task
        /// </summary>
        /// <param name="task">Task to add</param>
		public void AddTask(Task task)
		{
            CheckObjectIsValid(task);
            _Task.AddTask(task);
		}

        /// <summary>
        /// Removes a task from this task
        /// </summary>
        /// <param name="task">Task to remove</param>
		public void RemoveTask(Task task)
		{
            CheckObjectIsValid(task);
            _Task.RemoveTask(task);
		}

        /// <summary>
        /// Adds an operation to this task
        /// </summary>
        /// <param name="operation">Operation to add</param>
		public void AddOperation(Operation operation)
		{
            CheckObjectIsValid(operation);
            _Task.AddOperation(operation);
		}

        /// <summary>
        /// Removes an operation from this task
        /// </summary>
        /// <param name="operation">Operation to remove</param>
		public void RemoveOperation(Operation operation)
		{
            CheckObjectIsValid(operation);
            _Task.RemoveOperation(operation);
		}
	}
}
