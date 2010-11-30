using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AzAlternative.Xml
{
	/// <summary>
	/// Wraps up access to the underlying store & provides helper method to access it
	/// </summary>
	internal class XmlService : ServiceBase
	{
		public XmlService(string connectionString)
		{
			ConnectionString = connectionString;
		}

		public override Interfaces.IAdminManager GetAdminManager()
		{
			return new XmlAdminManager(this);
		}

		public XmlElement LoadRoot()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(ConnectionString);

			return doc.DocumentElement;
		}

		public XmlElement Load(Guid guid)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(ConnectionString);

			XmlElement e = (XmlElement)doc.SelectSingleNode(string.Format("//*[@Guid='{0}']", guid));
			if (e == null)
				throw new AzException("The Guid was not found in the store.");

			return e;
		}

		public void Save(XmlBaseObject o)
		{
			XmlElement e = o.ToXml();
			e.OwnerDocument.Save(ConnectionString);
		}

		public void Save(XmlElement node)
		{
			node.OwnerDocument.Save(ConnectionString);
		}

		public void RemoveElement(XmlBaseObject o)
		{
			XmlElement e = Load(o.Guid);
			e.ParentNode.RemoveChild(e);
			Save(e);
		}

		public void CreateLink(XmlBaseObject parent, string linkName, Guid guid)
		{
			XmlElement parentNode = Load(parent.Guid);

			if (parentNode.SelectNodes(string.Format("{0}[.='{1}']", linkName, guid)).Count > 0)
				return;

			XmlElement e = parentNode.OwnerDocument.CreateElement(linkName);
			e.InnerText = guid.ToString();
			parentNode.AppendChild(e);
			Save(parentNode);
		}

		public void RemoveLink(XmlBaseObject parent, string linkName, Guid guid)
		{
			XmlElement parentNode = Load(parent.Guid);
			XmlNode e = parentNode.SelectSingleNode(string.Format("{0}[.='{1}']", linkName, guid));
			if (e == null)
				return;

			parentNode.RemoveChild(e);
			Save(parentNode);
		}

		public override Application GetApplication(Guid guid)
		{
			XmlApplication a = new XmlApplication(this);
			a.Load(guid);

            return new Application(a);
		}

        public override IEnumerator<Application> GetApplications(IEnumerable<Guid> guids, AdminManager store)
        {
			foreach (var item in FindElements(guids))
			{
				XmlApplication a = new XmlApplication(this);
				a.Load(item);

				yield return new Application(a, store);
			}
		}

        public override ApplicationGroup GetGroup(Guid guid)
        {
            XmlApplicationGroup g = new XmlApplicationGroup(this);
            g.Load(guid);

            return new ApplicationGroup(g);
        }

		public override IEnumerator<ApplicationGroup> GetGroups(IEnumerable<Guid> guids, AdminManager store, Application application)
		{
			foreach (var item in FindElements(guids))
			{
				XmlApplicationGroup g = new XmlApplicationGroup(this);
				g.Load(item);

				ApplicationGroup result = new ApplicationGroup(g);
				if (g.IsGlobalGroup)
					result.Store = store;
				else
					result.Parent = application;

				yield return result;
			}
		}

        public override Operation GetOperation(Guid guid)
        {
            XmlOperation o = new XmlOperation(this);
            o.Load(guid);

            return new Operation(o);
        }

		public override IEnumerator<Operation> GetOperations(IEnumerable<Guid> guids, Application application)
        {
			foreach (var item in FindElements(guids))
			{
				XmlOperation o = new XmlOperation(this);
				o.Load(item);

				yield return new Operation(o, application);
			}
        }

        public override Task GetTask(Guid guid)
        {
			XmlTask t = new XmlTask(this);
			t.Load(guid);

			return new Task(t);
        }

		public override IEnumerator<Task> GetTasks(IEnumerable<Guid> guids, Application application)
        {
			foreach (var item in FindElements(guids))
			{
				XmlTask t = new XmlTask(this);
				t.Load(item);

				yield return new Task(t, application);
			}
        }

        public override RoleAssignments GetRoleAssignments(Guid guid)
        {
			XmlRoleAssignments r = new XmlRoleAssignments(this);
			r.Load(guid);

			return new RoleAssignments(r);
        }

		public override IEnumerator<RoleAssignments> GetRoleAssignmentsCollection(IEnumerable<Guid> guids, Application application)
        {
			foreach (var item in FindElements(guids))
			{
				XmlRoleAssignments r = new XmlRoleAssignments(this);
				r.Load(item);

				yield return new RoleAssignments(r, application);
			}
		}

        public override RoleDefinition GetRoleDefinition(Guid guid)
        {
			XmlRoleDefinition r = new XmlRoleDefinition(this);
			r.Load(guid);

			return new RoleDefinition(r);
        }

		public override IEnumerator<RoleDefinition> GetRoleDefinitions(IEnumerable<Guid> guids, Application application)
        {
			foreach (var item in FindElements(guids))
			{
				XmlRoleDefinition r = new XmlRoleDefinition(this);
				r.Load(item);

				yield return new RoleDefinition(r, application);
			}

        }

		public Collections.MemberCollection GetMembers(XmlElement parent)
		{
			List<Member> result = new List<Member>();
			foreach (XmlNode item in parent.SelectNodes("Member"))
			{
				XmlMember m = new XmlMember(this);
				m.Load((XmlElement)item);
				result.Add(new Member(m));
			}

			return new Collections.MemberCollection(result);
		}

		public Collections.MemberCollection GetExclusions(XmlElement parent)
		{
			List<Member> result = new List<Member>();
			foreach (XmlNode item in parent.SelectNodes("NonMember"))
			{
				XmlMember m = new XmlMember(this);
				m.Load((XmlElement)item);
				result.Add(new Member(m));
			}

			return new Collections.MemberCollection(result, true);
		}

		private IEnumerable<XmlElement> FindElements(IEnumerable<Guid> guids)
		{
			XmlElement root = LoadRoot();

			foreach (var item in guids)
			{
				yield return (XmlElement)root.SelectSingleNode(string.Format("//*[@Guid='{0}']", item));
			}
		}
	}
}
