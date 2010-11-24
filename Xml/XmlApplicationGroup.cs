﻿using System;
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

		public Collections.MemberCollection Members
		{
			get;
			set;
		}

		public Collections.MemberCollection Exclusions
		{
			get;
			set;
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

			Groups = new Collections.ApplicationGroupCollection(Service, GetLinks(element, GROUP));
			Exclusions = Service.GetExclusions(element);
			Members = Service.GetMembers(element);
		}

		public void RemoveMember(Interfaces.IMember member)
		{
			throw new NotImplementedException();
		}

		public void RemoveMember(string name)
		{
			string tmp = Member.ToSid(name);
			Member m = Members.First(item => item.Instance.Sid == tmp);

			Members.RemoveMember(m);
		}

		public void AddMember(string name)
		{
			Member m = new Member(new XmlMember(Service), name);
			m.Instance.Parent = this.Guid;
			Members.AddMember(m);
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

			parent.AppendChild(e);
			
			return e;
		}

		public override XmlElement ToXml()
		{
			XmlElement e = base.ToXml();
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);

			return e;
		}

		public static Dictionary<string, Guid> GetChildren(XmlElement parent)
        {
            Dictionary<string, Guid> result = new Dictionary<string, Guid>();

            foreach (XmlNode item in parent.SelectNodes(ELEMENTNAME))
            {
                result.Add(item.Attributes[NAME].Value, new Guid(item.Attributes[GUID].Value));
            }

            return result;
        }

	}
}
