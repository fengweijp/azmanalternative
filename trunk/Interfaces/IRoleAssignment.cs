﻿using System;
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
		Guid Guid { get; }
		/// <summary>
		/// Gets or sets the role name
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the role description
		/// </summary>
		string Description { get; set; }
		IRoleDefinition Definition { get; }
		/// <summary>
		/// Gets the collection of groups assigned to the role
		/// </summary>
		System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups { get; }
		/// <summary>
		/// Gets the collection of users assigned to the role
		/// </summary>
		System.Collections.ObjectModel.ReadOnlyCollection<IMember> Members { get; }
		//System.Collections.ObjectModel.ReadOnlyCollection<Role> Roles { get; }

		void AddGroup(ApplicationGroup group);
		void RemoveGroup(ApplicationGroup group);
	}
}