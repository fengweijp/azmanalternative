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

		public AdAdminManager(AdService service)
			: base(service)
		{
			SearchResultEntry en = Service.LoadRoot();
			MajorVersion = int.Parse(GetAttribute(en.Attributes, MAJORVERSION));
			MinorVersion = int.Parse(GetAttribute(en.Attributes, MINORVERSION));
		}

		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			throw new NotImplementedException();
		}

		public void DeleteGroup(ApplicationGroup group)
		{
			throw new NotImplementedException();
		}

		public void UpdateGroup(ApplicationGroup group)
		{
			throw new NotImplementedException();
		}

		public Application CreateApplication(string name, string description, string versionInformation)
		{
			throw new NotImplementedException();
		}

		public void DeleteApplication(Application application)
		{
			throw new NotImplementedException();
		}

		public void UpdateApplication(Application application)
		{
			throw new NotImplementedException();
		}

		public new void Update()
		{
			Service.Save(GetUpdate());
		}

		protected override System.DirectoryServices.Protocols.AddRequest CreateNew()
		{
			AddRequest ar = base.CreateNew();
			
			ar.Attributes.Add(CreateAttribute("name", Name));

			return ar;
		}

		protected override ModifyRequest GetUpdate()
		{
			return base.GetUpdate();
		}
	}
}
