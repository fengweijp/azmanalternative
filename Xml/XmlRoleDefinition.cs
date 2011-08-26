using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	internal class XmlRoleDefinition : XmlTask, Interfaces.IRoleDefinition
	{
		private const string ROLE = "TaskLink";

		public Collections.RoleDefinitionCollection Roles
		{
			get;
			set;
		}

		public void AddRole(RoleDefinition role)
		{
			Service.CreateLink(this, ROLE, role.Key);
		}

		public void RemoveRole(RoleDefinition role)
		{
			Service.RemoveLink(this, ROLE, role.Key);
		}

		protected override void LoadInternal(XmlElement element)
		{
			base.LoadInternal(element);
			Roles = new Collections.RoleDefinitionCollection(GetLinks(element, ROLE), true);
		}

		public override XmlElement ToXml(XmlElement parent)
		{
			XmlElement e = base.ToXml(parent);
			SetAttribute(e, ROLEDEFINITION, "True");

			return e;
		}

		public static Dictionary<string, string> GetRoles(XmlElement element)
		{
			return GetChildren(element, string.Format("{0}[@{1}='True']", ELEMENTNAME, ROLEDEFINITION));
		}
	}
}
