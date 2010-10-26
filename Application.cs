using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	public class Application : Interfaces.IApplication
	{
		private readonly Interfaces.IApplication _Application;

        /// <summary>
        /// Gets the application identifier
        /// </summary>
        public Guid Guid
		{
			get { return _Application.Guid; }
		}

        /// <summary>
        /// Gets or sets the application name
        /// </summary>
        public string Name
		{
			get { return _Application.Name; }
			set { _Application.Name = value; }
		}

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description
		{
			get { return _Application.Description; }
			set { _Application.Description = value; }
		}

        /// <summary>
        /// Gets or sets the application version
        /// </summary>
        public string ApplicationVersion
		{
			get { return _Application.ApplicationVersion; }
			set { _Application.ApplicationVersion = value; }
		}

        /// <summary>
        /// Gets the collection of roles defined in the application
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<Role> Roles
		{
			get { return _Application.Roles; }
		}

        /// <summary>
        /// Gets the collection of groups defined in the application
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		{
			get { return _Application.Groups; }
		}

        /// <summary>
        /// Gets the collection of operations defined in the application
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations
		{
			get { return _Application.Operations; }
		}

        /// <summary>
        /// Gets the collection of tasks defined in the application
        /// </summary>
        public System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks
		{
			get { return _Application.Tasks; }
		}

		internal Application(Interfaces.IApplication application)
		{
			_Application = application;
		}

		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			throw new NotImplementedException();
		}

		public void DeleteGroup(ApplicationGroup group)
		{
			throw new NotImplementedException();
		}

		public Role CreateRole(string name, string description)
		{
			throw new NotImplementedException();
		}

		public void DeleteRole(Role role)
		{
			throw new NotImplementedException();
		}

		public Operation CreateOperation(string name, string description, int operationId)
		{
			throw new NotImplementedException();
		}

		public void DeleteOperation(Operation operation)
		{
			throw new NotImplementedException();
		}

		public Task CreateTask(string name, string description)
		{
			throw new NotImplementedException();
		}

		public void DeleteTask(Task task)
		{
			throw new NotImplementedException();
		}
	}
}
