using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	internal class XmlMember : XmlBaseObject, Interfaces.IMember
	{
		private const string MEMBER = "Member";
		private const string NONMEMBER = "NonMember";
		private const string LDAPQUERY = "LdapQuery";

		public string Sid
		{
			get;
			set;
		}

		public bool IsExclusion
		{
			get;
			set;
		}

		public Guid Parent
		{
			get;
			set;
		}

		public XmlMember(XmlService service)
			: base(service)
		{
		}

		public override System.Xml.XmlElement ToXml(System.Xml.XmlElement parent)
		{
			XmlElement e;
			if (IsExclusion)
				e = parent.OwnerDocument.CreateElement(NONMEMBER);
			else
				e = parent.OwnerDocument.CreateElement(MEMBER);

			e.InnerText = Sid;

			parent.AppendChild(e);

			return e;
		}

		public override XmlElement ToXml()
		{
			throw new NotSupportedException();
		}

		public override void Load(XmlElement element)
		{
			IsExclusion = element.Name == "NonMember";
			Sid = element.InnerText;
			Parent = new System.Guid(element.ParentNode.Attributes["Guid"].Value);
		}

		public void Save()
		{
			XmlElement parent = Service.Load(Parent);
			Service.Save(ToXml(parent));
		}


		public void Remove()
		{
			XmlElement parent = Service.Load(Parent);
			XmlElement e = (XmlElement)parent.SelectSingleNode(string.Format("{1}[. = '{0}']", Sid, IsExclusion? NONMEMBER : MEMBER));

			if (e == null)
				return;

			parent.RemoveChild(e);
			Service.Save(parent);
		}
	}
}
