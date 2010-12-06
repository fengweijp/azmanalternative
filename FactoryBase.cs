using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	/// <summary>
	/// Base class for loading store objects. Wraps up the store provider
	/// </summary>
	public abstract class ServiceBase
	{
        /// <summary>
        /// Gets the connectionstring used by this factory object
        /// </summary>
		public string ConnectionString
		{
			get;
			protected set;
		}

		public abstract Interfaces.IAdminManager GetAdminManager();

		public abstract Application GetApplication(string uniqueName);
        public abstract IEnumerator<Application> GetApplications(IEnumerable<string> uniqueNames, AdminManager store);

		public abstract ApplicationGroup GetGroup(string uniqueName);
		public abstract IEnumerator<ApplicationGroup> GetGroups(IEnumerable<string> uniqueNames, AdminManager store, Application application);

		public abstract Operation GetOperation(string uniqueName);
		public abstract IEnumerator<Operation> GetOperations(IEnumerable<string> uniqueNames, Application application);

		public abstract Task GetTask(string uniqueName);
		public abstract IEnumerator<Task> GetTasks(IEnumerable<string> uniqueNames, Application application);

		public abstract RoleAssignments GetRoleAssignments(string uniqueName);
		public abstract IEnumerator<RoleAssignments> GetRoleAssignmentsCollection(IEnumerable<string> uniqueNames, Application application);

		public abstract RoleDefinition GetRoleDefinition(string uniqueName);
		public abstract IEnumerator<RoleDefinition> GetRoleDefinitions(IEnumerable<string> uniqueNames, Application application);
	}
}
