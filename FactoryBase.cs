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

		public abstract AdminManager GetAdminManager();

		public abstract Application GetApplication(Guid guid);
        public abstract IEnumerator<Application> GetApplications(Guid guid);

        public abstract ApplicationGroup GetGroup(Guid guid);
        public abstract IEnumerator<ApplicationGroup> GetGroups(Guid guid);

        public abstract Operation GetOperation(Guid guid);
        public abstract IEnumerator<Operation> GetOperations(Guid guid);

        public abstract Task GetTask(Guid guid);
        public abstract IEnumerator<Task> GetTasks(Guid guid);

        public abstract RoleAssignments GetRoleAssignments(Guid guid);
        public abstract IEnumerator<RoleAssignments> GetRoleAssignmentsCollection(Guid guid);

        public abstract RoleDefinition GetRoleDefinition(Guid guid);
        public abstract IEnumerator<RoleDefinition> GetRoleDefinitions(Guid guid);
	}
}
