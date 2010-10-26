using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	internal class XmlAdminManager : XmlBaseObject, Interfaces.IAdminManager
	{
		private const string ELEMENTNAME = "AzAdminManager";
		private const string MAJORVERSION = "MajorVersion";
		private const string MINORVERSION = "MinorVersion";
		private const string DESCRIPTION = "Description";
		private const string GUID = "Guid";

		public int MajorVersion
		{
			get
			{
				string s = GetAttribute(MAJORVERSION);
				if (s == null)
					return 0;

				return int.Parse(s);
			}
			set
			{
				SetAttribute(MAJORVERSION, value.ToString());
				Factory.SaveChanges();
			}
		}

		public int MinorVersion
		{
			get
			{
				string s = GetAttribute(MINORVERSION);
				if (s == null)
					return 0;

				return int.Parse(s);
			}
			set
			{
				SetAttribute(MINORVERSION, value.ToString());
				Factory.SaveChanges();
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
				SetAttribute(DESCRIPTION, value.ToString());
				Factory.SaveChanges();
			}
		}

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

		public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		{
			get { return GetCollection<ApplicationGroup>(XmlApplicationGroup.GetApplicationGroups(Node), typeof(XmlApplicationGroup)); }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Application> Applications
		{
			get { return GetCollection<Application>(XmlApplication.GetApplications(Node), typeof(XmlApplication)); }
		}

		public XmlAdminManager(XmlElement node, XmlFactory factory)
			: base(node, factory)
		{ 

		}

		public void DeleteGroup(ApplicationGroup group)
		{
			XmlApplicationGroup.RemoveApplicationGroup(Node, group.Guid);
			Factory.SaveChanges();
		}

		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			XmlApplicationGroup ag = XmlApplicationGroup.NewApplicationGroup(Factory, name, description, groupType);
			ag.Update(Node);

			return new ApplicationGroup(ag);
		}

		public Application CreateApplication(string name, string description, string versionInformation)
		{
			XmlApplication a = XmlApplication.CreateApplication(Factory, name, description, versionInformation);
			a.Update(Node);

			return new Application(a);
		}

		public void DeleteApplication(Application application)
		{
			XmlApplication.RemoveApplication(Node, application.Guid);
			Factory.SaveChanges();
		}
	}
}
