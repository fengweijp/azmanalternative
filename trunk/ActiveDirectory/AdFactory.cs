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
			ag.ContainerDn = string.Format("cn={0}{1},{2}", GROUPSCONTAINER, parent.Substring(3, parent.IndexOf(",") - 3), parent);
			ag.IsGlobalGroup = isGlobalGroup;

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
			a.Key = string.Format("cn={0},{1}", a.CN, a.ContainerDn);

			a.Groups = new Collections.ApplicationGroupCollection(false);
			a.Operations = new Collections.OperationCollection(false);
			a.RoleAssignments = new Collections.RoleAssignmentsCollection();
			a.Roles = new Collections.RoleDefinitionCollection(false);
			a.Tasks = new Collections.TaskCollection(false);

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
			AdRoleAssignments r = new AdRoleAssignments();
			r.ContainerDn = parent;
			r.Name = name;
			r.Description = description;
			r.Definition = role;

			r.Groups = new Collections.ApplicationGroupCollection(true);
			r.Members = new Collections.MemberCollection(null);

			_Service.Save(r.CreateNew());

			return new RoleAssignments(r);
		}

		public void UpdateRoleAssignments(Interfaces.IRoleAssignment role)
		{
			AdRoleAssignments r = (AdRoleAssignments)role;
			_Service.Save(r.GetUpdate());
		}

		public void DeleteRoleAssignments(Interfaces.IRoleAssignment role)
		{
			((AdRoleAssignments)role).Delete();
		}

		public RoleDefinition CreateRole(string parent, string name, string description)
		{
			AdRoleDefinition role = new AdRoleDefinition();
			role.Name = name;
			role.Description = description;

			role.Operations = new Collections.OperationCollection(true);
			role.Tasks = new Collections.TaskCollection(true);
			role.Roles = new Collections.RoleDefinitionCollection(true);

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
			AdTask t = new AdTask();
			t.ContainerDn = parent;
			t.Name = name;
			t.Description = description;

			t.Operations = new Collections.OperationCollection(true);
			t.Tasks = new Collections.TaskCollection(true);

			_Service.Save(t.CreateNew());

			return new Task(t);
		}

		public void UpdateTask(Interfaces.ITask task)
		{
			AdTask t = (AdTask)task;
			_Service.Save(t.GetUpdate());
		}

		public void DeleteTask(Interfaces.ITask task)
		{
			((AdTask)task).Delete();
		}
	}
}
