using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	public enum ChangeType
	{
		Add,
		Remove,
		Modify
	}

	internal abstract class AdBaseObject
	{
		protected const string DESCRIPTION = "description";
		protected const string OBJECTCLASS = "objectClass";
		protected const string CONTAINER = "container";

		protected Dictionary<string, ChangeType> Changes;
		protected bool ChangeTrackingDisabled;
		private string _UniqueName;
		private string _Name;
		private string _Description;

		protected readonly AdService Service;

		protected virtual string KeyName
		{
			get { return "distinguishedName"; }
		}

		protected virtual string NAME
		{
			get { return "cn"; }
		}

		protected abstract string ObjectClass
		{
			get;
		}

		public string Key
		{
			get { return _UniqueName; }
			set
			{
				OnPropertyChanged(KeyName, _UniqueName, value);
				_UniqueName = value;
			}
		}

		public string Name
		{
			get { return _Name; }
			set
			{
				OnPropertyChanged(NAME, _Name, value);
				_Name = value;
			}
		}

		public string Description
		{
			get { return _Description; }
			set
			{
				OnPropertyChanged(DESCRIPTION, _Description, value);
				_Description = value;
			}
		}

		public string ContainerDn
		{
			get;
			set;
		}

		public AdBaseObject(AdService service)
		{
			Service = service;

			Changes = new Dictionary<string, ChangeType>();
		}

		public virtual void Load(SearchResultEntry entry)
		{
			//UniqueName = GetAttribute(entry.Attributes, UniqueName);
			Key = entry.DistinguishedName;
			Name = GetAttribute(entry.Attributes, NAME);
			Description = GetAttribute(entry.Attributes, DESCRIPTION);
			ContainerDn = Key.Substring(4 + Name.Length);
		}

		protected string GetAttribute(SearchResultAttributeCollection attributes, string name)
		{
			if (attributes[name] == null)
				return null;

			string result = attributes[name][0].ToString();

			return string.IsNullOrEmpty(result) ? null : result;
		}

		protected void SetAttribute(DirectoryAttributeModificationCollection modifications, string name, string value)
		{
			SetAttribute(modifications, name, name, value);
		}

		protected void SetAttribute(DirectoryAttributeModificationCollection modifications, string name, string property, string value)
		{
			if (!Changes.ContainsKey(property))
				return;

			DirectoryAttributeModification m = new DirectoryAttributeModification();
			m.Add(value);
			m.Name = name;
			switch (Changes[property])
			{
				case ChangeType.Add:
					m.Operation = DirectoryAttributeOperation.Add;
					break;
				case ChangeType.Remove:
					m.Operation = DirectoryAttributeOperation.Delete;
					break;
				case ChangeType.Modify:
					m.Operation = DirectoryAttributeOperation.Replace;
					break;
			}

			modifications.Add(m);
		}

		protected DirectoryAttribute CreateAttribute(string name, string value)
		{
			DirectoryAttribute result = new DirectoryAttribute();
			result.Name = name;
			result.Add(value);

			return result;
		}

		public virtual ModifyRequest GetUpdate()
		{
			string tmp = GetNewUniqueName();
			if (Key != tmp)
			{
				string orignalDn = Key;
				Key = tmp;

				Service.UpdateDN(Key, orignalDn);
			}

			ModifyRequest mr = new ModifyRequest();
			mr.DistinguishedName = Key;

			SetAttribute(mr.Modifications, DESCRIPTION, Description);
//			SetAttribute(mr.Modifications, NAME, Name);

			return mr;
		}

		protected void OnPropertyChanged(string name, string oldValue, string newValue)
		{
			if (ChangeTrackingDisabled)
				return;

			if (oldValue == newValue)
				return;

			ChangeType c;
			if (oldValue == null)
			{
				c = ChangeType.Add;
				if (Changes.ContainsKey(name) && Changes[name] != ChangeType.Add)
					c = ChangeType.Modify;
			}
			else if (newValue == null)
			{
				c = ChangeType.Remove;
				if (Changes.ContainsKey(name) && Changes[name] == ChangeType.Add)
				{
					Changes.Remove(name);
					return;
				}
			}
			else
			{
				c = ChangeType.Modify;
				if (Changes.ContainsKey(name) && Changes[name] == ChangeType.Add)
					c = ChangeType.Add;
			}

			Changes[name] = c;
		}

		protected virtual string GetNewUniqueName()
		{
			return string.Format("CN={0},{1}", Name, ContainerDn);
		}

		public virtual AddRequest CreateNew()
		{
			AddRequest ar = new AddRequest();
			ar.DistinguishedName = GetNewUniqueName();
			//ar.Attributes.Add(CreateAttribute(NAME, Name));
			ar.Attributes.Add(CreateAttribute(DESCRIPTION, Description));
			ar.Attributes.Add(CreateAttribute(OBJECTCLASS, ObjectClass));

			return ar;
		}

		public virtual AddRequest[] CreateChildEntries()
		{
			throw new NotSupportedException();
		}

		public virtual void Delete()
		{
			DeleteRequest dr = new DeleteRequest();
			dr.DistinguishedName = Key;

			Service.Save(dr);
		}

		//protected Dictionary<string, string> GetLinks(DirectoryAttribute attribute)
		//{

		//}

		//protected string GetBaseDN()
		//{

		//}
		
		public Dictionary<string, string> GetChildren(string container, string filter)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			foreach (SearchResultEntry item in Service.Load(container, filter))
			{
				result.Add(GetAttribute(item.Attributes, NAME), item.DistinguishedName);
			}

			return result;
		}

	}
}
