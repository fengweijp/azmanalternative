using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	internal class AdTask : AdBaseObject, Interfaces.ITask
	{
		private const string BIZRULE = "msDS-AzBizRule";
		private const string BIZRULELANGUAGE = "msDS-AzBizRuleLanguage";
		private const string BIZRULEPATH = "msDS-AzLastImportedBizRulePath";
		private const string OPERATIONS = "msDS-OperationsForAzTask";
		protected const string TASKS = "msDS-TasksForAzTask";
		protected const string CLASSNAME = "msDS-AzTask";

		private string _BizRuleImportedPath;
		private BizRuleLanguage _BizRuleLanguage;
		private string _BizRule;

		protected override string ObjectClass
		{
			get { return CLASSNAME; }
		}
		
		public string BizRuleImportedPath
		{
			get { return _BizRuleImportedPath; }
			set
			{
				OnPropertyChanged(BIZRULEPATH, _BizRuleImportedPath, value);
				_BizRuleImportedPath = value;
			}
		}

		public BizRuleLanguage BizRuleLanguage
		{
			get { return _BizRuleLanguage; }
			set
			{
				OnPropertyChanged(BIZRULELANGUAGE, _BizRuleLanguage.ToString(), value.ToString());
				_BizRuleLanguage = value;
			}
		}

		public string BizRule
		{
			get { return _BizRule; }
			set
			{
				OnPropertyChanged(BIZRULE, _BizRule, value);
				_BizRule = value;
			}
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
			Service.UpdateListAttribute(Key, TASKS, task.Key, DirectoryAttributeOperation.Add);
		}

		public void RemoveTask(Task task)
		{
			Service.UpdateListAttribute(Key, TASKS, task.Key, DirectoryAttributeOperation.Delete);
		}

		public void AddOperation(Operation operation)
		{
			Service.UpdateListAttribute(Key, OPERATIONS, operation.Key, DirectoryAttributeOperation.Add);
		}

		public void RemoveOperation(Operation operation)
		{
			Service.UpdateListAttribute(Key, OPERATIONS, operation.Key, DirectoryAttributeOperation.Delete);
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

		public override void Load(SearchResultEntry entry)
		{
			ChangeTrackingDisabled = true;

			base.Load(entry);

			BizRule = GetAttribute(entry.Attributes, BIZRULE);
			string tmp = GetAttribute(entry.Attributes, BIZRULELANGUAGE);
			if (!string.IsNullOrEmpty(tmp))
				BizRuleLanguage = (BizRuleLanguage)Enum.Parse(typeof(BizRuleLanguage), tmp);
			BizRuleImportedPath = GetAttribute(entry.Attributes, BIZRULEPATH);

			Operations = new Collections.OperationCollection(GetValues(entry.Attributes, OPERATIONS), true);
			Tasks = GetTasks(this.Key, true);

			ChangeTrackingDisabled = false;
		}

		protected override System.DirectoryServices.Protocols.AddRequest CreateNewThis()
		{
			AddRequest ar = base.CreateNewThis();

			if (!string.IsNullOrEmpty(BizRule))
			{
				ar.Attributes.Add(CreateAttribute(BIZRULE, BizRule));
				ar.Attributes.Add(CreateAttribute(BIZRULELANGUAGE, BizRuleLanguage.ToString()));
				ar.Attributes.Add(CreateAttribute(BIZRULEPATH, BizRuleImportedPath));
			}

			return ar;
		}

		public override DirectoryRequest[] GetUpdate()
		{
			var result = base.GetUpdate();
			ModifyRequest mr = (ModifyRequest)result[result.Length - 1];

			SetAttribute(mr.Modifications, BIZRULE, BizRule);
			SetAttribute(mr.Modifications, BIZRULELANGUAGE, BizRuleLanguage.ToString());
			SetAttribute(mr.Modifications, BIZRULEPATH, BizRuleImportedPath);

			Changes.Clear();
			return result;
		}

		public static Collections.TaskCollection GetTasks(string key,bool isChildList)
		{
			string searchbase = key;
			string filter = "(&(ObjectClass=" + CLASSNAME + ")(!msDS-AzTaskIsRoleDefinition=*))";
			
			if (isChildList)
			{
				searchbase = key.Substring(key.IndexOf(",") + 1);
				filter = "(&(msDS-TasksForAzTaskBL=" + key + ")(!msDS-AzTaskIsRoleDefinition=*))";
			}

			var results = ((AdService)Locator.Service).Load(searchbase, filter);
			var q = from i in results.Cast<SearchResultEntry>() select i;
			return new Collections.TaskCollection(q.ToDictionary(x => x.Attributes["name"][0].ToString(), x => x.DistinguishedName), isChildList);
		}
	}
}
