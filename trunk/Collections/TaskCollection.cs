using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	/// <summary>
	/// A collection of tasks
	/// </summary>
	public class TaskCollection : CollectionBase<Task>
	{
		protected override string ErrorObjectName
		{
			get { return "task"; }
		}

		internal TaskCollection(ServiceBase service, Dictionary<string, string> values, bool isChildList)
			: base(service, values, isChildList)
		{ }

		internal TaskCollection(ServiceBase service, bool isChildList)
			: this(service, new Dictionary<string, string>(), isChildList)
		{ }

		public override IEnumerator<Task> GetEnumerator()
		{
			return Service.GetTasks(Guids.Values, Application);
		}

		protected override ContainerBase LinkedItemLoader(string name)
		{
			return Application.Tasks[name];
		}

		protected override Task ItemLoader(string uniqueName)
		{
			return Service.GetTask(uniqueName);
		}
	}
}
