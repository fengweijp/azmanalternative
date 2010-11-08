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

		public XmlRoleDefinition(XmlService service)
			: base(service)
		{ }

		public void AddRole(RoleDefinition role)
		{
			Service.CreateLink(this, ROLE, role.Guid);
		}

		public void RemoveRole(RoleDefinition role)
		{
			Service.RemoveLink(this, ROLE, role.Guid);
		}
	}
}
