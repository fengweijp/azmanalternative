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

		public abstract Application GetApplication(Guid guid);
        public abstract IEnumerator<Application> GetApplications(IEnumerable<Guid> guids, AdminManager store);

        public abstract ApplicationGroup GetGroup(Guid guid);
		public abstract IEnumerator<ApplicationGroup> GetGroups(IEnumerable<Guid> guids, AdminManager store, Application application);

        public abstract Operation GetOperation(Guid guid);
		public abstract IEnumerator<Operation> GetOperations(IEnumerable<Guid> guids, Application application);

        public abstract Task GetTask(Guid guid);
		public abstract IEnumerator<Task> GetTasks(IEnumerable<Guid> guids, Application application);

        public abstract RoleAssignments GetRoleAssignments(Guid guid);
		public abstract IEnumerator<RoleAssignments> GetRoleAssignmentsCollection(IEnumerable<Guid> guids, Application application);

        public abstract RoleDefinition GetRoleDefinition(Guid guid);
		public abstract IEnumerator<RoleDefinition> GetRoleDefinitions(IEnumerable<Guid> guids, Application application);
	}
}
