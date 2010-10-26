using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Interfaces
{
	/// <summary>
	/// A group of users
	/// </summary>
	public interface IApplicationGroup
	{
		/// <summary>
		/// Gets the group identifier
		/// </summary>
		Guid Guid { get; }
		/// <summary>
		/// Gets or sets the group name
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the group description
		/// </summary>
		string Description { get; set; }
		/// <summary>
		/// Gets the type of group
		/// </summary>
		GroupType GroupType { get; }
		/// <summary>
		/// Gets the collection of members
		/// </summary>
		List<IMember> Members { get; }

		void AddMember(IMember member);
		void RemoveMember(IMember member);
	}
}
