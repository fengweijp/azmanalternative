using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	internal class XmlFactory : Interfaces.IFactoryService
	{
		private XmlService _Service;

		public XmlFactory(XmlService service)
		{
			_Service = service;
		}

		public ApplicationGroup CreateGroup(string parent, string name, string description, GroupType groupType, bool isGlobalGroup)
		{
			XmlApplicationGroup ag = new XmlApplicationGroup();
			ag.Key = System.Guid.NewGuid().ToString();
			ag.Name = name;
			ag.Description = description;
			ag.GroupType = groupType;
			ag.IsGlobalGroup = isGlobalGroup;

			ag.Groups = new Collections.ApplicationGroupCollection(true);

			XmlElement parentNode = null;
			if (isGlobalGroup)
				parentNode = _Service.LoadRoot();
			else
				parentNode = _Service.Load(parent);
			_Service.Save(ag.ToXml(parentNode));

			return new ApplicationGroup(ag);
		}

		public void UpdateGroup(Interfaces.IApplicationGroup group)
		{
			XmlApplicationGroup ag = (XmlApplicationGroup)group;
			_Service.Save(ag);
		}

		public void DeleteGroup(Interfaces.IApplicationGroup group)
		{
			_Service.RemoveElement((XmlApplicationGroup)group);
		}

		public Application CreateApplication(string parent, string name, string description, string versionInformation)
		{
			XmlApplication a = new XmlApplication();
			a.Key = System.Guid.NewGuid().ToString();
			a.Name = name;
			a.Description = description;
			a.ApplicationVersion = versionInformation;

			a.Groups = new Collections.ApplicationGroupCollection(false);
			a.Operations = new Collections.OperationCollection(false);
			a.Tasks = new Collections.TaskCollection(false);
			a.Roles = new Collections.RoleDefinitionCollection(false);
			a.RoleAssignments = new Collections.RoleAssignmentsCollection();

			XmlElement root = _Service.LoadRoot();
			_Service.Save(a.ToXml(root));

			return new Application(a);
		}

		public void UpdateApplication(Interfaces.IApplication application)
		{
			XmlApplication a = (XmlApplication)application;
			_Service.Save(a);
		}

		public void DeleteApplication(Interfaces.IApplication application)
		{
			_Service.RemoveElement((XmlApplication)application);
		}

		public RoleAssignments CreateRoleAssignments(string parent, string name, string description, RoleDefinition role)
		{
			XmlRoleAssignments r = new XmlRoleAssignments();
			r.Key = System.Guid.NewGuid().ToString();
			r.Name = name;
			r.Description = description;
			r.Definition = role;

			r.Groups = new Collections.ApplicationGroupCollection(true);

			XmlElement thisNode = _Service.Load(parent);
			_Service.Save(r.ToXml(thisNode));

			return new RoleAssignments(r);
		}

		public void UpdateRoleAssignments(Interfaces.IRoleAssignment role)
		{
			_Service.Save((XmlRoleAssignments)role);
		}

		public void DeleteRoleAssignments(Interfaces.IRoleAssignment role)
		{
			_Service.RemoveElement((XmlRoleAssignments)role);
		}

		public RoleDefinition CreateRole(string parent, string name, string description)
		{
			XmlRoleDefinition d = new XmlRoleDefinition();
			d.Key = System.Guid.NewGuid().ToString();
			d.Name = name;
			d.Description = description;

			d.Operations = new Collections.OperationCollection(true);
			d.Tasks = new Collections.TaskCollection(true);
			d.Roles = new Collections.RoleDefinitionCollection(true);

			_Service.Save(d.ToXml(_Service.Load(parent)));

			return new RoleDefinition(d);
		}

		public void UpdateRole(Interfaces.IRoleDefinition role)
		{
			_Service.Save((XmlRoleDefinition)role);
		}

		public void DeleteRole(Interfaces.IRoleDefinition role)
		{
			_Service.RemoveElement((XmlRoleDefinition)role);
		}

		public Operation CreateOperation(string parent, string name, string description, int operationId)
		{
			XmlOperation o = new XmlOperation();
			o.Key = System.Guid.NewGuid().ToString();
			o.Name = name;
			o.Description = description;
			o.OperationId = operationId;

			XmlElement thisNode = _Service.Load(parent);
			_Service.Save(o.ToXml(thisNode));

			return new Operation(o);
		}

		public void UpdateOperation(Interfaces.IOperation operation)
		{
			_Service.Save((XmlOperation)operation);
		}

		public void DeleteOperation(Interfaces.IOperation operation)
		{
			_Service.RemoveElement((XmlOperation)operation);
		}

		public Task CreateTask(string parent, string name, string description)
		{
			XmlTask t = new XmlTask();
			t.Key = System.Guid.NewGuid().ToString();
			t.Name = name;
			t.Description = description;

			t.Operations = new Collections.OperationCollection(true);
			t.Tasks = new Collections.TaskCollection(true);

			XmlElement thisNode = _Service.Load(parent);
			_Service.Save(t.ToXml(thisNode));

			return new Task(t);
		}

		public void UpdateTask(Interfaces.ITask task)
		{
			_Service.Save((XmlTask)task);
		}

		public void DeleteTask(Interfaces.ITask task)
		{
			_Service.RemoveElement((XmlTask)task);
		}

	}
}
