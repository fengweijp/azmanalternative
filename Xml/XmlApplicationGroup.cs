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
		private const string LDAPQUERY = "LdapQuery";

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

		public string LdapQuery
		{
			get;
			set;
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

		protected override void LoadInternal(XmlElement element)
		{
			base.LoadInternal(element);

			IsGlobalGroup = (element.ParentNode.Name == "AzAdminManager");

			switch (GetAttribute(element, GROUPTYPE))
			{
				case "LdapQuery":
					GroupType = AzAlternative.GroupType.LdapQuery;
					XmlElement e = element[LDAPQUERY];
					if (e != null)
						LdapQuery = e.InnerXml;
					break;
				case "Basic":
					GroupType = AzAlternative.GroupType.Basic;
					break;
				default:
					throw new AzException("Unknown group type during load.");
			}

			Groups = new Collections.ApplicationGroupCollection(GetLinks(element, GROUP), true);
			Exclusions = new Collections.MemberCollection(Key, true);
			Members = new Collections.MemberCollection(Key);
		}

		public void RemoveMember(Member member)
		{
			member.Instance.Remove();
		}

		public void RemoveMember(string name)
		{
			string tmp = Member.ToSid(name);
			Member m = Members.First(item => item.Instance.Sid == tmp);

			RemoveMember(m);
		}

		public void AddMember(string name)
		{
			Member m = new Member(new XmlMember(), name);
			m.Instance.Parent = this.Key;

			if (Members.Contains(m))
				return;

			m.Instance.Save();
		}

		public void AddGroup(ApplicationGroup group)
		{
			Service.CreateLink(this, GROUP, group.Key);
		}

		public void RemoveGroup(ApplicationGroup group)
		{
			Service.RemoveLink(this, GROUP, group.Key);
		}

		public override XmlElement ToXml(XmlElement parent)
		{
			XmlElement e = parent.OwnerDocument.CreateElement(ELEMENTNAME);
			SetAttribute(e, GUID, Key);
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);
			SetAttribute(e, GROUPTYPE, GroupType.ToString());
			if (GroupType == AzAlternative.GroupType.LdapQuery)
			{
				XmlElement lq = parent.OwnerDocument.CreateElement(LDAPQUERY);
				lq.InnerText = LdapQuery;
				e.AppendChild(lq);
			}

			parent.AppendChild(e);
			
			return e;
		}

		public override XmlElement ToXml()
		{
			XmlElement e = base.ToXml();
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);

			if (GroupType == AzAlternative.GroupType.LdapQuery)
			{
				XmlElement lq = e[LDAPQUERY];
				if (lq == null)
				{
					lq = e.OwnerDocument.CreateElement(LDAPQUERY);
					e.AppendChild(lq);
				}

				lq.InnerText = LdapQuery;
			}
			else
			{
				XmlElement lq = e[LdapQuery];
				if (lq != null)
					e.RemoveChild(lq);
			}

			return e;
		}

		public static Dictionary<string, string> GetChildren(XmlElement parent)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			foreach (XmlNode item in parent.SelectNodes(ELEMENTNAME))
			{
				result.Add(item.Attributes[NAME].Value, item.Attributes[GUID].Value);
			}

			return result;
		}

	}
}
