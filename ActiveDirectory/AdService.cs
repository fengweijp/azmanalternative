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

		private SearchResultEntry Load(string uniqueName, LdapConnection conn)
		{
			try
			{
				SearchRequest sr = new SearchRequest();
				sr.DistinguishedName = uniqueName;
				sr.Scope = System.DirectoryServices.Protocols.SearchScope.Base;
				sr.Filter = "(ObjectClass=*)";

				SearchResponse resp = (SearchResponse)conn.SendRequest(sr);
				if (resp.Entries.Count != 1)
					throw new AzException("The requested object could not be found.");

				return resp.Entries[0];
			}
			catch (Exception)
			{
				throw;
			}
		}
		
		public SearchResultEntry LoadRoot()
		{
			return Load(_BaseDN);
		}

		public SearchResultEntry Load(string uniqueName)
		{
			using (LdapConnection conn = GetConnection())
			{
				return Load(uniqueName, conn);
			}
		}

		public SearchResultEntryCollection Load(string container, string filter)
		{
			LdapConnection conn = null;
			try
			{
				conn = GetConnection();

				SearchRequest sr = new SearchRequest();
				sr.DistinguishedName = container;
				sr.Scope = System.DirectoryServices.Protocols.SearchScope.OneLevel;
				sr.Filter = filter;

				conn.Bind();
				SearchResponse resp = (SearchResponse)conn.SendRequest(sr);

				return resp.Entries;
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
			AdApplication a = new AdApplication(this);
			a.Load(Load(uniqueName));

			return new Application(a);
		}

		public override IEnumerator<Application> GetApplications(IEnumerable<string> uniqueNames, AdminManager store)
		{
			using (LdapConnection conn = GetConnection())
			{
				foreach (var item in uniqueNames)
				{
					AdApplication a = new AdApplication(this);
					a.Load(Load(item, conn));

					yield return new Application(a, store);
				}
			}
		}

		public override ApplicationGroup GetGroup(string uniqueName)
		{
			AdApplicationGroup ag = new AdApplicationGroup(this);
			ag.Load(Load(uniqueName));

			return new ApplicationGroup(ag);
		}

		public override IEnumerator<ApplicationGroup> GetGroups(IEnumerable<string> uniqueNames, AdminManager store, Application application)
		{
			using (LdapConnection conn = GetConnection())
			{
				foreach (var item in uniqueNames)
				{
					AdApplicationGroup g = new AdApplicationGroup(this);
					g.Load(Load(item, conn));
					ApplicationGroup result = new ApplicationGroup(g);

					if (result.IsGlobalGroup)
						result.Store = store;
					else
						result.Parent = application;

					yield return result;
				}
			}
		}

		public override Operation GetOperation(string uniqueName)
		{
			AdOperation o = new AdOperation(this);
			o.Load(Load(uniqueName));

			return new Operation(o);
		}

		public override IEnumerator<Operation> GetOperations(IEnumerable<string> uniqueNames, Application application)
		{
			using (LdapConnection conn = GetConnection())
			{
				foreach (var item in uniqueNames)
				{
					AdOperation a = new AdOperation(this);
					a.Load(Load(item, conn));

					yield return new Operation(a, application);
				}
			}
		}

		public override Task GetTask(string uniqueName)
		{
			AdTask t = new AdTask(this);
			t.Load(Load(uniqueName));

			return new Task(t);
		}

		public override IEnumerator<Task> GetTasks(IEnumerable<string> uniqueNames, Application application)
		{
			using (LdapConnection conn = GetConnection())
			{
				foreach (var item in uniqueNames)
				{
					AdTask a = new AdTask(this);
					a.Load(Load(item, conn));

					yield return new Task(a, application);
				}
			}
		}

		public override RoleAssignments GetRoleAssignments(string uniqueName)
		{
			AdRoleAssignments r = new AdRoleAssignments(this);
			r.Load(Load(uniqueName));

			return new RoleAssignments(r);
		}

		public override IEnumerator<RoleAssignments> GetRoleAssignmentsCollection(IEnumerable<string> uniqueNames, Application application)
		{
			using (LdapConnection conn = GetConnection())
			{
				foreach (var item in uniqueNames)
				{
					AdRoleAssignments a = new AdRoleAssignments(this);
					a.Load(Load(item, conn));

					yield return new RoleAssignments(a, application);
				}
			}
		}

		public override RoleDefinition GetRoleDefinition(string uniqueName)
		{
			AdRoleDefinition r = new AdRoleDefinition(this);
			r.Load(Load(uniqueName));

			return new RoleDefinition(r);
		}

		public override IEnumerator<RoleDefinition> GetRoleDefinitions(IEnumerable<string> uniqueNames, Application application)
		{
			using (LdapConnection conn = GetConnection())
			{
				foreach (var item in uniqueNames)
				{
					AdRoleDefinition a = new AdRoleDefinition(this);
					a.Load(Load(item, conn));

					yield return new RoleDefinition(a, application);
				}
			}
		}

		public override IEnumerator<Member> GetMembers(string uniqueName, bool isExclusions)
		{
			SearchResultEntry result = Load(uniqueName);

			foreach (var item in AdApplicationGroup.GetMembers(result, isExclusions))
			{
				AdMember a = new AdMember(this);

				yield return new Member(a);
			}
		}
	}
}
