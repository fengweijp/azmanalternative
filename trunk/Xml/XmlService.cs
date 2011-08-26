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
			return new XmlAdminManager();
		}

		public XmlElement LoadRoot()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(ConnectionString);

			return doc.DocumentElement;
		}

		public XmlElement Load(string uniqueName)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(ConnectionString);

			XmlElement e = (XmlElement)doc.SelectSingleNode(string.Format("//*[@Guid='{0}']", uniqueName));
			if (e == null)
				throw new AzException("The unique name was not found in the store.");

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
			XmlElement e = Load(o.Key);
			e.ParentNode.RemoveChild(e);
			Save(e);
		}

		public void CreateLink(XmlBaseObject parent, string linkName, string uniqueName)
		{
			XmlElement parentNode = Load(parent.Key);

			if (parentNode.SelectNodes(string.Format("{0}[.='{1}']", linkName, uniqueName)).Count > 0)
				return;

			XmlElement e = parentNode.OwnerDocument.CreateElement(linkName);
			e.InnerText = uniqueName;
			parentNode.AppendChild(e);
			Save(parentNode);
		}

		public void RemoveLink(XmlBaseObject parent, string linkName, string uniqueName)
		{
			XmlElement parentNode = Load(parent.Key);
			XmlNode e = parentNode.SelectSingleNode(string.Format("{0}[.='{1}']", linkName, uniqueName));
			if (e == null)
				return;

			parentNode.RemoveChild(e);
			Save(parentNode);
		}

		public override Application GetApplication(string uniqueName)
		{
			XmlApplication a = new XmlApplication();
			a.Load(uniqueName);

			return new Application(a);
		}

		public override IEnumerator<Application> GetApplications(IEnumerable<string> uniqueNames, AdminManager store)
		{
			foreach (var item in FindElements(uniqueNames))
			{
				XmlApplication a = new XmlApplication();
				a.Load(item);

				yield return new Application(a, store);
			}
		}

		public override ApplicationGroup GetGroup(string uniqueName)
		{
			XmlApplicationGroup g = new XmlApplicationGroup();
			g.Load(uniqueName);

			return new ApplicationGroup(g);
		}

		public override IEnumerator<ApplicationGroup> GetGroups(IEnumerable<string> uniqueNames, AdminManager store, Application application)
		{
			foreach (var item in FindElements(uniqueNames))
			{
				XmlApplicationGroup g = new XmlApplicationGroup();
				g.Load(item);

				ApplicationGroup result = new ApplicationGroup(g);
				if (g.IsGlobalGroup)
					result.Store = store;
				else
					result.Parent = application;

				yield return result;
			}
		}

		public override Operation GetOperation(string uniqueName)
		{
			XmlOperation o = new XmlOperation();
			o.Load(uniqueName);

			return new Operation(o);
		}

		public override IEnumerator<Operation> GetOperations(IEnumerable<string> uniqueNames, Application application)
		{
			foreach (var item in FindElements(uniqueNames))
			{
				XmlOperation o = new XmlOperation();
				o.Load(item);

				yield return new Operation(o, application);
			}
		}

		public override Task GetTask(string uniqueName)
		{
			XmlTask t = new XmlTask();
			t.Load(uniqueName);

			return new Task(t);
		}

		public override IEnumerator<Task> GetTasks(IEnumerable<string> uniqueNames, Application application)
		{
			foreach (var item in FindElements(uniqueNames))
			{
				XmlTask t = new XmlTask();
				t.Load(item);

				yield return new Task(t, application);
			}
		}

		public override RoleAssignments GetRoleAssignments(string uniqueName)
		{
			XmlRoleAssignments r = new XmlRoleAssignments();
			r.Load(uniqueName);

			return new RoleAssignments(r);
		}

		public override IEnumerator<RoleAssignments> GetRoleAssignmentsCollection(IEnumerable<string> uniqueNames, Application application)
		{
			foreach (var item in FindElements(uniqueNames))
			{
				XmlRoleAssignments r = new XmlRoleAssignments();
				r.Load(item);

				yield return new RoleAssignments(r, application);
			}
		}

		public override RoleDefinition GetRoleDefinition(string uniqueName)
		{
			XmlRoleDefinition r = new XmlRoleDefinition();
			r.Load(uniqueName);

			return new RoleDefinition(r);
		}

		public override IEnumerator<RoleDefinition> GetRoleDefinitions(IEnumerable<string> uniqueNames, Application application)
		{
			foreach (var item in FindElements(uniqueNames))
			{
				XmlRoleDefinition r = new XmlRoleDefinition();
				r.Load(item);

				yield return new RoleDefinition(r, application);
			}

		}

		//public Collections.MemberCollection GetMembers(XmlElement parent)
		//{
		//    List<Member> result = new List<Member>();
		//    foreach (XmlNode item in parent.SelectNodes("Member"))
		//    {
		//        XmlMember m = new XmlMember(this);
		//        m.Load((XmlElement)item);
		//        result.Add(new Member(m));
		//    }

		//    return new Collections.MemberCollection(result);
		//}

		//public Collections.MemberCollection GetExclusions(XmlElement parent)
		//{
		//    List<Member> result = new List<Member>();
		//    foreach (XmlNode item in parent.SelectNodes("NonMember"))
		//    {
		//        XmlMember m = new XmlMember(this);
		//        m.Load((XmlElement)item);
		//        result.Add(new Member(m));
		//    }

		//    return new Collections.MemberCollection(result, true);
		//}

		private IEnumerable<XmlElement> FindElements(IEnumerable<string> uniqueNames)
		{
			XmlElement root = LoadRoot();

			foreach (var item in uniqueNames)
			{
				yield return (XmlElement)root.SelectSingleNode(string.Format("//*[@Guid='{0}']", item));
			}
		}

		public override IEnumerator<Member> GetMembers(string uniqueName, bool isExclusions)
		{
			XmlElement parent = Load(uniqueName);

			foreach (XmlNode item in XmlMember.GetNodes(parent, isExclusions))
			{
				XmlMember m = new XmlMember();
				m.Load((XmlElement)item);

				yield return new Member(m);
			}
		}
	}
}
