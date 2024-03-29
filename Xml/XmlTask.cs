﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	internal class XmlTask : XmlBaseObject, Interfaces.ITask
	{
		protected const string ELEMENTNAME = "AzTask";
		protected const string BIZRULEPATH = "BizRuleImportedPath";
		protected const string BIZRULELANGUAGE = "BizRuleLanguage";
		protected const string BIZRULE = "BizRule";
		protected const string OPERATION = "OperationLink";
		protected const string TASK = "TaskLink";
		protected const string ROLEDEFINITION = "RoleDefinition";

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

		public void AddTask(Task task)
		{
			Service.CreateLink(this, TASK, task.Key);
		}

		public void RemoveTask(Task task)
		{
			Service.RemoveLink(this, TASK, task.Key);
		}

		public void AddOperation(Operation operation)
		{
			Service.CreateLink(this, OPERATION, operation.Key);
		}

		public void RemoveOperation(Operation operation)
		{
			Service.RemoveLink(this, OPERATION, operation.Key);
		}

		public override XmlElement ToXml(XmlElement parent)
		{
			XmlElement e = parent.OwnerDocument.CreateElement(ELEMENTNAME);
			SetAttribute(e, GUID, Key);
			SetAttribute(e, NAME, Name);
			SetAttribute(e, DESCRIPTION, Description);
			
			SetAttribute(e, BIZRULEPATH, BizRuleImportedPath);
			SetElement(e, BIZRULELANGUAGE, BizRuleLanguage == AzAlternative.BizRuleLanguage.Undefined ? null : BizRuleLanguage.ToString());
			SetElement(e, BIZRULE, BizRule);

			parent.AppendChild(e);

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

			Operations = new Collections.OperationCollection(GetLinks(element, OPERATION), true);
			Tasks = new Collections.TaskCollection(GetLinks(element, TASK), true);
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

		public static Dictionary<string, string> GetTasks(XmlElement element)
		{
			return GetChildren(element, string.Format("{0}[not(@{1}) or @{1}!='True']", ELEMENTNAME, ROLEDEFINITION));
		}
	}
}
