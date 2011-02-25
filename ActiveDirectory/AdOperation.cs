﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	internal class AdOperation : AdBaseObject, Interfaces.IOperation
	{
		private const string OPERATIONID = "msDS-AzOperationID";
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
			get { return "msDS-AzOperation"; }
		}

		public AdOperation(AdService service)
			: base(service)
		{ }

		public override void Load(SearchResultEntry entry)
		{
			base.Load(entry);

			OperationId = int.Parse(GetAttribute(entry.Attributes, OPERATIONID));
		}

		public override ModifyRequest GetUpdate()
		{
			ModifyRequest mr = base.GetUpdate();
			SetAttribute(mr.Modifications, OPERATIONID, OperationId.ToString());

			Changes.Clear();
			return mr;
		}

		public override AddRequest CreateNew()
		{
			AddRequest ar = base.CreateNew();

			ar.Attributes.Add(CreateAttribute(OPERATIONID, OperationId.ToString()));

			return ar;
		}
	}
}
