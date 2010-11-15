﻿using System;
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
                ApplicationGroup result = base[name];
				if (result.IsGlobalGroup)
				{
					result.Store = Store;
					result.Application = null;
				}                
                return result;
            }
        }

        internal ApplicationGroupCollection(ServiceBase service, Dictionary<string, Guid> values)
            : base(service, values)
        {
            Loader = Service.GetGroup;
        }

        public override IEnumerator<ApplicationGroup> GetEnumerator()
        {
			return Service.GetGroups(Guids.Values, Store, Application);
        }
    }
}
