using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	/// <summary>
	/// A collection of applications
	/// </summary>
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

		protected override string ErrorObjectName
		{
			get { return "application"; }
		}

		internal ApplicationCollection(Dictionary<string, string> values)
			: base(values)
		{ }

		internal ApplicationCollection(ServiceBase service)
			: this(new Dictionary<string, string>())
		{ }
		

		public override IEnumerator<Application> GetEnumerator()
		{
			return Service.GetApplications(Guids.Values, AdminManager);
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
