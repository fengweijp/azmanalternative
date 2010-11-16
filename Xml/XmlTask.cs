using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
    internal class XmlTask : XmlBaseObject, Interfaces.ITask
    {
        private const string ELEMENTNAME = "AzApplication";
		private const string BIZRULEPATH = "BizRuleImportedPath";
		private const string BIZRULELANGUAGE = "BizRuleLanguage";
		private const string BIZRULE = "BizRule";
		private const string OPERATION = "OperationLink";
		private const string TASK = "TaskLink";

		public string BizRuleImportedPath
		{
			get;
			set;
		}

		public BizRuleLanguage BizRuleLanguage
		{
			get;
			set;
		}

		public string BizRule
		{
			get;
			set;
		}

		public Collections.TaskCollection Tasks
		{
			get;
			set;
		}

		public Collections.OperationCollection Operations
		{
			get;
			set;
		}

        public XmlTask(XmlService service)
            : base(service)
        { }

		public void AddTask(Task task)
		{
			Service.CreateLink(this, TASK, task.Guid);
		}

		public void RemoveTask(Task task)
		{
			Service.RemoveLink(this, TASK, task.Guid);
		}

        public void AddOperation(Operation operation)
        {
			Service.CreateLink(this, OPERATION, operation.Guid);
        }

        public void RemoveOperation(Operation operation)
        {
			Service.RemoveLink(this, OPERATION, operation.Guid);
        }

		public override XmlElement ToXml(XmlElement parent)
		{
			XmlElement e = parent.OwnerDocument.CreateElement(ELEMENTNAME);
			SetAttribute(e, GUID, Guid.ToString());
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);
			
			SetAttribute(e, BIZRULEPATH, BizRuleImportedPath);
			SetElement(e, BIZRULELANGUAGE, BizRuleLanguage == AzAlternative.BizRuleLanguage.Undefined ? null : BizRuleLanguage.ToString());
			SetElement(e, BIZRULE, BizRule);

			return e;
		}

		public override XmlElement ToXml()
		{
			XmlElement e = base.ToXml();
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);

			SetAttribute(e, BIZRULEPATH, BizRuleImportedPath);

			SetElement(e, BIZRULELANGUAGE, BizRuleLanguage == AzAlternative.BizRuleLanguage.Undefined ? null : BizRuleLanguage.ToString());
			SetElement(e, BIZRULE, BizRule);

			return e;
		}

		protected override void LoadInternal(XmlElement element)
		{
			base.LoadInternal(element);

			BizRuleImportedPath = GetAttribute(element, BIZRULEPATH);
			BizRule = GetElement(element, BIZRULE);
			switch (GetElement(element, BIZRULELANGUAGE))
			{
				case null:
					BizRuleLanguage = AzAlternative.BizRuleLanguage.Undefined;
					break;
				case "VBScript":
					BizRuleLanguage = AzAlternative.BizRuleLanguage.VBScript;
					break;
				case "JScript":
					BizRuleLanguage = AzAlternative.BizRuleLanguage.JScript;
					break;
				default:
					throw new AzException("Unknown Biz Rule language.");
			}

			Operations = new Collections.OperationCollection(Service, GetLinks(element, OPERATION));
			Tasks = new Collections.TaskCollection(Service, GetLinks(element, TASK));
		}

		private void SetElement(XmlElement parent, string elementName, string value)
		{
			XmlElement e = parent[elementName];
			if (e == null && value == null)
				return;
			else if (e == null)
			{
				e = parent.OwnerDocument.CreateElement(elementName);
				e.InnerText = value;
				parent.AppendChild(e);
			}
			else if (value == null)
			{
				parent.RemoveChild(e);
			}
			else
			{
				e.InnerText = value;
			}
		}

		private string GetElement(XmlElement parent, string elementName)
		{
			XmlElement e = parent[elementName];
			if (e == null)
				return null;

			return e.InnerXml;
		}

		public void LoadBizRuleScript(string path, BizRuleLanguage language)
		{
			BizRule = BizRuleLoader.LoadScript(path);
			BizRuleImportedPath = path;
			BizRuleLanguage = language;
		}

		public void ClearBizRuleScript()
		{
			BizRule = null;
			BizRuleImportedPath = null;
			BizRuleLanguage = AzAlternative.BizRuleLanguage.Undefined;
		}
	}
}
