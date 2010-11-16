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
	}
}
