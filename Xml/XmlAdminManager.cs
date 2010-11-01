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
			get;
			set;
		}

		public int MinorVersion
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		//public Guid Guid
		//{
		//    get;
		//    set;
		//}

		public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		{
			get { throw new NotImplementedException(); }
			//get { return GetCollection<ApplicationGroup>(XmlApplicationGroup.GetApplicationGroups(Node), typeof(XmlApplicationGroup)); }
		}

		//public System.Collections.ObjectModel.ReadOnlyCollection<Application> Applications
		//{
		//    get { return GetCollection<Application>(XmlApplication.GetApplications(Node), typeof(XmlApplication)); }
		//}

		public XmlAdminManager(XmlService factory)
			: base(factory)
		{
			XmlElement e = factory.LoadRoot();
			MajorVersion = int.Parse(e.Attributes[MAJORVERSION].Value);
			MinorVersion = int.Parse(e.Attributes[MINORVERSION].Value);
			Description = e.Attributes[DESCRIPTION].Value;
			Guid = new Guid(e.Attributes[GUID].Value);
		}

		public void DeleteGroup(ApplicationGroup group)
		{
			//XmlApplicationGroup.RemoveApplicationGroup(Node, group.Guid);
			//Factory.SaveChanges();
		}

		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			throw new NotImplementedException();
			//XmlApplicationGroup ag = XmlApplicationGroup.NewApplicationGroup(Factory, name, description, groupType);
			//ag.Update(Node);

			//return new ApplicationGroup(ag);
		}

		public void UpdateGroup(ApplicationGroup group)
		{
			throw new NotImplementedException();
		}

		//public Application CreateApplication(string name, string description, string versionInformation)
		//{
		//    XmlApplication a = XmlApplication.CreateApplication(Factory, name, description, versionInformation);
		//    a.Update(Node);

		//    return new Application(a);
		//}

		//public void DeleteApplication(Application application)
		//{
		//    XmlApplication.RemoveApplication(Node, application.Guid);
		//    Factory.SaveChanges();
		//}

		public void Update()
		{
			Factory.Save(this);
		}

		public override XmlElement ToXml()
		{
			XmlElement e = base.ToXml();
			e.Attributes[DESCRIPTION].Value = Description;

			return e;
		}
	}
}
