using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Interfaces
{
	/// <summary>
	/// A scope
	/// </summary>
	public interface IScope
	{
		/// <summary>
		/// Gets the scope identifier
		/// </summary>
		Guid Guid { get; }
		/// <summary>
		/// Gets or sets the scope name
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the scope description
		/// </summary>
		string Description { get; set; }
	}
}
