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

		protected override string NAME
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

		public AdApplication(AdService service)
			:base(service)
		{}

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

		public RoleAssignments CreateRoleAssignments(string name, string description, RoleDefinition role)
		{
			throw new NotImplementedException();
		}

		public void DeleteRoleAssignments(RoleAssignments role)
		{
			throw new NotImplementedException();
		}

		public void UpdateRoleAssignments(RoleAssignments role)
		{
			throw new NotImplementedException();
		}

		public RoleDefinition CreateRole(string name, string description)
		{
			AdRoleDefinition role = new AdRoleDefinition(Service);
			role.Name = name;
			role.Description = description;

			Service.Save(role.CreateNew());

			return new RoleDefinition(role);
		}

		public void DeleteRole(RoleDefinition role)
		{
			((AdRoleDefinition)role.Instance).Delete();
		}

		public void UpdateRole(RoleDefinition role)
		{
			AdRoleDefinition introle = (AdRoleDefinition)role.Instance;
			Service.Save(introle.GetUpdate());
		}

		public Operation CreateOperation(string name, string description, int operationId)
		{
			AdOperation op = new AdOperation(Service);
			op.Name = name;
			op.Description = description;
			op.OperationId = operationId;

			Service.Save(op.CreateNew());

			return new Operation(op);
		}

		public void DeleteOperation(Operation operation)
		{
			((AdOperation)operation.Instance).Delete();
		}

		public void UpdateOperation(Operation operation)
		{
			AdOperation op = (AdOperation)operation.Instance;
			Service.Save(op.GetUpdate());
		}

		public Task CreateTask(string name, string description)
		{
			throw new NotImplementedException();
		}

		public void DeleteTask(Task task)
		{
			throw new NotImplementedException();
		}

		public void UpdateTask(Task task)
		{
			throw new NotImplementedException();
		}

		public override AddRequest[] CreateChildEntries()
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

			Groups = AdApplicationGroup.GetCollection(Service, string.Format("{0}{2},{1}", GROUPSCONTAINER, Key, CN), false);
			Roles = new Collections.RoleDefinitionCollection(Service, false);
			RoleAssignments = new Collections.RoleAssignmentsCollection(Service);
			Tasks = new Collections.TaskCollection(Service, false);
			Operations = AdOperation.GetCollection(Service, string.Format("{0}{1},{2}", OPSCONTAINER, CN, Key), false);

			ChangeTrackingDisabled = false;
		}

		public override AddRequest CreateNew()
		{
			AddRequest ar = base.CreateNew();
			ar.DistinguishedName = string.Format("cn={0},{1}", CN, ContainerDn);
			ar.Attributes.Add(CreateAttribute(APPNAME, Name));

			if (!string.IsNullOrEmpty(ApplicationVersion))
				ar.Attributes.Add(CreateAttribute(VERSION, ApplicationVersion));

			return ar;
		}

		public static Collections.ApplicationCollection GetCollection(AdService service, string key)
		{
			var results = service.Load(key, "(ObjectClass=" + CLASSNAME + ")");
			var q = from i in results.Cast<SearchResultEntry>() select i;
			return new Collections.ApplicationCollection(service, q.ToDictionary(x => x.Attributes[APPNAME][0].ToString(), x => x.DistinguishedName));
		}
	}
}
