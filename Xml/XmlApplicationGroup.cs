using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	internal class XmlApplicationGroup : XmlBaseObject, Interfaces.IApplicationGroup
	{
		private const string ELEMENTNAME = "AzApplicationGroup";
		private const string GROUPTYPE = "GroupType";
		private const string GROUP = "AppMemberLink";

		public GroupType GroupType
		{
			get;
			set;
		}

		public bool IsGlobalGroup
		{
			get;
			internal set;
		}

		public List<Interfaces.IMember> Members
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

        public Collections.ApplicationGroupCollection Groups
        {
            get;
            set;
        }

		public XmlApplicationGroup(XmlService service)
			: base(service)
		{ }

		protected override void LoadInternal(XmlElement element)
		{
			base.LoadInternal(element);

			IsGlobalGroup = (element.ParentNode.Name == "AzAdminManager");

			switch (GetAttribute(element, GROUPTYPE))
			{
				case "LdapQuery":
					GroupType = AzAlternative.GroupType.LdapQuery;
					break;
				case "Basic":
					GroupType = AzAlternative.GroupType.Basic;
					break;
				default:
					throw new AzException("Unknown group type during load.");
			}
		}

		//public static XmlNodeList GetApplicationGroups(XmlElement parent)
		//{
		//    return parent.SelectNodes(ELEMENTNAME);
		//}

		//public static XmlApplicationGroup NewApplicationGroup(XmlFactory factory, string name, string description, GroupType groupType)
		//{
		//    XmlApplicationGroup ag = new XmlApplicationGroup(factory.CreateNew(ELEMENTNAME), factory);
		//    ag.Name = name;
		//    ag.Description = description;
		//    ag.GroupType = groupType;

		//    return ag;
		//}

		//public static void RemoveApplicationGroup(XmlElement parent, Guid guid)
		//{
		//    XmlNode node = parent.SelectSingleNode(string.Format("{0}[@{1}={2}]", ELEMENTNAME, GUID, guid));
		//    if (node == null)
		//        return;

		//    parent.RemoveChild(node);
		//}

		public void AddMember(Interfaces.IMember member)
		{
			throw new NotImplementedException();
		}

		public void RemoveMember(Interfaces.IMember member)
		{
			throw new NotImplementedException();
		}

        public void AddGroup(ApplicationGroup group)
        {
			Service.CreateLink(this, GROUP, group.Guid);
        }

        public void RemoveGroup(ApplicationGroup group)
        {
			Service.RemoveLink(this, GROUP, group.Guid);
		}

		public override XmlElement ToXml(XmlElement parent)
		{
			XmlElement e = parent.OwnerDocument.CreateElement(ELEMENTNAME);
			SetAttribute(e, GUID, Guid.ToString());
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);
			SetAttribute(e, GROUPTYPE, GroupType.ToString());

			return e;
		}

		public override XmlElement ToXml()
		{
			XmlElement e = base.ToXml();
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);

			return e;
		}

		//public static IEnumerator<ApplicationGroup> GetGroups(XmlService service, IEnumerable<Guid> guids, AdminManager store, Application app)
		//{
		//    if (guids.Count() == 0)
		//        return null;

		//    XmlElement parent = service.Load(guids[0]);

		//    foreach (XmlNode item in parent.OwnerDocument.SelectNodes(string.Format( ELEMENTNAME))
		//    {
		//        XmlApplicationGroup g = new XmlApplicationGroup(service);
		//        g.Load((XmlElement)item);

		//        ApplicationGroup result = new ApplicationGroup(g);
		//        if (g.IsGlobalGroup)
		//            result.Store = store;
		//        else
		//            result.Application = app;

		//        yield return result;
		//    }
		//}

        public static Dictionary<string, Guid> GetChildren(XmlElement parent)
        {
            Dictionary<string, Guid> result = new Dictionary<string, Guid>();

            foreach (XmlNode item in parent.SelectNodes(ELEMENTNAME))
            {
                result.Add(item.Attributes[NAME].Value, new Guid(item.Attributes[GUID].Value));
            }

            return result;
        }

		//public static Dictionary<string, Guid> GetLinks
	}
}
