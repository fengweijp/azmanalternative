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
                if (Node[OPERATIONID] == null)
                    return 0;

                return int.Parse(Node[OPERATIONID].InnerText);
			}
			set
			{
                XmlElement e = Node[OPERATIONID];
                if (e == null)
                {
                    e = Factory.CreateNew(OPERATIONID);
                    Node.AppendChild(e);
                }
                e.InnerText = value.ToString();
			}
		}

		public XmlOperation(XmlElement node, XmlFactory factory)
			: base(node, factory)
		{ }

        public static XmlNodeList GetOperations(XmlElement parent)
        {
            return parent.SelectNodes(ELEMENTNAME);
        }

        public static XmlOperation CreateOperation(XmlFactory factory, string name, string description, int operationId)
        {
            XmlOperation o = new XmlOperation(factory.CreateNew(ELEMENTNAME), factory);
            o.Guid = Guid.NewGuid();
            o.Name = name;
            o.Description = description;
            o.OperationId = operationId;

            return o;
        }

        public static void RemoveOperation(XmlElement parent, Guid guid)
        {
            XmlNode n = parent.SelectSingleNode(string.Format("{0}[@{1}={2}]", ELEMENTNAME, GUID, guid));
            if (n == null)
                return;

            parent.RemoveChild(n);
        }
	}
}
