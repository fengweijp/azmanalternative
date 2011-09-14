using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	internal class AdOperation : AdBaseObject, Interfaces.IOperation
	{
		private const string OPERATIONID = "msDS-AzOperationID";
		private const string CLASSNAME = "msDS-AzOperation";

		private int _OperationId;

		public int OperationId
		{
			get { return _OperationId; }
			set
			{
				OnPropertyChanged(OPERATIONID, _OperationId.ToString(), value.ToString());
				_OperationId = value;
			}
		}

		protected override string ObjectClass
		{
			get { return CLASSNAME; }
		}

		public override void Load(SearchResultEntry entry)
		{
			ChangeTrackingDisabled = true;

			base.Load(entry);
			OperationId = int.Parse(GetAttribute(entry.Attributes, OPERATIONID));
			
			ChangeTrackingDisabled = false;
		}

		public override DirectoryRequest[] GetUpdate()
		{
			var result = base.GetUpdate();
			ModifyRequest mr = (ModifyRequest)result[result.Length - 1];
			SetAttribute(mr.Modifications, OPERATIONID, OperationId.ToString());

			Changes.Clear();
			return result;
		}

		protected override AddRequest CreateNewThis()
		{
			AddRequest ar = base.CreateNewThis();

			ar.Attributes.Add(CreateAttribute(OPERATIONID, OperationId.ToString()));

			return ar;
		}

		public static Collections.OperationCollection GetCollection(string key, bool linked)
		{
			var results = ((AdService)Locator.Service).Load(key, "(ObjectClass=" + CLASSNAME + ")");
			var q = from i in results.Cast<SearchResultEntry>() select i;
			return new Collections.OperationCollection(q.ToDictionary(x => x.Attributes["cn"][0].ToString(), x => x.DistinguishedName), linked);
		}
	}
}
