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
		private const string GUID = "Guid";
		private const string NAME = "Name";
		private const string DESCRIPTION = "Description";
		private const string OPERATIONID = "OperationID";

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

		public int OperationId
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

		public XmlOperation(XmlElement node, XmlFactory factory)
			: base(node, factory)
		{ }

	}
}
