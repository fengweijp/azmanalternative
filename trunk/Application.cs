using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	/// <summary>
	/// An application in the store
	/// </summary>
	public class Application : ContainerBase, Interfaces.IApplication
	{
		internal readonly Interfaces.IApplication Instance;

		public override Application Parent
		{
			get { return BaseApplication; }
			internal set
			{
				BaseApplication = value;
				Groups.Application = value;
				Operations.Application = value;
				Tasks.Application = value;
			}
		}

		/// <summary>
        /// Gets the application identifier
        /// </summary>
        public override Guid Guid
		{
			get { return Instance.Guid; }
		}

        /// <summary>
        /// Gets or sets the application name
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
        /// Gets or sets the description
        /// </summary>
        public string Description
		{
			get { return Instance.Description; }
			set { Instance.Description = value; }
		}

        /// <summary>
        /// Gets or sets the application version
        /// </summary>
        public string ApplicationVersion
		{
			get { return Instance.ApplicationVersion; }
			set { Instance.ApplicationVersion = value; }
		}

		/// <summary>
		/// Gets the collection of roles defined in the application
		/// </summary>
		public Collections.RoleDefinitionCollection Roles
		{
			get { return Instance.Roles; }
		}

		public Collections.RoleAssignmentsCollection RoleAssignments
		{
			get { return Instance.RoleAssignments; }
		}

		/// <summary>
		/// Gets the collection of groups defined in the application
		/// </summary>
		public Collections.ApplicationGroupCollection Groups
		{
			get { return Instance.Groups; }
		}

		/// <summary>
		/// Gets the collection of operations defined in the application
		/// </summary>
		public Collections.OperationCollection Operations
		{
			get { return Instance.Operations; }
		}

		/// <summary>
		/// Gets the collection of tasks defined in the application
		/// </summary>
		public Collections.TaskCollection Tasks
		{
			get { return Instance.Tasks; }
		}

        public AdminManager Store
        {
            get;
            internal set;
        }

		internal Application(Interfaces.IApplication application)
		{
			Instance = application;
		}

		internal Application(Interfaces.IApplication application, AdminManager parent)
			: this(application)
		{
			Store = parent;
		}

		/// <summary>
		/// Create a new group in the application
		/// </summary>
		/// <param name="name">name of the group</param>
		/// <param name="description">description of the group</param>
		/// <param name="groupType">type of group</param>
		/// <returns>group in the current application</returns>
		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if (Groups.ContainsName(name))
				throw new AzException("The group name is already in use.");

			ApplicationGroup g = Instance.CreateGroup(name, description, groupType);
			g.Parent = this;
			Groups.AddValue(g.Guid, g.Name);

			return g;
		}

		/// <summary>
		/// Delete a group defined in the application
		/// </summary>
		/// <param name="group">group to delete</param>
		public void DeleteGroup(ApplicationGroup group)
		{
			if (!CheckObjectIsValid(group))
				throw new AzException("The group is not part of this application. Only application groups can be removed here.");

			Instance.DeleteGroup(group);
			Groups.RemoveValue(group.Guid);
		}

		public void UpdateGroup(ApplicationGroup group)
		{
			if (!CheckObjectIsValid(group))
				throw new AzException("The group is not part of this application. Only application groups can be updated here.");
			if (Groups.ContainsName(group.Name) && Groups[group.Name].Guid != group.Guid)
				throw new AzException("The group name is already in use by another group.");

			Instance.UpdateGroup(group);
			Groups.UpdateValue(group.Guid, group.Name);
		}

		/// <summary>
		/// Create a new role in the application
		/// </summary>
		/// <param name="name">Required name of the role</param>
		/// <param name="description">Description of the role</param>
		/// <returns>new role</returns>
		public RoleDefinition CreateRole(string name, string description)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if (Roles.ContainsName(name))
				throw new AzException("The role name is already in use by another role");

			RoleDefinition r = Instance.CreateRole(name, description);
			r.Parent = this;
			Roles.AddValue(r.Guid, r.Name);

			return r;
		}

		/// <summary>
		/// Deletes a role from the application
		/// </summary>
		/// <param name="role"></param>
		public void DeleteRole(RoleDefinition role)
		{
			if (!CheckObjectIsValid(role))
				throw new AzException("The Role is not part of the application.");

			Instance.DeleteRole(role);
			Roles.RemoveValue(role.Guid);
		}

		public void UpdateRole(RoleDefinition role)
		{
			if (!CheckObjectIsValid(role))
				throw new AzException("The Role is not part of the application.");

			Instance.UpdateRole(role);
			Roles.UpdateValue(role.Guid, role.Name);
		}

		public RoleAssignments CreateRoleAssignments(string name, string description, RoleDefinition role)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if (role == null)
				throw new ArgumentNullException("role");

			if (!CheckObjectIsValid(role))
				throw new AzException("The role is not defined in this application");

			return Instance.CreateRoleAssignments(name, description, role);
		}

		public void DeleteRoleAssignments(RoleAssignments role)
		{
			if (!CheckObjectIsValid(role))
				throw new AzException("The Role is not part of the application.");

			Instance.DeleteRoleAssignments(role);
		}

		public void UpdateRoleAssignments(RoleAssignments role)
		{
			if (!CheckObjectIsValid(role))
				throw new AzException("The Role is not part of the application.");

			Instance.UpdateRoleAssignments(role);
		}

		/// <summary>
		/// Create a new operation in the application
		/// </summary>
		/// <param name="name">Required operation name</param>
		/// <param name="description">Operaton description</param>
		/// <param name="operationId">Required operaton ID</param>
		/// <returns>new operation</returns>
		public Operation CreateOperation(string name, string description, int operationId)
		{
			if (operationId == 0)
				throw new ArgumentOutOfRangeException("operationId", "Operation ID must be non-zero");
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			Operation o = Instance.CreateOperation(name, description, operationId);
			o.Parent = this;

			return o;
		}

		/// <summary>
		/// Deletes the operation from the application
		/// </summary>
		/// <param name="operation">Operaton to delete</param>
		public void DeleteOperation(Operation operation)
		{
			if (!CheckObjectIsValid(operation))
				throw new AzException("The operation is not part of this application.");

			Instance.DeleteOperation(operation);
		}

		public void UpdateOperation(Operation operation)
		{
			if (!CheckObjectIsValid(operation))
				throw new AzException("The operation is not part of this application.");

			Instance.UpdateOperation(operation);
		}

		/// <summary>
		/// Creates a new task in the application
		/// </summary>
		/// <param name="name">Required name of the task</param>
		/// <param name="description">Description of the task</param>
		/// <returns>new task</returns>
		public Task CreateTask(string name, string description)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			Task t = Instance.CreateTask(name, description);
			t.Parent = this;
			return t;
		}

		/// <summary>
		/// Deletes a task from the application
		/// </summary>
		/// <param name="task">task to delete</param>
		public void DeleteTask(Task task)
		{
			if (!CheckObjectIsValid(task))
				throw new AzException("The task is not part of this application.");

			Instance.DeleteTask(task);
		}
		
		public void UpdateTask(Task task)
		{
			if (!CheckObjectIsValid(task))
				throw new AzException("The task is not part of this application.");

			Instance.UpdateTask(task);
		}


	}
}
