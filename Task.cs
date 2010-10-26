using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	public class Task : Interfaces.ITask
	{
		private readonly Interfaces.ITask _Task;

		public Guid Guid
		{
			get { return _Task.Guid; }
		}

		public string Name
		{
			get { return _Task.Name; }
			set { _Task.Name = value; }
		}

		public string Description
		{
			get { return _Task.Description; }
			set { _Task.Description = value; }
		}

		public string BizRuleImportedPath
		{
			get { return _Task.BizRuleImportedPath; }
			set { _Task.BizRuleImportedPath = value; }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks
		{
			get { return _Task.Tasks; }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations
		{
			get { return _Task.Operations; }
		}

		public void AddTask(Task task)
		{
			throw new NotImplementedException();
		}

		public void RemoveTask(Task task)
		{
			throw new NotImplementedException();
		}

		public void AddOperation(Operation operation)
		{
			throw new NotImplementedException();
		}

		public void RemoveOperation(Operation operation)
		{
			throw new NotImplementedException();
		}
	}
}
