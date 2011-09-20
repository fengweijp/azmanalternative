using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	internal class AdApplication : AdBaseObject, Interfaces.IApplication
	{
		private const string GROUPSCONTAINER = "AzGroupObjectContainer-";
		private const string OPSCONTAINER = "AzOpObjectContainer-";
		private const string ROLESCONTAINER = "AzRoleObjectContainer-";
		private const string TASKSCONTAINER = "AzTaskObjectContainer-";
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
			a.DistinguishedName = string.Format("cn={0}{1},cn={1},{2}", GROUPSCONTAINER, CN, this.ContainerDn);
			a.Attributes.Add(new DirectoryAttribute(OBJECTCLASS, CONTAINER));
			result.Add(a);

			a = new AddRequest();
			a.DistinguishedName = string.Format("cn={0}{1},cn={1},{2}", OPSCONTAINER, CN, this.ContainerDn);
			a.Attributes.Add(new DirectoryAttribute(OBJECTCLASS, CONTAINER));
			result.Add(a);

			a = new AddRequest();
			a.DistinguishedName = string.Format("cn={0}{1},cn={1},{2}", ROLESCONTAINER, CN, this.ContainerDn);
			a.Attributes.Add(new DirectoryAttribute(OBJECTCLASS, CONTAINER));
			result.Add(a);

			a = new AddRequest();
			a.DistinguishedName = string.Format("cn={0}{1},cn={1},{2}", TASKSCONTAINER, CN, this.ContainerDn);
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

			Groups = AdApplicationGroup.GetCollection(string.Format("cn={0}{2},{1}", GROUPSCONTAINER, Key, CN), false);
			Roles = AdRoleDefinition.GetRoles(string.Format("cn={0}{2},{1}",TASKSCONTAINER, Key, CN), false);
			RoleAssignments = new Collections.RoleAssignmentsCollection();
			Tasks = AdTask.GetTasks(string.Format("cn={0}{2},{1}", TASKSCONTAINER, Key, CN), false);
			Operations = AdOperation.GetCollection(string.Format("cn={0}{1},{2}", OPSCONTAINER, CN, Key), false);

			ChangeTrackingDisabled = false;
		}

		protected override void GetNewUniqueName()
		{
			Key = string.Format("cn={0},{1}", CN, ContainerDn);
		}

		protected override AddRequest CreateNewThis()
		{
			AddRequest ar = base.CreateNewThis();
			ar.Attributes.Add(CreateAttribute(APPNAME, Name));

			if (!string.IsNullOrEmpty(ApplicationVersion))
				ar.Attributes.Add(CreateAttribute(VERSION, ApplicationVersion));

			return ar;
		}

		public override DirectoryRequest[] GetUpdate()
		{
			ModifyRequest mr = new ModifyRequest();
			mr.DistinguishedName = Key;

			SetAttribute(mr.Modifications, DESCRIPTION, Description);
			SetAttribute(mr.Modifications, NameAttribute, Name);
			SetAttribute(mr.Modifications, VERSION, ApplicationVersion);

			return new DirectoryRequest[] { mr };
		}

		public override void Delete()
		{
			List<DirectoryRequest> result = new List<DirectoryRequest>();

			result.Add(new DeleteRequest(Key));
			result[0].Controls.Add(new TreeDeleteControl());
			//foreach (var item in Groups.AllKeys)
			//{
			//    result.Add(new DeleteRequest(item));
			//}
			//result.Add(new DeleteRequest(string.Format("cn={0}{1},{2}", GROUPSCONTAINER, CN, Key)));

			//foreach (var item in Tasks.AllKeys)
			//{
			//    result.Add(new DeleteRequest(item));
			//}
			//result.Add(new DeleteRequest(string.Format("cn={0}{1},{2}", OPSCONTAINER, CN, Key)));

			//foreach (var item in Operations.AllKeys)
			//{
			//    result.Add(new DeleteRequest(item));
			//}
			//result.Add(new DeleteRequest(string.Format("cn={0}{1},{2}", TASKSCONTAINER, CN, Key)));

			//foreach (var item in RoleAssignments.AllKeys)
			//{
			//    result.Add(new DeleteRequest(item));
			//}
			//result.Add(new DeleteRequest(string.Format("cn={0}{1},{2}", ROLESCONTAINER, CN, Key)));
			//result.Add(new DeleteRequest(Key));

			Service.Save(result.ToArray());
		}

		public static Collections.ApplicationCollection GetCollection(string key)
		{
			var results = ((AdService)Locator.Service).Load(key, "(ObjectClass=" + CLASSNAME + ")");
			var q = from i in results.Cast<SearchResultEntry>() select i;
			return new Collections.ApplicationCollection(q.ToDictionary(x => x.Attributes[APPNAME][0].ToString(), x => x.DistinguishedName, StringComparer.InvariantCultureIgnoreCase));
		}
	}
}
