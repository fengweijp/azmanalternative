using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.ActiveDirectory
{
    internal class AdService : ServiceBase
    {
        public AdService(string connectionString)
            : base()
        {
            ConnectionString = connectionString;
        }

        public override AdminManager GetAdminManager()
        {
            throw new NotImplementedException();
        }

        public override Application GetApplication(Guid guid)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<Application> GetApplications(IEnumerable<Guid> guids, AdminManager store)
        {
            throw new NotImplementedException();
        }

        public override ApplicationGroup GetGroup(Guid guid)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<ApplicationGroup> GetGroups(IEnumerable<Guid> guids, AdminManager store, Application application)
        {
            throw new NotImplementedException();
        }

        public override Operation GetOperation(Guid guid)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<Operation> GetOperations(IEnumerable<Guid> guids, Application application)
        {
            throw new NotImplementedException();
        }

        public override Task GetTask(Guid guid)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<Task> GetTasks(IEnumerable<Guid> guids, Application application)
        {
            throw new NotImplementedException();
        }

        public override RoleAssignments GetRoleAssignments(Guid guid)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<RoleAssignments> GetRoleAssignmentsCollection(IEnumerable<Guid> guids, Application application)
        {
            throw new NotImplementedException();
        }

        public override RoleDefinition GetRoleDefinition(Guid guid)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<RoleDefinition> GetRoleDefinitions(IEnumerable<Guid> guids, Application application)
        {
            throw new NotImplementedException();
        }
    }
}
