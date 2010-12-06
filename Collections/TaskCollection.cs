using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public class TaskCollection : CollectionBase<Task>
	{

		internal TaskCollection(ServiceBase service, Dictionary<string, string> values, bool linked)
			: base(service, values, linked)
		{ }

		internal TaskCollection(ServiceBase service, bool linked)
			: this(service, new Dictionary<string, string>(), linked)
		{ }

		public override IEnumerator<Task> GetEnumerator()
		{
			return Service.GetTasks(Guids.Values, Application);
		}

		internal override void CheckName(Task entry)
		{
			CheckName(entry, "task");
		}

		internal override void CheckName(string name)
		{
			CheckName(name, "task");
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
