using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
    public abstract class CollectionBase<T> : ICollection<T>, IList<T>, IEnumerable<T> where T : ContainerBase
    {
        private Guid[] _Guids;
        private T[] _Objects;

        public int Count
        {
            get { return _Guids.Count(); }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException("index");

                if (_Objects[index] != null)
                    return _Objects[index];

                return Load(index);
            }
        }

        protected T Load(int index)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int index)
        {
            _Objects.CopyTo(array, index);
        }

        public bool Contains(T value)
        {
			return _Guids.Contains(value.Guid);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)_Objects.GetEnumerator();
        }

		public int IndexOf(T item)
		{
			return Array.IndexOf(_Guids, item.Guid);
		}

        #region Interfaces

        bool ICollection<T>.Contains(T item)
        {
			return Contains(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex);
        }

        int ICollection<T>.Count
        {
            get { return Count; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return true; }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
			return this.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
			return this.GetEnumerator();
        }

        int IList<T>.IndexOf(T item)
        {
			return IndexOf(item);
        }

        T IList<T>.this[int index]
        {
            get { return this[index]; }
            set { throw new NotSupportedException(); }
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException();
        }

        void ICollection<T>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
