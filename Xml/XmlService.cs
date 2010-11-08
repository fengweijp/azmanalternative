using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	/// <summary>
	/// Wraps up access to the underlying store & provides helper method to access it
	/// </summary>
	internal class XmlService : ServiceBase
	{
		public XmlService(string connectionString)
		{
			ConnectionString = connectionString;
		}

		public override AdminManager GetAdminManager()
		{
			return new AdminManager(new XmlAdminManager(this));
		}

		public XmlElement LoadRoot()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(ConnectionString);

			return doc.DocumentElement;
		}

		public XmlElement Load(Guid guid)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(ConnectionString);

			XmlElement e = (XmlElement)doc.SelectSingleNode(string.Format("*[Guid={0}]", guid));
			if (e == null)
				throw new AzException("The Guid was not found in the store.");

			return e;
		}

		public void Save(XmlBaseObject o)
		{
			XmlElement e = o.ToXml();
			e.OwnerDocument.Save(ConnectionString);
		}

		public void Save(XmlElement node)
		{
			node.OwnerDocument.Save(ConnectionString);
		}

		public void RemoveElement(XmlBaseObject o)
		{
			XmlElement e = Load(o.Guid);
			e.ParentNode.RemoveChild(e);
			Save(e);
		}

		public void CreateLink(XmlBaseObject parent, string linkName, Guid guid)
		{
			XmlElement parentNode = Load(parent.Guid);

			if (parentNode.SelectNodes(string.Format("{0}=\"{1}\"", linkName, guid)).Count > 0)
				return;

			XmlElement e = parentNode.OwnerDocument.CreateElement(linkName);
			e.InnerText = guid.ToString();
			parentNode.AppendChild(e);
			Save(parentNode);
		}

		public void RemoveLink(XmlBaseObject parent, string linkName, Guid guid)
		{
			XmlElement parentNode = Load(parent.Guid);
			XmlNode e = parentNode.SelectSingleNode(string.Format("{0}=\"{1}\"", linkName, guid));
			if (e == null)
				return;

			parentNode.RemoveChild(e);
			Save(parentNode);
		}
	}
}
