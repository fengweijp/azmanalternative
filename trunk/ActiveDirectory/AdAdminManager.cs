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

		public AdAdminManager(AdService service)
			: base(service)
		{
			ChangeTrackingDisabled = true;

			SearchResultEntry en = Service.LoadRoot();
			Load(en);

			MajorVersion = int.Parse(GetAttribute(en.Attributes, MAJORVERSION));
			MinorVersion = int.Parse(GetAttribute(en.Attributes, MINORVERSION));

			Groups = AdApplicationGroup.GetCollection(Service, GetGroupContainerName(), false);
			Applications = AdApplication.GetCollection(Service, Key);

			ChangeTrackingDisabled = false;
		}

		public ApplicationGroup CreateGroup(string name, string description, GroupType groupType)
		{
			AdApplicationGroup ag = new AdApplicationGroup(Service);
			ag.Name = name;
			ag.Description = description;
			ag.GroupType = groupType;
			ag.ContainerDn = GROUPSCONTAINER + this.Name + "," + this.Key;
			ag.IsGlobalGroup = true;

			ag.Groups = new Collections.ApplicationGroupCollection(Service, true);

			Service.Save(ag.CreateNew());

			return new ApplicationGroup(ag);
		}

		public void DeleteGroup(ApplicationGroup group)
		{
			((AdApplicationGroup)group.Instance).Delete();
		}

		public void UpdateGroup(ApplicationGroup group)
		{
			AdApplicationGroup ag = (AdApplicationGroup)group.Instance;
			Service.Save(ag.GetUpdate());
		}

		public Application CreateApplication(string name, string description, string versionInformation)
		{
			AdApplication a = new AdApplication(Service);
			a.Name = name;
			a.Description = description;
			a.ApplicationVersion = versionInformation;

			a.Groups = new Collections.ApplicationGroupCollection(Service, false);
			a.Operations = new Collections.OperationCollection(Service, false);
			a.RoleAssignments = new Collections.RoleAssignmentsCollection(Service);
			a.Roles = new Collections.RoleDefinitionCollection(Service, false);

			Service.Save(a.CreateNew());

			return new Application(a);
		}

		public void DeleteApplication(Application application)
		{
			((AdApplication)application.Instance).Delete();
		}

		public void UpdateApplication(Application application)
		{
			AdApplication a = (AdApplication)application.Instance;
			Service.Save(a.GetUpdate());
		}

		public void Update()
		{
			Service.Save(GetUpdate());
		}

		public override System.DirectoryServices.Protocols.AddRequest CreateNew()
		{
			AddRequest ar = base.CreateNew();
			
			return ar;
		}

		public override AddRequest[] CreateChildEntries()
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
