using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public delegate ContainerBase LoadDelegate(string uniqueName);

	/// <summary>
	/// Base class for the collections
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class CollectionBase<T> : IEnumerable<T> where T : ContainerBase
	{
		private const string DUPLICATENAMEERROR = "The {0} name is already in use by another {0}.";

		protected Dictionary<string, string> Guids;
		private InternalCollection<T> InternalCollection;
		protected ServiceBase Service;
		internal Application Application;
		//protected LoadDelegate ItemLoader;
		protected bool IsChildList;

		/// <summary>
		/// Retrieves an item from the collection. The item is cached if this is the parent collection
		/// </summary>
		/// <param name="name">The name of the item to retrieve</param>
		/// <returns>Returns the item, or null if not in the collection</returns>
		public virtual T this[string name]
		{
			get
			{
				if (!ContainsName(name))
					return null;

				if (IsChildList)
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

		/// <summary>
		/// Gets the count of items in this collection
		/// </summary>
		public int Count
		{
			get { return Guids.Count; }
		}

		protected abstract string ErrorObjectName { get; }

		/// <summary>
		/// Creates an instance of the collection
		/// </summary>
		/// <param name="service">The service factory</param>
		/// <param name="children">The list of names & keys for this collection</param>
		internal CollectionBase(Dictionary<string, string> children)
			: this(children, false)
		{ }

		/// <summary>
		/// Creates an instance of the collection
		/// </summary>
		/// <param name="service">The service factory</param>
		/// <param name="children">The list of names & keys for this collection</param>
		/// <param name="isChildList">Indicates this collection contains items from other collections</param>
		internal CollectionBase(Dictionary<string, string> children, bool isChildList)
		{
			Service = Locator.Service;
			Guids = children;
			InternalCollection = new InternalCollection<T>();

			IsChildList = isChildList;
		}

		public abstract IEnumerator<T> GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Adds a value to collection cache
		/// </summary>
		/// <param name="entry"></param>
		internal void AddValue(T entry)
		{
			Guids.Add(entry.Name, entry.Key);

			if (!IsChildList)
				InternalCollection.Add(entry);
		}

		/// <summary>
		/// Removes an item from the collection cache
		/// </summary>
		/// <param name="uniqueName">the unique key of the item to remove</param>
		internal void RemoveValue(string uniqueName)
		{	
			if (!Guids.ContainsValue(uniqueName))
				return;

			Guids.Remove(Guids.First(item => item.Value == uniqueName).Key);

			if (IsChildList)
				return;

			if (InternalCollection.Contains(uniqueName))
				InternalCollection.Remove(uniqueName);
		}

		/// <summary>
		/// Updates an item in the collection cache
		/// </summary>
		/// <param name="entry">The item to locate and update</param>
		internal void UpdateValue(T entry)
		{
			var k = Guids.First(item => item.Value == entry.Key);

			if (k.Key == entry.Name)
				return;

			Guids.Remove(k.Key);
			Guids.Add(entry.Name, entry.Key);

			if (IsChildList)
				return;

			if (InternalCollection.Contains(entry.Key))
				InternalCollection.Remove(k.Value);
			InternalCollection.Add(entry);
		}

		/// <summary>
		/// Checks whether the collection contains the specified name
		/// </summary>
		/// <param name="name">The name to search for</param>
		/// <returns>returns true if the name is in the collection</returns>
		public bool ContainsName(string name)
		{
			return Guids.ContainsKey(name);
		}

		/// <summary>
		/// Checks whether the specified unique key is contained in the collection
		/// </summary>
		/// <param name="uniqueName">The key to search for</param>
		/// <returns>true if the key is in the collection</returns>
		internal bool ContainsKey(string uniqueName)
		{
			return Guids.ContainsValue(uniqueName);
		}

		/// <summary>
		/// Checks whether the name exists in the collection
		/// </summary>
		/// <param name="entry">The entry to look for</param>
		internal virtual void CheckName(T entry)
		{
			if (!Guids.ContainsKey(entry.Name))
				return;

			if (Guids[entry.Name] != entry.Key)
				throw new AzException(string.Format(DUPLICATENAMEERROR, ErrorObjectName));
		}

		/// <summary>
		/// Checks whether the name exists in the collection
		/// </summary>
		/// <param name="name">The name to look for</param>
		internal virtual void CheckName(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			if (Guids.ContainsKey(name))
				throw new AzException(string.Format(DUPLICATENAMEERROR, ErrorObjectName));
		}

		/// <summary>
		/// Loads an item from another collection
		/// </summary>
		/// <param name="name">the item name to load</param>
		/// <returns>The loaded item</returns>
		protected abstract ContainerBase LinkedItemLoader(string name);

		/// <summary>
		/// Loads an item from the underlying store
		/// </summary>
		/// <param name="uniqueName">the key of the item to load</param>
		/// <returns>The loaded item</returns>
		protected abstract T ItemLoader(string uniqueName);
	}

	/// <summary>
	/// Collection of keyed items
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal class InternalCollection<T> : System.Collections.ObjectModel.KeyedCollection<string, T> where T : ContainerBase
	{
		protected override string GetKeyForItem(T item)
		{
			return item.Key;
		}
	}
}
