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
                ApplicationGroup result = base[name];
                if (Store != null)
                    result.Store = Store;
                
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
            if (Store == null)
                return Service.GetGroups(Application.Guid);
            else
                return Service.GetGroups(Store.Guid);
        }
    }
}
