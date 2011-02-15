using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.DirectoryServices;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	internal class AdService : ServiceBase
	{
		private string _BaseDN;
		private string _Host;

		public AdService(string connectionString)
			: base()
		{
			ConnectionString = connectionString;
			ParseConnectionString();
		}

		private void ParseConnectionString()
		{
			_Host = ConnectionString.Substring(0, ConnectionString.IndexOf("/"));
			_BaseDN = ConnectionString.Substring(ConnectionString.IndexOf("/") + 1);
		}

		private LdapConnection GetConnection()
		{
			LdapConnection conn = new LdapConnection(_Host);
			conn.AuthType = AuthType.Negotiate;

			return conn;
		}

		public SearchResultEntry LoadRoot()
		{
			return Load(_BaseDN);
		}

		public SearchResultEntry Load(string uniqueName)
		{
			LdapConnection conn = null;
			try
			{
				conn = GetConnection();

				SearchRequest sr = new SearchRequest();
				sr.DistinguishedName = uniqueName;
				sr.Scope = System.DirectoryServices.Protocols.SearchScope.Base;
				sr.Filter = "(ObjectClass=*)";

				conn.Bind();
				SearchResponse resp = (SearchResponse)conn.SendRequest(sr);
				if (resp.Entries.Count != 1)
					throw new AzException("The requested object could not be found.");

				return resp.Entries[0];
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				if (conn != null) conn.Dispose();
			}
		}

		public void UpdateDN(string uniqueName, string originalName)
		{
			if (uniqueName == originalName)
				return;

			ModifyDNRequest mdr = new ModifyDNRequest();
			mdr.DistinguishedName = originalName;
			mdr.NewName = uniqueName;

			LdapConnection conn = null;
			try
			{
				conn.SendRequest(mdr);
			}
			finally
			{
				if (conn != null) conn.Dispose();
			}
		}

		public void Save(DirectoryRequest dr)
		{
			LdapConnection conn = null;
			try
			{
				conn = GetConnection();
				conn.SendRequest(dr);
			}
			finally
			{
				if (conn != null)
					conn.Dispose();
			}

		}

		public override Interfaces.IAdminManager GetAdminManager()
		{
			return new AdAdminManager(this);
		}

		public override Application GetApplication(string uniqueName)
		{
			throw new NotImplementedException();
		}

		public override IEnumerator<Application> GetApplications(IEnumerable<string> uniqueNames, AdminManager store)
		{
			throw new NotImplementedException();
		}

		public override ApplicationGroup GetGroup(string uniqueName)
		{
			throw new NotImplementedException();
		}

		public override IEnumerator<ApplicationGroup> GetGroups(IEnumerable<string> uniqueNames, AdminManager store, Application application)
		{
			throw new NotImplementedException();
		}

		public override Operation GetOperation(string uniqueName)
		{
			throw new NotImplementedException();
		}

		public override IEnumerator<Operation> GetOperations(IEnumerable<string> uniqueNames, Application application)
		{
			throw new NotImplementedException();
		}

		public override Task GetTask(string uniqueName)
		{
			throw new NotImplementedException();
		}

		public override IEnumerator<Task> GetTasks(IEnumerable<string> uniqueNames, Application application)
		{
			throw new NotImplementedException();
		}

		public override RoleAssignments GetRoleAssignments(string uniqueName)
		{
			throw new NotImplementedException();
		}

		public override IEnumerator<RoleAssignments> GetRoleAssignmentsCollection(IEnumerable<string> uniqueNames, Application application)
		{
			throw new NotImplementedException();
		}

		public override RoleDefinition GetRoleDefinition(string uniqueName)
		{
			throw new NotImplementedException();
		}

		public override IEnumerator<RoleDefinition> GetRoleDefinitions(IEnumerable<string> uniqueNames, Application application)
		{
			throw new NotImplementedException();
		}

		public Collections.MemberCollection GetMembers(DirectoryAttribute members)
		{
			throw new NotImplementedException();
		}

		public Collections.MemberCollection GetExclusions(DirectoryAttribute nonMembers)
		{
			throw new NotImplementedException();
		}
	}
}
