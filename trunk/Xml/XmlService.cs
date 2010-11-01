using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	/// <summary>
	/// Wraps up access to the underlying store & provides helper method to access it
	/// </summary>
	internal class XmlFactory : FactoryBase
	{
		private XmlDocument _Doc;

		/// <summary>
		/// Save changes to the store
		/// </summary>
		public void SaveChanges()
		{
			_Doc.Save(ConnectionString);
		}

		/// <summary>
		/// Create and load the store
		/// </summary>
		public override void Load()
		{
			_Doc = new XmlDocument();
			_Doc.Load(ConnectionString);
		}

		/// <summary>
		/// Returns the root node from the store
		/// </summary>
		/// <returns>root or document element</returns>
		public XmlElement GetRoot()
		{
			return _Doc.DocumentElement;
		}

		/// <summary>
		/// Creates a new element from the store
		/// </summary>
		/// <param name="name">name to use for the new element</param>
		/// <returns>empty xml element</returns>
		public XmlElement CreateNew(string name)
		{
			return _Doc.CreateElement(name);
		}
	}
}
