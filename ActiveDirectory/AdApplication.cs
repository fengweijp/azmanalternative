using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	internal class AdApplication : AdBaseObject, Interfaces.IApplication
	{
		private const string GROUPSCONTAINER = "AzGroupObjectConatiner-";
		private const string OPSCONTAINER = "AzOpObjectConatiner-";
		private const string ROLESCONTAINER = "AzRoleObjectConatiner-";
		private const string TASKSCONTAINER = "AzTaskObjectConatiner-";
		private const string APPNAME = "msDS-AzApplicationName";
		private const string VERSION = "msDS-AzApplicationVersion";
		private const string CLASSNAME = "msDS-AzApplication";

		private string _ApplicationVersion;

		protected override string ObjectClass
		{
			get { return CLASSNAME; }
		}

		protected override string NameAttribute
		{
			get { return APPNAME; }
		}

		public string ApplicationVersion
		{
			get { return _ApplicationVersion; }
			set
			{
				OnPropertyChanged(VERSION, _ApplicationVersion, value);
				_ApplicationVersion = value;
			}
		}

		public string CN
		{
			get;
			set;
		}

		public AdApplication()
			:base()
		{
			CN = Guid.NewGuid().ToString();
		}

		public Collections.RoleDefinitionCollection Roles
		{
			get;
			set;
		}

		public Collections.RoleAssignmentsCollection RoleAssignments
		{
			get;
			set;
		}

		public Collections.ApplicationGroupCollection Groups
		{
			get;
			set;
		}

		public Collections.OperationCollection Operations
		{
			get;
			set;
		}

		public Collections.TaskCollection Tasks
		{
			get;
			set;
		}

		protected override AddRequest[] CreateChildEntries()
		{
			List<AddRequest> result = new List<AddRequest>();

			AddRequest a = new AddRequest();
			a.DistinguishedName = string.Format("cn={0}{1},{2}", GROUPSCONTAINER, "", this.Key);
			a.Attributes.Add(new DirectoryAttribute(OBJECTCLASS, CONTAINER));
			result.Add(a);

			a = new AddRequest();
			a.DistinguishedName = string.Format("cn={0}{1},{2}", OPSCONTAINER, "", this.Key);
			a.Attributes.Add(new DirectoryAttribute(OBJECTCLASS, CONTAINER));
			result.Add(a);

			a = new AddRequest();
			a.DistinguishedName = string.Format("cn={0}{1},{2}", ROLESCONTAINER, "", this.Key);
			a.Attributes.Add(new DirectoryAttribute(OBJECTCLASS, CONTAINER));
			result.Add(a);

			a = new AddRequest();
			a.DistinguishedName = string.Format("cn={0}{1},{2}", TASKSCONTAINER, "", this.Key);
			a.Attributes.Add(new DirectoryAttribute(OBJECTCLASS, CONTAINER));
			result.Add(a);

			return result.ToArray();
		}

		public override void Load(SearchResultEntry entry)
		{
			ChangeTrackingDisabled = true;

			base.Load(entry);

			CN = GetAttribute(entry.Attributes, "name");
			ApplicationVersion = GetAttribute(entry.Attributes, VERSION);
			ContainerDn = Key.Substring(4 + CN.Length);

			Groups = AdApplicationGroup.GetCollection(string.Format("{0}{2},{1}", GROUPSCONTAINER, Key, CN), false);
			Roles = new Collections.RoleDefinitionCollection(false);
			RoleAssignments = new Collections.RoleAssignmentsCollection();
			Tasks = new Collections.TaskCollection(false);
			Operations = AdOperation.GetCollection(string.Format("{0}{1},{2}", OPSCONTAINER, CN, Key), false);

			ChangeTrackingDisabled = false;
		}

		protected override AddRequest CreateNewThis()
		{
			AddRequest ar = base.CreateNewThis();
			ar.DistinguishedName = string.Format("cn={0},{1}", CN, ContainerDn);
			ar.Attributes.Add(CreateAttribute(APPNAME, Name));

			if (!string.IsNullOrEmpty(ApplicationVersion))
				ar.Attributes.Add(CreateAttribute(VERSION, ApplicationVersion));

			return ar;
		}

		public static Collections.ApplicationCollection GetCollection(string key)
		{
			var results = ((AdService)Locator.Service).Load(key, "(ObjectClass=" + CLASSNAME + ")");
			var q = from i in results.Cast<SearchResultEntry>() select i;
			return new Collections.ApplicationCollection(q.ToDictionary(x => x.Attributes[APPNAME][0].ToString(), x => x.DistinguishedName));
		}
	}
}
