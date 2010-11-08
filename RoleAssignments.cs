using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	public class RoleAssignments: ContainerBase, Interfaces.IRoleAssignment
	{
		internal readonly Interfaces.IRoleAssignment Instance;

		/// <summary>
		/// Gets the role identifier
		/// </summary>
		public override Guid Guid
		{
			get { return Instance.Guid; }
		}

		/// <summary>
		/// Gets or sets the role name
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
		/// Gets or sets the role description
		/// </summary>
		public string Description
		{
			get { return Instance.Description; }
			set { Instance.Description = value; }
		}

		public Interfaces.IRoleDefinition Definition
		{
			get { throw new NotImplementedException(); }
		}

		internal RoleAssignments(Interfaces.IRoleAssignment role)
			: base()
		{
			Instance = role;
		}

		/// <summary>
		/// Gets the collection of groups in the role
		/// </summary>
		public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		{
			get { return Instance.Groups; }
		}

		/// <summary>
		/// Gets the collection of members assigned to the role
		/// </summary>
		public System.Collections.ObjectModel.ReadOnlyCollection<Interfaces.IMember> Members
		{
			get { return Instance.Members; }
		}

		/// <summary>
		/// Adds a group to the role
		/// </summary>
		/// <param name="group">Group to add</param>
		public void AddGroup(ApplicationGroup group)
		{
			CheckObjectIsValid(group);

			if (group.Store != null && group.Store.Guid != this.Application.Store.Guid)
				throw new AzException("The group is not defined in the current store.");

			Instance.AddGroup(group);
		}

		/// <summary>
		/// Removes a group from the role
		/// </summary>
		/// <param name="group">group to remove</param>
		public void RemoveGroup(ApplicationGroup group)
		{
			CheckObjectIsValid(group);

			if (group.Store != null && group.Store.Guid != this.Application.Store.Guid)
				throw new AzException("The group is not defined in the current store.");

			Instance.RemoveGroup(group);
		}
	}
}
