using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public delegate ContainerBase LoadDelegate(string uniqueName);

	public abstract class CollectionBase<T> : IEnumerable<T> where T : ContainerBase
	{
		private const string DUPLICATENAMEERROR = "The {0} name is already in use by another {0}.";

		protected Dictionary<string, string> Guids;
		private InternalCollection<T> InternalCollection;
		protected ServiceBase Service;
		internal Application Application;
		//protected LoadDelegate ItemLoader;
		protected bool LinkedList;

		public virtual T this[string name]
		{
			get
			{
				if (!ContainsName(name))
					return null;

				if (LinkedList)
				{
					return (T)LinkedItemLoader(name);
				}

				if (!InternalCollection.Contains(Guids[name]))
				{
					T Result = ItemLoader(Guids[name]);
					Result.Parent = Application;

					InternalCollection.Add((T)Result);
				}
				return InternalCollection[Guids[name]];
			}
		}

		public int Count
		{
			get { return Guids.Count; }
		}

		internal CollectionBase(ServiceBase service, Dictionary<string, string> children)
			: this(service, children, false)
		{ }

		internal CollectionBase(ServiceBase service, Dictionary<string, string> children, bool linked)
		{
			Service = service;
			Guids = children;
			InternalCollection = new InternalCollection<T>();

			LinkedList = linked;
		}

		public abstract IEnumerator<T> GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal void AddValue(T entry)
		{
			Guids.Add(entry.Name, entry.Key);

			if (!LinkedList)
				InternalCollection.Add(entry);
		}

		internal void RemoveValue(string uniqueName)
		{	
			if (!Guids.ContainsValue(uniqueName))
				return;

			Guids.Remove(Guids.First(item => item.Value == uniqueName).Key);

			if (LinkedList)
				return;

			if (InternalCollection.Contains(uniqueName))
				InternalCollection.Remove(uniqueName);
		}

		internal void UpdateValue(T entry)
		{
			var k = Guids.First(item => item.Value == entry.Key);

			if (k.Key == entry.Name)
				return;

			Guids.Remove(k.Key);
			Guids.Add(entry.Name, entry.Key);

			if (LinkedList)
				return;

			if (InternalCollection.Contains(entry.Key))
				InternalCollection.Remove(k.Value);
			InternalCollection.Add(entry);
		}

		public bool ContainsName(string name)
		{
			return Guids.ContainsKey(name);
		}

		internal bool ContainsKey(string uniqueName)
		{
			return Guids.ContainsValue(uniqueName);
		}

		internal abstract void CheckName(T entry);

		protected virtual void CheckName(T entry, string error)
		{
			if (!Guids.ContainsKey(entry.Name))
				return;

			if (Guids[entry.Name] != entry.Key)
				throw new AzException(string.Format(DUPLICATENAMEERROR, error));
		}

		internal abstract void CheckName(string name);

		protected virtual void CheckName(string name, string error)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			if (Guids.ContainsKey(name))
				throw new AzException(string.Format(DUPLICATENAMEERROR, error));
		}

		protected abstract ContainerBase LinkedItemLoader(string name);

		protected abstract T ItemLoader(string uniqueName);
	}

	internal class InternalCollection<T> : System.Collections.ObjectModel.KeyedCollection<string, T> where T : ContainerBase
	{
		protected override string GetKeyForItem(T item)
		{
			return item.Key;
		}
	}
}
