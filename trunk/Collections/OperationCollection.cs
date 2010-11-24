using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public class OperationCollection : CollectionBase<Operation>
	{

		internal OperationCollection(ServiceBase service, Dictionary<string, Guid> values)
			: base(service, values)
		{
			ItemLoader = service.GetOperation;
		}

		internal OperationCollection(ServiceBase service)
			: this(service, new Dictionary<string, Guid>())
		{ }

		public override IEnumerator<Operation> GetEnumerator()
		{
			return Service.GetOperations(Guids.Values, Application);
		}

		internal override void CheckName(Operation entry)
		{
			CheckName(entry, "operation");
		}

		internal override void CheckName(string name)
		{
			CheckName(name, "operation");
		}

		internal void CheckId(int id)
		{
			foreach (var item in this)
			{
				if (item.OperationId == id)
					throw new AzException("Operation ID is already in use.");
			}
		}
	}
}
