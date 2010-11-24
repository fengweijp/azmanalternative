using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Interfaces
{
	/// <summary>
	/// A user
	/// </summary>
	public interface IMember
	{
		string Sid { get; set; }
		bool IsExclusion { get; set; }
		Guid Parent { get; set; }

		void Save();
		void Remove();
	}
}
