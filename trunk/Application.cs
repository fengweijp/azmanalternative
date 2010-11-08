using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	public class Application : ContainerBase, Interfaces.IApplication
	{
		internal readonly Interfaces.IApplication Instance;

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

		///// <summary>
		///// Gets the collection of roles defined in the application
		///// </summary>
		//public System.Collections.ObjectModel.ReadOnlyCollection<Role> Roles
		//{
		//    get { return _Application.Roles; }
		//}

		///// <summary>
		///// Gets the collection of groups defined in the application
		///// </summary>
		//public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		//{
		//    get { return _Application.Groups; }
		//}

		///// <summary>
		///// Gets the collection of operations defined in the application
		///// </summary>
		//public System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations
		//{
		//    get { return _Application.Operations; }
		//}

		///// <summary>
		///// Gets the collection of tasks defined in the application
		///// </summary>
		//public System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks
		//{
		//    get { return _Application.Tasks; }
		//}

        public AdminManager Store
        {
            get;
            internal set;
        }

		internal Application(Interfaces.IApplication application)
		{
			Instance = application;
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

			ApplicationGroup g = Instance.CreateGroup(name, description, groupType);
			g.Application = this;

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
		}

		public void UpdateGroup(ApplicationGroup group)
		{
			if (!CheckObjectIsValid(group))
				throw new AzException("The group is not part of this application. Only application groups can be updated here.");

			Instance.UpdateGroup(group);
		}

		///// <summary>
		///// Create a new role in the application
		///// </summary>
		///// <param name="name">Required name of the role</param>
		///// <param name="description">Description of the role</param>
		///// <returns>new role</returns>
		//public Role CreateRole(string name, string description)
		//{
		//    if (string.IsNullOrEmpty(name))
		//        throw new ArgumentNullException("name");

		//    Role r = _Application.CreateRole(name, description);
		//    r.Application = this;

		//    return r;
		//}

		///// <summary>
		///// Deletes a role from the application
		///// </summary>
		///// <param name="role"></param>
		//public void DeleteRole(Role role)
		//{
		//    if (role.Application.Guid != this.Guid)
		//        throw new AzException("Role is part of the application.");

		//    _Application.DeleteRole(role);
		//}

		/// <summary>
		/// Create a new operation in the application
		/// </summary>
		/// <param name="name">Required operation name</param>
		/// <param name="description">Operaton description</param>
		/// <param name="operationId">Required operaton ID</param>
		/// <returns>new operation</returns>
		public Operation CreateOperation(string name, string description, int operationId)
		{
			Operation o = Instance.CreateOperation(name, description, operationId);
			o.Application = this;

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

		///// <summary>
		///// Creates a new task in the application
		///// </summary>
		///// <param name="name">Required name of the task</param>
		///// <param name="description">Description of the task</param>
		///// <returns>new task</returns>
		//public Task CreateTask(string name, string description)
		//{
		//    if (string.IsNullOrEmpty(name))
		//        throw new ArgumentNullException("name");

		//    Task t = _Application.CreateTask(name, description);
		//    t.Application = this;
		//    return t;
		//}

		///// <summary>
		///// Deletes a task from the application
		///// </summary>
		///// <param name="task">task to delete</param>
		//public void DeleteTask(Task task)
		//{
		//    if (task.Application.Guid != this.Guid)
		//        throw new AzException("The task is not part of this application.");

		//    _Application.DeleteTask(task);
		//}
	}
}
