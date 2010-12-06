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
		string UniqueName { get; }
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
		/// Gets whether the group is a global group or an application group
		/// </summary>
		bool IsGlobalGroup { get; }
		string LdapQuery { get; set; }
		/// <summary>
		/// Gets the collection of members
		/// </summary>
		Collections.MemberCollection Members { get; }
		Collections.MemberCollection Exclusions { get; }
        /// <summary>
        /// Gets a collection of groups in this group
        /// </summary>
        Collections.ApplicationGroupCollection Groups { get; }

        /// <summary>
        /// Adds a member to this group
        /// </summary>
        /// <param name="member">Member to add</param>
		void AddMember(string name);
        /// <summary>
        /// Removes a member from this group
        /// </summary>
        /// <param name="member">Member to remove</param>
		void RemoveMember(string member);

        /// <summary>
        /// Adds a group to this group
        /// </summary>
        /// <param name="group">Group to add</param>
        void AddGroup(ApplicationGroup group);
        /// <summary>
        /// Removes a group from this group
        /// </summary>
        /// <param name="group">Group to remove</param>
        void RemoveGroup(ApplicationGroup group);
	}
}
