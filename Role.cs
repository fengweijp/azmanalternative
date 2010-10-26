using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	public class Role : Interfaces.IRole
	{
		private readonly Interfaces.IRole _Role;

		public Guid Guid
		{
			get { return _Role.Guid; }
		}

		public string Name
		{
			get { return _Role.Name; }
			set { _Role.Name = value; }
		}

		public string Description
		{
			get { return _Role.Description; }
			set { _Role.Description = value; }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> ApplicationGroups
		{
			get { return _Role.ApplicationGroups; }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Interfaces.IMember> Members
		{
			get { return _Role.Members; }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Role> Roles
		{
			get { throw new NotImplementedException(); }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations
		{
			get { throw new NotImplementedException(); }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks
		{
			get { throw new NotImplementedException(); }
		}

		public void AddGroup(ApplicationGroup group)
		{
			throw new NotImplementedException();
		}

		public void RemoveGroup(ApplicationGroup group)
		{
			throw new NotImplementedException();
		}

		public void AddRole(Role role)
		{
			throw new NotImplementedException();
		}

		public void RemoveRole(Role role)
		{
			throw new NotImplementedException();
		}

		public void AddOperation(Operation operation)
		{
			throw new NotImplementedException();
		}

		public void RemoveOperation(Operation operation)
		{
			throw new NotImplementedException();
		}

		public void AddTask(Task task)
		{
			throw new NotImplementedException();
		}

		public void RemoveTask(Task task)
		{
			throw new NotImplementedException();
		}
	}
}
