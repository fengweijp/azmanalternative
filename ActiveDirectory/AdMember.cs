using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	internal class AdMember : AdBaseObject, Interfaces.IMember
	{
		private const string SID = "objectSid";

		protected override string ObjectClass
		{
			get { throw new NotImplementedException(); }
		}

		public string Sid
		{
			get;
			set;
		}

		public bool IsExclusion
		{
			get;
			set;
		}

		public string Parent
		{
			get;
			set;
		}

		public void Save()
		{
			throw new NotImplementedException();
		}

		public void Remove()
		{
			SearchResultEntry en = Service.Load(Parent);
		}

		public override void Load(System.DirectoryServices.Protocols.SearchResultEntry entry)
		{
			ChangeTrackingDisabled = true;
			base.Load(entry);

			Sid = GetAttribute(entry.Attributes, SID);
			Parent = entry.DistinguishedName;
			ChangeTrackingDisabled = false;
		}
	}
}
