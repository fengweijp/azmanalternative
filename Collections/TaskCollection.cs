using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public class TaskCollection : CollectionBase<Task>
	{

		internal TaskCollection(ServiceBase service, Dictionary<string, Guid> values)
			: base(service, values)
		{
			ItemLoader = Service.GetTask;
		}

		internal TaskCollection(ServiceBase service)
			: this(service, new Dictionary<string, Guid>())
		{ }

		public override IEnumerator<Task> GetEnumerator()
		{
			return Service.GetTasks(Guids.Values, Application);
		}
	}
}
