using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Interfaces
{
	/// <summary>
	/// An operation
	/// </summary>
	public interface IOperation
	{
		/// <summary>
		/// Gets the operation identifier
		/// </summary>
		Guid Guid { get; }
		/// <summary>
		/// Gets or sets the operation name
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the operation description
		/// </summary>
		string Description { get; set; }
		/// <summary>
		/// Gets or sets the operation ID
		/// </summary>
		int OperationId { get; set; }
	}
}
