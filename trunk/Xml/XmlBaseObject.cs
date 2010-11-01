using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
	/// <summary>
	/// Base class for all XML objects
	/// </summary>
	internal abstract class XmlBaseObject
	{
		///// <summary>
		///// Gets the XML element for the current object
		///// </summary>
		//protected readonly XmlElement Node;
		///// <summary>
		///// Gets the factory object that talks to the store.
		///// </summary>
		protected readonly XmlService Factory;

		public Guid Guid
		{
			get;
			set;
		}
		
		///// <summary>
		///// Gets an attribute, returning null if the attribute is not defined
		///// </summary>
		///// <param name="name">name of the attribute to get</param>
		///// <returns>string value, null if not defined</returns>
		//protected string GetAttribute(string name)
		//{
		//    if (!Node.HasAttribute(name))
		//        return null;

		//    return Node.Attributes[name].Value;
		//}

		///// <summary>
		///// Sets an attribute, creating it if the attribute does not exist
		///// </summary>
		///// <param name="name">name of the attribute</param>
		///// <param name="value">value of the attribute</param>
		//protected void SetAttribute(string name, string value)
		//{
		//    if (!Node.HasAttribute(name))
		//    {
		//        Node.Attributes.Append(Node.OwnerDocument.CreateAttribute(name));
		//    }
		//    Node.Attributes[name].Value = value;
		//}

		public XmlBaseObject(XmlService factory)
		{
			//Node = node;
			Factory = factory;
		}

		///// <summary>
		///// Saves changes to the current node. Base implementation does nothing
		///// </summary>
		///// <param name="parent">parent element</param>
		//public virtual void Update(XmlElement parent)
		//{ }

		//protected System.Collections.ObjectModel.ReadOnlyCollection<T> GetCollection<T>(XmlNodeList nodes, Type innerType)
		//{
		//    List<T> result = new List<T>();
		//    Type t = typeof(T);

		//    foreach (XmlNode item in nodes)
		//    {
		//        result.Add((T)Activator.CreateInstance(t, Activator.CreateInstance(innerType, (XmlElement)item, Factory)));
		//    }

		//    return new System.Collections.ObjectModel.ReadOnlyCollection<T>(result);
		//}

		public virtual XmlElement ToXml()
		{
			return Factory.Load(Guid);
		}
	}
}
