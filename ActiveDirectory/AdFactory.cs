using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.ActiveDirectory
{
	internal class AdFactory : Interfaces.IFactoryService
	{
		protected const string GROUPSCONTAINER = "AzGroupObjectContainer-";

		private AdService _Service;

		public AdFactory(AdService service)
		{
			_Service = service;
		}

		public ApplicationGroup CreateGroup(string parent, string name, string description, GroupType groupType, bool isGlobalGroup)
		{
			AdApplicationGroup ag = new AdApplicationGroup();
			ag.Name = name;
			ag.Description = description;
			ag.GroupType = groupType;
			//ag.ContainerDn = GROUPSCONTAINER + this.Name + "," + this.Key;
			ag.IsGlobalGroup = true;

			ag.Groups = new Collections.ApplicationGroupCollection(true);

			_Service.Save(ag.CreateNew());

			return new ApplicationGroup(ag);
		}

		public void UpdateGroup(Interfaces.IApplicationGroup group)
		{
			AdApplicationGroup ag = (AdApplicationGroup)group;
			_Service.Save(ag.GetUpdate());
		}

		public void DeleteGroup(Interfaces.IApplicationGroup group)
		{
			((AdApplicationGroup)group).Delete();
		}

		public Application CreateApplication(string parent, string name, string description, string versionInformation)
		{
			AdApplication a = new AdApplication();
			a.ContainerDn = parent;
			a.Name = name;
			a.Description = description;
			a.ApplicationVersion = versionInformation;

			a.Groups = new Collections.ApplicationGroupCollection(false);
			a.Operations = new Collections.OperationCollection(false);
			a.RoleAssignments = new Collections.RoleAssignmentsCollection();
			a.Roles = new Collections.RoleDefinitionCollection(false);

			_Service.Save(a.CreateNew());

			return new Application(a);
		}

		public void UpdateApplication(Interfaces.IApplication application)
		{
			AdApplication a = (AdApplication)application;
			_Service.Save(a.GetUpdate());
		}

		public void DeleteApplication(Interfaces.IApplication application)
		{
			((AdApplication)application).Delete();
		}

		public RoleAssignments CreateRoleAssignments(string parent, string name, string description, RoleDefinition role)
		{
			throw new NotImplementedException();
		}

		public void UpdateRoleAssignments(Interfaces.IRoleAssignment role)
		{
			throw new NotImplementedException();
		}

		public void DeleteRoleAssignments(Interfaces.IRoleAssignment role)
		{
			throw new NotImplementedException();
		}

		public RoleDefinition CreateRole(string parent, string name, string description)
		{
			AdRoleDefinition role = new AdRoleDefinition();
			role.Name = name;
			role.Description = description;

			_Service.Save(role.CreateNew());

			return new RoleDefinition(role);
		}

		public void UpdateRole(Interfaces.IRoleDefinition role)
		{
			AdRoleDefinition introle = (AdRoleDefinition)role;
			_Service.Save(introle.GetUpdate());
		}

		public void DeleteRole(Interfaces.IRoleDefinition role)
		{
			((AdRoleDefinition)role).Delete();
		}

		public Operation CreateOperation(string parent, string name, string description, int operationId)
		{
			AdOperation op = new AdOperation();
			op.Name = name;
			op.Description = description;
			op.OperationId = operationId;

			_Service.Save(op.CreateNew());

			return new Operation(op);
		}

		public void UpdateOperation(Interfaces.IOperation operation)
		{
			AdOperation op = (AdOperation)operation;
			_Service.Save(op.GetUpdate());
		}

		public void DeleteOperation(Interfaces.IOperation operation)
		{
			((AdOperation)operation).Delete();
		}

		public Task CreateTask(string parent, string name, string description)
		{
			throw new NotImplementedException();
		}

		public void UpdateTask(Interfaces.ITask task)
		{
			throw new NotImplementedException();
		}

		public void DeleteTask(Interfaces.ITask task)
		{
			throw new NotImplementedException();
		}
	}
}
