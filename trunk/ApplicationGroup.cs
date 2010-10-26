using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	public class ApplicationGroup : Interfaces.IApplicationGroup
	{
		private readonly Interfaces.IApplicationGroup _ApplicationGroup;

		public Guid Guid
		{
			get { return _ApplicationGroup.Guid; }
		}

		public string Name
		{
			get { return _ApplicationGroup.Name; }
			set { _ApplicationGroup.Name = value; }
		}

		public string Description
		{
			get { return _ApplicationGroup.Description; }
			set { _ApplicationGroup.Description = value; }
		}

		public GroupType GroupType
		{
			get { return _ApplicationGroup.GroupType; }
		}

		public List<Interfaces.IMember> Members
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		internal ApplicationGroup(Interfaces.IApplicationGroup applicationGroup)
		{
			_ApplicationGroup = applicationGroup;
		}

		internal bool ValidateGroup()
		{
			if (string.IsNullOrEmpty(Name))
				return false;

			return true;
		}

		public void AddMember(Interfaces.IMember member)
		{
			throw new NotImplementedException();
		}

		public void RemoveMember(Interfaces.IMember member)
		{
			throw new NotImplementedException();
		}
	}
}
