using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	public class Operation : Interfaces.IOperation
	{
		private readonly Interfaces.IOperation _Operation;

		public Guid Guid
		{
			get { return _Operation.Guid; }
		}

		public string Name
		{
			get { return _Operation.Name; }
			set { _Operation.Name = value; }
		}

		public string Description
		{
			get { return _Operation.Description; }
			set { _Operation.Description = value; }
		}

		public int OperationId
		{
			get { return _Operation.OperationId; }
			set { _Operation.OperationId = value; }
		}
	}
}
