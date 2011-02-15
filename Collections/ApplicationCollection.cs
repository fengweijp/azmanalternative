using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{

	public class ApplicationCollection : CollectionBase<Application>
	{
		internal AdminManager AdminManager;

		public override Application this[string name]
		{
			get 
			{
				Application a = base[name];
				if (a == null)
					return null;

				a.Parent = a;
				a.Store = AdminManager;

				return a;
			}
		}

		internal ApplicationCollection(ServiceBase service, Dictionary<string, string> values)
			: base(service, values)
		{ }

		internal ApplicationCollection(ServiceBase service)
			: this(service, new Dictionary<string, string>())
		{ }
		

		public override IEnumerator<Application> GetEnumerator()
		{
			return Service.GetApplications(Guids.Values, AdminManager);
		}

		internal override void CheckName(Application entry)
		{
			CheckName(entry, "application");
		}

		internal override void CheckName(string name)
		{
			CheckName(name, "application");
		}

		protected override ContainerBase LinkedItemLoader(string name)
		{
			throw new NotSupportedException();
		}

		protected override Application ItemLoader(string uniqueName)
		{
			return Service.GetApplication(uniqueName);
		}
	}
}
