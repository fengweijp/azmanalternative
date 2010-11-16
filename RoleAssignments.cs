using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	public class RoleAssignments: ContainerBase, Interfaces.IRoleAssignment
	{
		internal readonly Interfaces.IRoleAssignment Instance;

		public override Application Parent
		{
			get { return BaseApplication; }
			internal set
			{
				BaseApplication = value;
				
			}
		}

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

		public RoleDefinition Definition
		{
			get { return Instance.Definition; }
		}

		internal RoleAssignments(Interfaces.IRoleAssignment role)
			: base()
		{
			Instance = role;
		}

		internal RoleAssignments(Interfaces.IRoleAssignment role, Application parent)
			: this(role)
		{
			Parent = parent;
		}

		/// <summary>
		/// Gets the collection of groups in the role
		/// </summary>
		public Collections.ApplicationGroupCollection Groups
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

			Instance.AddGroup(group);
		}

		/// <summary>
		/// Removes a group from the role
		/// </summary>
		/// <param name="group">group to remove</param>
		public void RemoveGroup(ApplicationGroup group)
		{
			CheckObjectIsValid(group);

			Instance.RemoveGroup(group);
		}

		protected override bool CheckObjectIsValid(ContainerBase o)
		{
			ApplicationGroup g = (ApplicationGroup)o;

			if (g.IsGlobalGroup)
			{
				if (g.Store.Guid != this.Parent.Store.Guid)
					throw new AzException("The group is not defined in the current store.");
				return true;
			}
			else
				return base.CheckObjectIsValid(o);
		}
	}
}
