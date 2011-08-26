using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Interfaces
{
	interface IFactoryService
	{
		ApplicationGroup CreateGroup(string parent, string name, string description, GroupType groupType, bool isGlobalGroup);
		void UpdateGroup(Interfaces.IApplicationGroup group);
		void DeleteGroup(Interfaces.IApplicationGroup group);

		Application CreateApplication(string parent, string name, string description, string versionInformation);
		void UpdateApplication(Interfaces.IApplication application);
		void DeleteApplication(Interfaces.IApplication application);

		RoleAssignments CreateRoleAssignments(string parent, string name, string description, RoleDefinition role);
		void UpdateRoleAssignments(Interfaces.IRoleAssignment role);
		void DeleteRoleAssignments(Interfaces.IRoleAssignment role);

		RoleDefinition CreateRole(string parent, string name, string description);
		void UpdateRole(Interfaces.IRoleDefinition role);
		void DeleteRole(Interfaces.IRoleDefinition role);

		Operation CreateOperation(string parent, string name, string description, int operationId);
		void UpdateOperation(Interfaces.IOperation operation);
		void DeleteOperation(Interfaces.IOperation operation);

		Task CreateTask(string parent, string name, string description);
		void UpdateTask(Interfaces.ITask task);
		void DeleteTask(Interfaces.ITask task);
	}
}
