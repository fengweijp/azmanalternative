using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public class OperationCollection : CollectionBase<Operation>
	{

		internal OperationCollection(ServiceBase service, Dictionary<string, string> values, bool linked)
			: base(service, values, linked)
		{ }

		internal OperationCollection(ServiceBase service, bool linked)
			: this(service, new Dictionary<string, string>(), linked)
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

		internal void CheckId(Operation operation)
		{
			foreach (var item in this)
			{
				if (item.OperationId == operation.OperationId && item.Key != operation.Key)
					throw new AzException("Operation ID is already in use.");
			}
		}

		protected override ContainerBase LinkedItemLoader(string name)
		{
			return Application.Operations[name];
		}

		protected override Operation ItemLoader(string uniqueName)
		{
			return Service.GetOperation(uniqueName);
		}
	}
}
