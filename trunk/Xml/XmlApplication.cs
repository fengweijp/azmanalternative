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
		private const string GUID = "Guid";
		private const string NAME = "Name";
		private const string DESCRIPTION = "Description";
		private const string APPLICATIONVERSION = "ApplicationVersion";

		public Guid Guid
		{
			get
			{
				string s = GetAttribute(GUID);
				if (s == null)
					return Guid.Empty;

				return new Guid(s);
			}
			set
			{
				SetAttribute(GUID, value.ToString());
			}
		}

		public string Name
		{
			get
			{
				return GetAttribute(NAME);
			}
			set
			{
				SetAttribute(NAME, value);
			}
		}

		public string Description
		{
			get
			{
				return GetAttribute(DESCRIPTION);
			}
			set
			{
				SetAttribute(DESCRIPTION, value);
			}
		}

		public string ApplicationVersion
		{
			get
			{
				return GetAttribute(APPLICATIONVERSION);
			}
			set
			{
				SetAttribute(APPLICATIONVERSION, value);
			}
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Role> Roles
		{
			get { throw new NotImplementedException(); }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		{
			get { return GetCollection<ApplicationGroup>(XmlApplicationGroup.GetApplicationGroups(Node), typeof(XmlApplicationGroup)); }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations
		{
			get { throw new NotImplementedException(); }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks
		{
			get { throw new NotImplementedException(); }
		}

		public XmlApplication(XmlElement node, XmlFactory factory)
			: base(node, factory)
		{ }

		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			XmlApplicationGroup ag = XmlApplicationGroup.NewApplicationGroup(Factory, name, description, groupType);
			ag.Update(Node);

			return new ApplicationGroup(ag);
		}

		public void DeleteGroup(ApplicationGroup group)
		{
            XmlApplicationGroup.RemoveApplicationGroup(Node, group.Guid);
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
            XmlOperation o = XmlOperation.CreateOperation(Factory, name, description, operationId);
            o.Update(Node);

            return new Operation(o);
		}

		public void DeleteOperation(Operation operation)
		{
            XmlOperation.RemoveOperation(Node, operation.Guid);
		}

		public Task CreateTask(string name, string description)
		{
			throw new NotImplementedException();
		}

		public void DeleteTask(Task task)
		{
			throw new NotImplementedException();
		}

		public static XmlNodeList GetApplications(XmlElement parent)
		{
			return parent.SelectNodes(ELEMENTNAME);
		}

		public static XmlApplication CreateApplication(XmlFactory factory, string name, string description, string applicationVersion)
		{
			XmlApplication a = new XmlApplication(factory.CreateNew(ELEMENTNAME), factory);
			a.Guid = Guid.NewGuid();
			a.Name = name;
			a.Description = description;
			a.ApplicationVersion = applicationVersion;

			return a;
		}

		public static void RemoveApplication(XmlElement parent, Guid guid)
		{
			XmlNode node = parent.SelectSingleNode(string.Format("{0}[@{1}={2}]", ELEMENTNAME, GUID, guid));
			if (node == null)
				return;

			parent.RemoveChild(node);
		}
	}
}
