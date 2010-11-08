using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
    /// <summary>
    /// An application role. Used to define a role and check membership
    /// </summary>
	public class Role : BaseObject, Interfaces.IRole
	{
		private readonly Interfaces.IRole _Role;

        /// <summary>
        /// Gets the role identifier
        /// </summary>
		public Guid Guid
		{
			get { return _Role.Guid; }
		}

        /// <summary>
        /// Gets or sets the role name
        /// </summary>
		public string Name
		{
			get { return _Role.Name; }
			set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Name");
                _Role.Name = value; 
            }
		}

        /// <summary>
        /// Gets or sets the role description
        /// </summary>
		public string Description
		{
			get { return _Role.Description; }
			set { _Role.Description = value; }
		}

        /// <summary>
        /// Gets the collection of groups in the role
        /// </summary>
		public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		{
			get { return _Role.Groups; }
		}

        /// <summary>
        /// Gets the collection of members assigned to the role
        /// </summary>
		public System.Collections.ObjectModel.ReadOnlyCollection<Interfaces.IMember> Members
		{
			get { return _Role.Members; }
		}

        /// <summary>
        /// Gets the collection of roles added to the role
        /// </summary>
		public System.Collections.ObjectModel.ReadOnlyCollection<Role> Roles
		{
            get { return _Role.Roles; }
		}

        /// <summary>
        /// Gets the collection of operations directly defined in the role
        /// </summary>
		public System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations
		{
            get { return _Role.Operations; }
		}

        /// <summary>
        /// Gets the collection of tasks directly defined in the role
        /// </summary>
		public System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks
		{
            get { return _Role.Tasks; }
		}

        internal Role(Interfaces.IRole role)
        {
            _Role = role;
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

            _Role.AddGroup(group);
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

            _Role.RemoveGroup(group);
		}

        /// <summary>
        /// Adds a role to this role
        /// </summary>
        /// <param name="role">Role to add</param>
		public void AddRole(Role role)
		{
            CheckObjectIsValid(role);

            _Role.AddRole(role);
		}

        /// <summary>
        /// Removes a role from this role
        /// </summary>
        /// <param name="role">Role to remove</param>
		public void RemoveRole(Role role)
		{
            CheckObjectIsValid(role);

            _Role.RemoveRole(role);
		}

        /// <summary>
        /// Adds an operation to the role
        /// </summary>
        /// <param name="operation"></param>
		public void AddOperation(Operation operation)
		{
            CheckObjectIsValid(operation);

            _Role.AddOperation(operation);
		}

        /// <summary>
        /// Removes an operation from this role
        /// </summary>
        /// <param name="operation">Operation to remove</param>
		public void RemoveOperation(Operation operation)
		{
            CheckObjectIsValid(operation);

            _Role.RemoveOperation(operation);
		}

        /// <summary>
        /// Adds a task to this role
        /// </summary>
        /// <param name="task">Task to add</param>
		public void AddTask(Task task)
		{
            CheckObjectIsValid(task);

            _Role.AddTask(task);
		}

        /// <summary>
        /// Removes a task from this role
        /// </summary>
        /// <param name="task">Task to remove</param>
		public void RemoveTask(Task task)
		{
            CheckObjectIsValid(task);

            _Role.RemoveTask(task);
		}

	}
}
