using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.ActiveDirectory
{
	internal class AdTask : AdBaseObject, Interfaces.ITask
	{
		protected override string ObjectClass
		{
			get { return "msDS-AzTask"; }
		}
		
		public string BizRuleImportedPath
		{
			get { throw new NotImplementedException(); }
		}

		public BizRuleLanguage BizRuleLanguage
		{
			get { throw new NotImplementedException(); }
		}

		public string BizRule
		{
			get { throw new NotImplementedException(); }
		}

		public Collections.TaskCollection Tasks
		{
			get { throw new NotImplementedException(); }
		}

		public Collections.OperationCollection Operations
		{
			get { throw new NotImplementedException(); }
		}

		public AdTask(AdService service)
			: base(service)
		{}

		public void AddTask(Task task)
		{
			throw new NotImplementedException();
		}

		public void RemoveTask(Task task)
		{
			throw new NotImplementedException();
		}

		public void AddOperation(Operation operation)
		{
			throw new NotImplementedException();
		}

		public void RemoveOperation(Operation operation)
		{
			throw new NotImplementedException();
		}

		public void LoadBizRuleScript(string path, BizRuleLanguage language)
		{
			throw new NotImplementedException();
		}

		public void ClearBizRuleScript()
		{
			throw new NotImplementedException();
		}
	}
}
