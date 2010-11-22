﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public delegate ContainerBase LoadDelegate(Guid guid);

	public abstract class CollectionBase<T> : IEnumerable<T> where T : ContainerBase
	{
		protected Dictionary<string, Guid> Guids;
		private InternalCollection<T> InternalCollection;
		protected ServiceBase Service;
		internal Application Application;
		protected LoadDelegate ItemLoader;

		public virtual T this[string name]
		{
			get
			{
				if (!Guids.ContainsKey(name))
					return null;

				if (!InternalCollection.Contains(Guids[name]))
				{
					ContainerBase Result = ItemLoader(Guids[name]);
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

		internal CollectionBase(ServiceBase service, Dictionary<string, Guid> children)
		{
			Service = service;
			Guids = children;
			InternalCollection = new InternalCollection<T>();
		}

		public abstract IEnumerator<T> GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal void AddValue(Guid guid, string name)
		{
			Guids.Add(name, guid);
		}

		internal void RemoveValue(Guid guid)
		{	
			if (!Guids.ContainsValue(guid))
				return;

			Guids.Remove(Guids.First(item => item.Value == guid).Key);

			if (InternalCollection.Contains(guid))
				InternalCollection.Remove(guid);
		}

		internal void UpdateValue(Guid guid, string name)
		{
			var k = Guids.First(item => item.Value == guid);

			if (k.Key == name)
				return;

			Guids.Remove(k.Key);
			Guids.Add(name, guid);
		}

		public bool ContainsName(string name)
		{
			return Guids.ContainsKey(name);
		}

		internal bool ContainsGuid(Guid guid)
		{
			return Guids.ContainsValue(guid);
		}

	}

	internal class InternalCollection<T> : System.Collections.ObjectModel.KeyedCollection<Guid, T> where T : ContainerBase
	{
		protected override Guid GetKeyForItem(T item)
		{
			return item.Guid;
		}
	}
}
