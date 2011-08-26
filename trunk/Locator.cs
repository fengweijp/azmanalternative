using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	internal static class Locator
	{
		private static ServiceBase _Service;
		private static Interfaces.IFactoryService _Factory;

		public static Interfaces.IFactoryService Factory
		{
			get { return _Factory; }
		}

		public static ServiceBase Service
		{
			get { return _Service; }
		}

		public static void SetService(ServiceBase service, Interfaces.IFactoryService factory)
		{
			_Service = service;
			_Factory = factory;
		}
	}
}
