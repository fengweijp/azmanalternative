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

        public Collections.ApplicationGroupCollection Groups
        {
            get;
            set;
        }

        public Collections.ApplicationCollection Applications
        {
            get;
            set;
        }

		public XmlAdminManager(XmlService factory)
			: base(factory)
		{
			XmlElement e = factory.LoadRoot();
			MajorVersion = int.Parse(GetAttribute(e, MAJORVERSION));
			MinorVersion = int.Parse(GetAttribute(e, MINORVERSION));
			Description = GetAttribute(e, DESCRIPTION);
			Guid = new Guid(e.Attributes[GUID].Value);
            Applications = new Collections.ApplicationCollection(factory, XmlApplication.GetChildren(e));
            Groups = new Collections.ApplicationGroupCollection(factory, XmlApplicationGroup.GetChildren(e));
		}

		public void DeleteGroup(ApplicationGroup group)
		{
			Service.RemoveElement((XmlApplicationGroup)group.Instance);
		}

		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			XmlApplicationGroup ag = new XmlApplicationGroup(Service);
			ag.Guid = System.Guid.NewGuid();
			ag.Name = name;
			ag.Description = description;
			ag.GroupType = groupType;
			ag.IsGlobalGroup = true;

			ag.Groups = new Collections.ApplicationGroupCollection(Service);

			XmlElement root = Service.LoadRoot();
			Service.Save(ag.ToXml(root));

			return new ApplicationGroup(ag);
		}

		public void UpdateGroup(ApplicationGroup group)
		{
			XmlApplicationGroup ag = (XmlApplicationGroup)group.Instance;
			Service.Save(ag);
		}

		public Application CreateApplication(string name, string description, string versionInformation)
		{
			XmlApplication a = new XmlApplication(Service);
			a.Guid = System.Guid.NewGuid();
			a.Name = name;
			a.Description = description;
			a.ApplicationVersion = versionInformation;
			
			a.Groups = new Collections.ApplicationGroupCollection(Service);
			a.Operations = new Collections.OperationCollection(Service);
			a.Tasks = new Collections.TaskCollection(Service);
			a.Roles = new Collections.RoleDefinitionCollection(Service);
			a.RoleAssignments = new Collections.RoleAssignmentsCollection(Service);

			XmlElement root = Service.LoadRoot();
			Service.Save(a.ToXml(root));

			return new Application(a);
		}

		public void DeleteApplication(Application application)
		{
			Service.RemoveElement((XmlApplication)application.Instance);
		}

		public void UpdateApplication(Application application)
		{
			XmlApplication a = (XmlApplication)application.Instance;
			Service.Save(a);
		}

		public void Update()
		{
			Service.Save(this);
		}

		public override XmlElement ToXml()
		{
			XmlElement e = base.ToXml();
			SetAttribute(e, DESCRIPTION, Description);

			return e;
		}

		public override XmlElement ToXml(XmlElement parent)
		{
			throw new NotSupportedException();
		}
	}
}
