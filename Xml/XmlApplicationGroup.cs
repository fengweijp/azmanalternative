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
		private const string GUID = "Guid";
		private const string NAME = "Name";
		private const string DESCRIPTION = "Description";
		private const string GROUPTYPE = "GroupType";

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

		public GroupType GroupType
		{
			get
			{
				string s = GetAttribute(GROUPTYPE);
				switch (s)
				{
					case "Basic":
						return AzAlternative.GroupType.Basic;
					case "LdapQuery":
						return AzAlternative.GroupType.LdapQuery;
					default:
						throw new ApplicationException("Unknown or unsupported group type");
				}
			}
			set
			{
				SetAttribute(GROUPTYPE, value.ToString());
			}
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

		public XmlApplicationGroup(XmlElement node, XmlFactory factory)
			: base(node, factory)
		{ }

		public override void Update(XmlElement Node)
		{
			Guid = new System.Guid();
			Node.AppendChild(Node);
			Factory.SaveChanges();
		}

		public static XmlNodeList GetApplicationGroups(XmlElement parent)
		{
			return parent.SelectNodes(ELEMENTNAME);
		}

		public static XmlApplicationGroup NewApplicationGroup(XmlFactory factory, string name, string description, GroupType groupType)
		{
			XmlApplicationGroup ag = new XmlApplicationGroup(factory.CreateNew(ELEMENTNAME), factory);
			ag.Name = name;
			ag.Description = description;
			ag.GroupType = groupType;

			return ag;
		}

		public static void RemoveApplicationGroup(XmlElement parent, Guid guid)
		{
			XmlNode node = parent.SelectSingleNode(string.Format("{0}[@{1}={2}]", ELEMENTNAME, GUID, guid));
			if (node == null)
				return;

			parent.RemoveChild(node);
		}

		public void AddMember(Interfaces.IMember member)
		{
			throw new NotImplementedException();
		}

		public void RemoveMember(Interfaces.IMember member)
		{
			throw new NotImplementedException();
		}
	}
}
