using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	internal class XmlOperation: XmlBaseObject, Interfaces.IOperation
	{
		private const string ELEMENTNAME = "AzOperation";
		private const string OPERATIONID = "OperationID";

		public int OperationId
		{
			get;
			set;
		}

		public XmlOperation(XmlService service)
			: base(service)
		{ }

		public override XmlElement ToXml(XmlElement parent)
		{
			XmlElement e = parent.OwnerDocument.CreateElement(ELEMENTNAME);
			SetAttribute(e, GUID, Guid.ToString());
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);
			XmlElement op = e.OwnerDocument.CreateElement(OPERATIONID);
			op.InnerText = OperationId.ToString();
			e.AppendChild(op);

			return e;
		}

		public override XmlElement ToXml()
		{
			XmlElement e = base.ToXml();
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);

			e[OPERATIONID].InnerText= OperationId.ToString();

			return e;
		}

		protected override void LoadInternal(XmlElement element)
		{
			base.LoadInternal(element);

			OperationId = int.Parse(element[OPERATIONID].InnerText);
		}

		public static Dictionary<string, Guid> GetChildren(XmlElement parent)
		{
			return GetChildren(parent, ELEMENTNAME);
		}
	}
}
