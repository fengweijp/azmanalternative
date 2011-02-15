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
		protected const string GUID = "Guid";
		protected const string NAME = "Name";
		protected const string DESCRIPTION = "Description";

		///// <summary>
		///// Gets the XML element for the current object
		///// </summary>
		//protected readonly XmlElement Node;
		///// <summary>
		///// Gets the factory object that talks to the store.
		///// </summary>
		protected readonly XmlService Service;

		public string Key
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Gets an attribute, returning null if the attribute is not defined
		/// </summary>
		/// <param name="name">name of the attribute to get</param>
		/// <returns>string value, null if not defined</returns>
		protected string GetAttribute(XmlElement node, string name)
		{
			if (!node.HasAttribute(name))
				return null;

			string result = node.Attributes[name].Value;

			return string.IsNullOrEmpty(result) ? null : result;
		}

		/// <summary>
		/// Sets an attribute, creating it if the attribute does not exist
		/// </summary>
		/// <param name="name">name of the attribute</param>
		/// <param name="value">value of the attribute</param>
		protected void SetAttribute(XmlElement node, string name, string value)
		{
			if (!node.HasAttribute(name))
			{
				node.Attributes.Append(node.OwnerDocument.CreateAttribute(name));
			}
			node.Attributes[name].Value = value;
		}

		public XmlBaseObject(XmlService service)
		{
			//Node = node;
			Service = service;
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

		public void Load(string uniqueName)
		{
			XmlElement e = Service.Load(uniqueName);
			Load(e);
		}

		public virtual void Load(XmlElement element)
		{
			Key = GetAttribute(element, GUID);
			Name = GetAttribute(element, NAME);
			Description = GetAttribute(element, DESCRIPTION);

			LoadInternal(element);
		}

		protected virtual void LoadInternal(XmlElement element)
		{ }

		public virtual XmlElement ToXml()
		{
			return Service.Load(Key);
		}

		public abstract XmlElement ToXml(XmlElement parent);

		protected static Dictionary<string, string> GetChildren(XmlElement parent, string elementName)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			foreach (XmlNode item in parent.SelectNodes(elementName))
			{
				result.Add(item.Attributes[NAME].Value, item.Attributes[GUID].Value);
			}

			return result;
		}

		protected static Dictionary<string, string> GetLinks(XmlElement parent, string elementName)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			foreach (XmlNode item in parent.SelectNodes(elementName))
			{
				result.Add(parent.OwnerDocument.SelectSingleNode(string.Format("//*[@{0}='{1}']/@Name", GUID, item.InnerText)).Value, item.InnerText);
			}

			return result;
		}
	}
}
