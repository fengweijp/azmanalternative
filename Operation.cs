using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
    /// <summary>
    /// An operation
    /// </summary>
	public class Operation : ContainerBase, Interfaces.IOperation
	{
		internal readonly Interfaces.IOperation Instance;

        /// <summary>
        /// Gets the operation identifier
        /// </summary>
		public override Guid Guid
		{
			get { return Instance.Guid; }
		}

        /// <summary>
        /// Gets or sets the operation name
        /// </summary>
		public string Name
		{
			get { return Instance.Name; }
			set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Name");

                Instance.Name = value; 
            }
		}

        /// <summary>
        /// Gets or sets the operation description
        /// </summary>
		public string Description
		{
			get { return Instance.Description; }
			set { Instance.Description = value; }
		}

        /// <summary>
        /// Gets or sets the operation ID. Must be unique in the application.
        /// </summary>
		public int OperationId
		{
			get { return Instance.OperationId; }
			set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("OperationId");
                Instance.OperationId = value; 
            }
		}

        internal Operation(Interfaces.IOperation operation)
        {
            Instance = operation;
        }

		internal Operation(Interfaces.IOperation operation, Application parent)
			: this(operation)
		{
			Application = parent;
		}
	}
}
