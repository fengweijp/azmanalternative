using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
    public delegate ContainerBase LoadDelegate(Guid guid);

    public abstract class CollectionBase<T> : IEnumerable<T> where T : ContainerBase
    {
        protected Dictionary<string, Guid> Guids;
        protected ServiceBase Service;
        internal Application Application;
        protected LoadDelegate Loader;

        public virtual T this[string name]
        {
            get
            {
                ContainerBase Result = Loader(Guids[name]);
                Result.Application = Application;

                return (T)Result;
            }
        }

        public int Count
        {
            get { return Guids.Count; }
        }

        internal CollectionBase(ServiceBase service, Dictionary<string, Guid> children)
        {
            Service = service;
            Guids = children;
        }

        public abstract IEnumerator<T> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
