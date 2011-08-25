using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	/// <summary>
	/// A collection of coperations
	/// </summary>
	public class OperationCollection : CollectionBase<Operation>
	{
		protected override string ErrorObjectName
		{
			get { return "operation"; }
		}

		internal OperationCollection(ServiceBase service, Dictionary<string, string> values, bool isChildList)
			: base(service, values, isChildList)
		{ }

		internal OperationCollection(ServiceBase service, bool isChildList)
			: this(service, new Dictionary<string, string>(), isChildList)
		{ }

		public override IEnumerator<Operation> GetEnumerator()
		{
			return Service.GetOperations(Guids.Values, Application);
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
