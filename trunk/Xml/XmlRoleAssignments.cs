using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	internal class XmlRoleAssignments : XmlBaseObject, Interfaces.IRoleAssignment
	{
		private const string ELEMENTNAME = "AzRole";
		private const string GROUP = "AppMemberLink";
		private const string MEMBER = "Member";
		private const string DEFINITION = "TaskLink";

		private string _DefinitionId;
		private RoleDefinition _Definition;

		public RoleDefinition Definition
		{
			get
			{
				if (_Definition == null)
				{
					_Definition = Service.GetRoleDefinition(_DefinitionId);
				}
				return _Definition;
			}
			set
			{
				_Definition = value;
				_DefinitionId = value.UniqueName;
			}
		}

		public Collections.ApplicationGroupCollection Groups
		{
			get;
			set;
		}

		public Collections.MemberCollection Members
		{
			get;
			set;
		}

		public XmlRoleAssignments(XmlService service)
			: base(service)
		{ }

		public void AddGroup(ApplicationGroup group)
		{
			Service.CreateLink(this, GROUP, group.UniqueName);
		}

		public void RemoveGroup(ApplicationGroup group)
		{
			Service.RemoveLink(this, GROUP, group.UniqueName);
		}

		public void RemoveMember(Member member)
		{
			Members.RemoveMember(member);
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
			m.Instance.Parent = this.UniqueName;
			Members.AddMember(m);
		}

		public override XmlElement ToXml(XmlElement parent)
		{
			XmlElement e = parent.OwnerDocument.CreateElement(ELEMENTNAME);
			SetAttribute(e, GUID, UniqueName);
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);

			XmlElement def = e.OwnerDocument.CreateElement(DEFINITION);
			def.InnerXml = _DefinitionId.ToString();
			e.AppendChild(def);

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

		protected override void LoadInternal(XmlElement element)
		{
			base.LoadInternal(element);

			_DefinitionId = element[DEFINITION].InnerXml;

			Groups = new Collections.ApplicationGroupCollection(Service, GetLinks(element, GROUP), true);
			Members = Service.GetMembers(element);
		}

		public static Dictionary<string, string> GetChildren(XmlElement element)
		{
			return GetChildren(element, ELEMENTNAME);
		}
	}
}
