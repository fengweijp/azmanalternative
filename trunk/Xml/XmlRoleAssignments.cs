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

		private Guid _DefinitionGuid;

		public Interfaces.IRoleDefinition Definition
		{
			get { throw new NotImplementedException(); }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<ApplicationGroup> Groups
		{
			get { throw new NotImplementedException(); }
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<Interfaces.IMember> Members
		{
			get { throw new NotImplementedException(); }
		}

		public XmlRoleAssignments(XmlService service)
			: base(service)
		{ }

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

			XmlElement def = e.OwnerDocument.CreateElement(DEFINITION);
			def.InnerXml = _DefinitionGuid.ToString();
			e.AppendChild(def);

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

			_DefinitionGuid = new Guid(element[DEFINITION].InnerXml);
		}
	}
}
