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

		private string _ApplicationVersion;

		protected override string ObjectClass
		{
			get { throw new NotImplementedException(); }
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
			throw new NotImplementedException();
		}

		public void DeleteRole(RoleDefinition role)
		{
			throw new NotImplementedException();
		}

		public void UpdateRole(RoleDefinition role)
		{
			throw new NotImplementedException();
		}

		public Operation CreateOperation(string name, string description, int operationId)
		{
			throw new NotImplementedException();
		}

		public void DeleteOperation(Operation operation)
		{
			throw new NotImplementedException();
		}

		public void UpdateOperation(Operation operation)
		{
			throw new NotImplementedException();
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
			base.Load(entry);

			CN = GetAttribute(entry.Attributes, "name");
			ApplicationVersion = GetAttribute(entry.Attributes, VERSION);
			ContainerDn = Key.Substring(4 + CN.Length);
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
	}
}
