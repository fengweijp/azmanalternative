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
                Application a = Service.GetApplication(Guids[name]);
                a.Store = AdminManager;

                return a;
            }
        }

        internal ApplicationCollection(ServiceBase service, Dictionary<string, Guid> values)
            : base(service, values)
        {
            Loader = Service.GetApplication;
        }

        public override IEnumerator<Application> GetEnumerator()
        {
            return Service.GetApplications(Guids.Values, AdminManager);
        }
    }
}
