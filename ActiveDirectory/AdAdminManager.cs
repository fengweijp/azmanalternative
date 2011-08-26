using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	internal class AdAdminManager : AdBaseObject, Interfaces.IAdminManager
	{
		private const string MAJORVERSION = "msDS-AzMajorVersion";
		private const string MINORVERSION = "msDS-AzMinorVersion";
		protected const string GROUPSCONTAINER = "AzGroupObjectContainer-";

		public int MajorVersion
		{
			get;
			set;
		}

		public int MinorVersion
		{
			get;
			set;
		}

		public Collections.ApplicationGroupCollection Groups
		{
			get;
			set;
		}

		public Collections.ApplicationCollection Applications
		{
			get;
			set;
		}

		protected override string ObjectClass
		{
			get { return "msDS-AzAdminManager"; }
		}

		public AdAdminManager()
			: base()
		{
			ChangeTrackingDisabled = true;

			SearchResultEntry en = Service.LoadRoot();
			Load(en);

			MajorVersion = int.Parse(GetAttribute(en.Attributes, MAJORVERSION));
			MinorVersion = int.Parse(GetAttribute(en.Attributes, MINORVERSION));

			Groups = AdApplicationGroup.GetCollection(GetGroupContainerName(), false);
			Applications = AdApplication.GetCollection(Key);

			ChangeTrackingDisabled = false;
		}

		public void Update()
		{
			Service.Save(GetUpdate());
		}

		protected override System.DirectoryServices.Protocols.AddRequest CreateNewThis()
		{
			AddRequest ar = base.CreateNewThis();
			
			return ar;
		}

		protected override AddRequest[] CreateChildEntries()
		{
			List<AddRequest> result = new List<AddRequest>();

			AddRequest a = new AddRequest();
			a.DistinguishedName = GetGroupContainerName();
			a.Attributes.Add(new DirectoryAttribute(OBJECTCLASS, CONTAINER));
			result.Add(a);

			return result.ToArray();
		}

		public override ModifyRequest GetUpdate()
		{
			return base.GetUpdate();
		}

		private string GetGroupContainerName()
		{
			return string.Format("cn={2}{0},{1}", this.Name, this.Key, GROUPSCONTAINER);
		}
	}
}
