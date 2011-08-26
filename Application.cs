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
		private readonly Interfaces.IApplication Instance;

		public override Application Parent
		{
			get { return null; }
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
		public override string Key
		{
			get { return Instance.Key; }
		}

		/// <summary>
		/// Gets or sets the application name
		/// </summary>
		public override string Name
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

			Instance.Groups.Application = this;
			Instance.Operations.Application = this;
			Instance.RoleAssignments.Application = this;
			Instance.Roles.Application = this;
			Instance.Tasks.Application = this;
		}

		internal Application(Interfaces.IApplication application, AdminManager parent)
			: this(application)
		{
			Store = parent;
		}

		public void Save()
		{
			Store.Applications.CheckName(this);
			Locator.Factory.UpdateApplication(Instance);
			Store.Applications.UpdateValue(this);
		}

		public void Delete()
		{
			Locator.Factory.DeleteApplication(Instance);
			Store.Applications.RemoveValue(Key);
			IsDeleted = true;
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
			Groups.CheckName(name);

			ApplicationGroup g = Locator.Factory.CreateGroup(Key, name, description, groupType, false);
			g.Parent = this;
			Groups.AddValue(g);

			return g;
		}

		public void DeleteGroup(string name)
		{
			ApplicationGroup g = Groups[name];
			if (g == null)
				return;

			g.Delete();
		}

		//public void UpdateGroup(ApplicationGroup group)
		//{
		//    if (group == null)
		//        throw new ArgumentNullException("group");
		//    if (!CheckObjectIsValid(group))
		//        throw new AzException("The group is not part of this application. Only application groups can be updated here.");

		//    Groups.CheckName(group);
		//    Locator.Factory.UpdateGroup(group);
		//    Groups.UpdateValue(group);
		//}

		/// <summary>
		/// Create a new role in the application
		/// </summary>
		/// <param name="name">Required name of the role</param>
		/// <param name="description">Description of the role</param>
		/// <returns>new role</returns>
		public RoleDefinition CreateRole(string name, string description)
		{
			Roles.CheckName(name);

			RoleDefinition r = Locator.Factory.CreateRole(Key, name, description);
			r.Parent = this;
			Roles.AddValue(r);

			return r;
		}

		/// <summary>
		/// Deletes a role from the application
		/// </summary>
		/// <param name="role"></param>
		public void DeleteRole(string name)
		{
			RoleDefinition r = Roles[name];
			if (r == null)
				return;

			r.Delete();
		}

		public RoleAssignments CreateRoleAssignments(string name, string description, RoleDefinition role, bool renameOnMatch)
		{
			if (role == null)
				throw new ArgumentNullException("role");

			if (renameOnMatch)
			{
				if (string.IsNullOrEmpty(name))
					throw new ArgumentNullException("name");
				name = RoleAssignments.MakeNameUnique(name);
			}
			else
				RoleAssignments.CheckName(name);

			if (!CheckObjectIsValid(role))
				throw new AzException("The role is not defined in this application");

			AzAlternative.RoleAssignments r = Locator.Factory.CreateRoleAssignments(Key, name, description, role);
			r.Parent = this;
			RoleAssignments.AddValue(r);

			return r;
		}

		public RoleAssignments CreateRoleAssignments(string name, string description, RoleDefinition role)
		{
			return CreateRoleAssignments(name, description, role, false);
		}

		public void DeleteRoleAssignments(string name)
		{
			RoleAssignments r = RoleAssignments[name];
			if (r == null)
				return;

			r.Delete();
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
			if (operationId < 1)
				throw new ArgumentOutOfRangeException("operationId", "Operation ID must be greater than zero.");
			Operations.CheckName(name);
			Operations.CheckId(operationId);

			Operation o = Locator.Factory.CreateOperation(Key, name, description, operationId);
			o.Parent = this;

			Operations.AddValue(o);
			return o;
		}

		/// <summary>
		/// Deletes the operation from the application
		/// </summary>
		/// <param name="operation">Operaton to delete</param>
		public void DeleteOperation(string name)
		{
			Operation o = Operations[name];
			if (o == null)
				return;

			o.Delete();
		}

		/// <summary>
		/// Creates a new task in the application
		/// </summary>
		/// <param name="name">Required name of the task</param>
		/// <param name="description">Description of the task</param>
		/// <returns>new task</returns>
		public Task CreateTask(string name, string description)
		{
			Tasks.CheckName(name);

			Task t = Locator.Factory.CreateTask(Key, name, description);
			t.Parent = this;

			Tasks.AddValue(t);
			return t;
		}

		/// <summary>
		/// Deletes a task from the application
		/// </summary>
		/// <param name="task">task to delete</param>
		public void DeleteTask(string name)
		{
			Task t = Tasks[name];
			if (t == null)
				return;

			t.Delete();
		}
	}
}
