using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	/// <summary>
	/// A collection of groups
	/// </summary>
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
				if (IsChildList)
					return result;

				if (result.IsGlobalGroup)
				{
					result.Store = Store;
					result.Parent = null;
				}                
				return result;
			}
		}

		protected override string ErrorObjectName
		{
			get { return "group"; }
		}

		/// <summary>
		/// Initialises the group collection
		/// </summary>
		/// <param name="service">The service factory object</param>
		/// <param name="values">A list of names & keys</param>
		/// <param name="isChildList">Indicates whether this collection can load objects from other collections</param>
		internal ApplicationGroupCollection(ServiceBase service, Dictionary<string, string> values, bool isChildList)
			: base(service, values, isChildList)
		{ }

		/// <summary>
		/// Initialises the group collection
		/// </summary>
		/// <param name="service">The service factory object</param>
		/// <param name="isChildList">Indicates whether this collection can load objects from other collections</param>
		internal ApplicationGroupCollection(ServiceBase service, bool isChildList)
			: this(service, new Dictionary<string, string>(), isChildList)
		{ }

		public override IEnumerator<ApplicationGroup> GetEnumerator()
		{
			return Service.GetGroups(Guids.Values, Store, Application);
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
