using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
    /// <summary>
    /// An operation
    /// </summary>
	public class Operation : BaseObject, Interfaces.IOperation
	{
		private readonly Interfaces.IOperation _Operation;

        /// <summary>
        /// Gets the operation identifier
        /// </summary>
		public Guid Guid
		{
			get { return _Operation.Guid; }
		}

        /// <summary>
        /// Gets or sets the operation name
        /// </summary>
		public string Name
		{
			get { return _Operation.Name; }
			set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Name");

                _Operation.Name = value; 
            }
		}

        /// <summary>
        /// Gets or sets the operation description
        /// </summary>
		public string Description
		{
			get { return _Operation.Description; }
			set { _Operation.Description = value; }
		}

        /// <summary>
        /// Gets or sets the operation ID. Must be unique in the application.
        /// </summary>
		public int OperationId
		{
			get { return _Operation.OperationId; }
			set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("OperationId");
                _Operation.OperationId = value; 
            }
		}

        internal Operation(Interfaces.IOperation operation)
        {
            _Operation = operation;
        }
	}
}
