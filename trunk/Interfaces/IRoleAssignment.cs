using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Interfaces
{
	/// <summary>
	/// A role
	/// </summary>
	public interface IRoleAssignment
	{
		/// <summary>
		/// Gets the role identifier
		/// </summary>
		string UniqueName { get; }
		/// <summary>
		/// Gets or sets the role name
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the role description
		/// </summary>
		string Description { get; set; }
		RoleDefinition Definition { get; }
		/// <summary>
		/// Gets the collection of groups assigned to the role
		/// </summary>
		Collections.ApplicationGroupCollection Groups { get; }
		/// <summary>
		/// Gets the collection of users assigned to the role
		/// </summary>
		Collections.MemberCollection Members { get; }

		void AddGroup(ApplicationGroup group);
		void RemoveGroup(ApplicationGroup group);

		void AddMember(string name);
		void RemoveMember(string name);
	}
}
