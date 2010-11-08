using System;
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

		//public System.Collections.ObjectModel.ReadOnlyCollection<Role> Roles
		//{
		//    get { throw new NotImplementedException(); }
		//}

		//public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		//{
		//    get { return GetCollection<ApplicationGroup>(XmlApplicationGroup.GetApplicationGroups(Node), typeof(XmlApplicationGroup)); }
		//}

		//public System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations
		//{
		//    get { throw new NotImplementedException(); }
		//}

		//public System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks
		//{
		//    get { throw new NotImplementedException(); }
		//}

		public XmlApplication(XmlService service)
			: base(service)
		{ }

		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			XmlApplicationGroup ag = new XmlApplicationGroup(Service);
			ag.Guid = System.Guid.NewGuid();
			ag.Name = name;
			ag.Description = description;
			ag.GroupType = groupType;

			XmlElement root = Service.LoadRoot();
			Service.Save(ag.ToXml(root));

			return new ApplicationGroup(ag);
		}

		public void DeleteGroup(ApplicationGroup group)
		{
			Service.RemoveElement((XmlApplicationGroup)group.Instance);
		}

		public void UpdateGroup(ApplicationGroup group)
		{
			XmlApplicationGroup ag = (XmlApplicationGroup)group.Instance;
			Service.Save(ag);
		}

		public RoleDefinition CreateRole(string name, string description)
		{
			XmlRoleDefinition d = new XmlRoleDefinition(Service);
			d.Guid = System.Guid.NewGuid();
			d.Name = name;
			d.Description = description;

			Service.Save(d.ToXml(Service.Load(this.Guid)));

			return new RoleDefinition(d);
		}

		public void DeleteRole(RoleDefinition role)
		{
			Service.RemoveElement((XmlRoleDefinition)role.Instance);
		}

		public void UpdateRole(RoleDefinition role)
		{
			Service.Save((XmlRoleDefinition)role.Instance);
		}

		public Operation CreateOperation(string name, string description, int operationId)
		{
			XmlOperation o = new XmlOperation(Service);
			o.Guid = System.Guid.NewGuid();
			o.Name = name;
			o.Description = description;
			o.OperationId = operationId;

			XmlElement thisNode = Service.Load(this.Guid);
			Service.Save(o.ToXml(thisNode));

			return new Operation(o);
		}

		public void DeleteOperation(Operation operation)
		{
			Service.RemoveElement((XmlOperation)operation.Instance);
		}

		public void UpdateOperation(Operation operation)
		{
			Service.Save((XmlOperation)operation.Instance);
		}

		public Task CreateTask(string name, string description)
		{
			XmlTask t = new XmlTask(Service);
			t.Guid = System.Guid.NewGuid();
			t.Name = name;
			t.Description = description;

			XmlElement thisNode = Service.Load(this.Guid);
			Service.Save(t.ToXml(thisNode));

			return new Task(t);
		}

		public void DeleteTask(Task task)
		{
			Service.RemoveElement((XmlTask)task.Instance);
		}

		public void UpdateTask(Task task)
		{
			Service.Save((XmlTask)task.Instance);
		}

		public override XmlElement ToXml(XmlElement parent)
		{
			XmlElement e = parent.OwnerDocument.CreateElement(ELEMENTNAME);
			SetAttribute(e, GUID, Guid.ToString());
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);
			SetAttribute(e, APPLICATIONVERSION, ApplicationVersion);

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
		}
	}
}
