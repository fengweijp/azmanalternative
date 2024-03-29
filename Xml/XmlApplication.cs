﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	internal class XmlApplication : XmlBaseObject, Interfaces.IApplication
	{
		private const string ELEMENTNAME = "AzApplication";
		private const string APPLICATIONVERSION = "ApplicationVersion";

		public string ApplicationVersion
		{
			get;
			set;
		}

		public Collections.RoleDefinitionCollection Roles
		{
			get;
			set;
		}

		public Collections.RoleAssignmentsCollection RoleAssignments
		{
			get;
			set;
		}

		public Collections.ApplicationGroupCollection Groups
		{
			get;
			set;
		}

		public Collections.OperationCollection Operations
		{
			get;
			set;
		}

		public Collections.TaskCollection Tasks
		{
			get;
			set;
		}

		//public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		//{
		//    XmlApplicationGroup ag = new XmlApplicationGroup();
		//    ag.Key = System.Guid.NewGuid().ToString();
		//    ag.Name = name;
		//    ag.Description = description;
		//    ag.GroupType = groupType;

		//    ag.Groups = new Collections.ApplicationGroupCollection(true);

		//    Service.Save(ag.ToXml(Service.Load(this.Key)));

		//    return new ApplicationGroup(ag);
		//}

		//public void DeleteGroup(ApplicationGroup group)
		//{
		//    Service.RemoveElement((XmlApplicationGroup)group.Instance);
		//}

		//public void UpdateGroup(ApplicationGroup group)
		//{
		//    XmlApplicationGroup ag = (XmlApplicationGroup)group.Instance;
		//    Service.Save(ag);
		//}

		//public RoleDefinition CreateRole(string name, string description)
		//{
		//    XmlRoleDefinition d = new XmlRoleDefinition();
		//    d.Key = System.Guid.NewGuid().ToString();
		//    d.Name = name;
		//    d.Description = description;

		//    d.Operations = new Collections.OperationCollection(true);
		//    d.Tasks = new Collections.TaskCollection(true);
		//    d.Roles = new Collections.RoleDefinitionCollection(true);

		//    Service.Save(d.ToXml(Service.Load(this.Key)));

		//    return new RoleDefinition(d);
		//}

		//public void DeleteRole(RoleDefinition role)
		//{
		//    Service.RemoveElement((XmlRoleDefinition)role.Instance);
		//}


		//public Operation CreateOperation(string name, string description, int operationId)
		//{
		//    XmlOperation o = new XmlOperation();
		//    o.Key = System.Guid.NewGuid().ToString();
		//    o.Name = name;
		//    o.Description = description;
		//    o.OperationId = operationId;

		//    XmlElement thisNode = Service.Load(this.Key);
		//    Service.Save(o.ToXml(thisNode));

		//    return new Operation(o);
		//}

		//public void DeleteOperation(Operation operation)
		//{
		//    Service.RemoveElement((XmlOperation)operation.Instance);
		//}

		//public Task CreateTask(string name, string description)
		//{
		//    XmlTask t = new XmlTask();
		//    t.Key = System.Guid.NewGuid().ToString();
		//    t.Name = name;
		//    t.Description = description;

		//    t.Operations = new Collections.OperationCollection(true);
		//    t.Tasks = new Collections.TaskCollection(true);

		//    XmlElement thisNode = Service.Load(this.Key);
		//    Service.Save(t.ToXml(thisNode));

		//    return new Task(t);
		//}

		//public void DeleteTask(Task task)
		//{
		//    Service.RemoveElement((XmlTask)task.Instance);
		//}


		//public RoleAssignments CreateRoleAssignments(string name, string description, RoleDefinition role)
		//{
		//    XmlRoleAssignments r = new XmlRoleAssignments();
		//    r.Key = System.Guid.NewGuid().ToString();
		//    r.Name = name;
		//    r.Description = description;
		//    r.Definition = role;

		//    r.Groups = new Collections.ApplicationGroupCollection(true);

		//    XmlElement thisNode = Service.Load(this.Key);
		//    Service.Save(r.ToXml(thisNode));

		//    return new RoleAssignments(r);
		//}

		//public void DeleteRoleAssignments(RoleAssignments role)
		//{
		//    Service.RemoveElement((XmlRoleAssignments)role.Instance);
		//}

		public override XmlElement ToXml(XmlElement parent)
		{
			XmlElement e = parent.OwnerDocument.CreateElement(ELEMENTNAME);
			SetAttribute(e, GUID, Key);
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);
			SetAttribute(e, APPLICATIONVERSION, ApplicationVersion);

			parent.AppendChild(e);

			return e;
		}

		public override XmlElement ToXml()
		{
			XmlElement e = base.ToXml();
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);
			SetAttribute(e, APPLICATIONVERSION, ApplicationVersion);

			return e;
		}

		protected override void LoadInternal(XmlElement element)
		{
			base.LoadInternal(element);
			ApplicationVersion = GetAttribute(element, APPLICATIONVERSION);

			Groups = new Collections.ApplicationGroupCollection(XmlApplicationGroup.GetChildren(element), false);
			Operations = new Collections.OperationCollection(XmlOperation.GetChildren(element), false);
			Tasks = new Collections.TaskCollection(XmlTask.GetTasks(element), false);
			Roles = new Collections.RoleDefinitionCollection(XmlRoleDefinition.GetRoles(element), false);
			RoleAssignments = new Collections.RoleAssignmentsCollection(XmlRoleAssignments.GetChildren(element));
		}

		//public static IEnumerator<Application> GetApplications(XmlService service, Guid guid)
		//{
		//    XmlElement parent = service.Load(guid);

		//    foreach (XmlNode item in parent.SelectNodes(ELEMENTNAME))
		//    {
		//        XmlApplication a = new XmlApplication(service);
		//        a.Load((XmlElement)item);
		//        yield return new Application(a);
		//    }
		//}

		public static Dictionary<string, string> GetChildren(XmlElement parent)
		{
			return GetChildren(parent, ELEMENTNAME);
		}
	}
}
