using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{

	public class ApplicationCollection
	{
		private ServiceBase _Service;

		internal ApplicationCollection(ServiceBase service)
		{
			_Service = service;
		}

		public Application Load(Guid guid)
		{
			return new Application(_Service.GetApplication(guid));
		}
	}
}
