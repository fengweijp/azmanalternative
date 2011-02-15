using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public class ApplicationGroupCollection : Collections.CollectionBase<ApplicationGroup>
	{
		internal AdminManager Store;

		public override ApplicationGroup this[string name]
		{
			get
			{
				if (!ContainsName(name))
					return null;

				ApplicationGroup result = base[name];
				if (LinkedList)
					return result;

				if (result.IsGlobalGroup)
				{
					result.Store = Store;
					result.Parent = null;
				}                
				return result;
			}
		}

		internal ApplicationGroupCollection(ServiceBase service, Dictionary<string, string> values, bool linked)
			: base(service, values, linked)
		{ }

		internal ApplicationGroupCollection(ServiceBase service, bool linked)
			: this(service, new Dictionary<string, string>(), linked)
		{ }

		public override IEnumerator<ApplicationGroup> GetEnumerator()
		{
			return Service.GetGroups(Guids.Values, Store, Application);
		}

		internal override void CheckName(ApplicationGroup entry)
		{
			CheckName(entry, "group");
		}

		internal override void CheckName(string name)
		{
			CheckName(name, "group");
		}

		protected override ContainerBase LinkedItemLoader(string name)
		{
			ContainerBase result = null;
			if (Application != null)
			{
				result = Application.Groups[name];
				if (result == null)
					result = Application.Store.Groups[name];
			}
			else
				result = Store.Groups[name];

			return result;
		}

		protected override ApplicationGroup ItemLoader(string uniqueName)
		{
			return Service.GetGroup(uniqueName);
		}
	}
}
