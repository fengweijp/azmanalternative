using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
    internal class AdBaseObject
    {
		protected const string DESCRIPTION = "description";
		protected const string OBJECTCLASS = "objectClass";

		protected List<string> Changes;
		private string _UniqueName;
		private string _Name;
		private string _Description;

		protected readonly AdService Service;

		protected virtual string UNIQUENAME
		{
			get { return "distinguishedName"; }
		}

		protected virtual string NAME
		{
			get { return "cn"; }
		}

        public string UniqueName
        {
			get { return _UniqueName; }
			set
			{
				if (_UniqueName == value)
					return;

				_UniqueName = value;
				FlagForChange(UNIQUENAME);
			}
        }

        public string Name
        {
			get { return _Name; }
			set
			{
				if (_Name == value)
					return;
				_Name = value;
				FlagForChange(NAME);
			}
        }

        public string Description
        {
			get { return _Description; }
			set
			{
				if (_Description == value)
					return;

				_Description = value;
				FlagForChange(DESCRIPTION);
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

			Changes = new List<string>();
        }

		public virtual void Load(SearchResultEntry entry)
		{
			//UniqueName = GetAttribute(entry.Attributes, UniqueName);
			UniqueName = entry.DistinguishedName;
			Name = GetAttribute(entry.Attributes, NAME);
			Description = GetAttribute(entry.Attributes, DESCRIPTION);
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
			if (!Changes.Contains(name))
				return;

			DirectoryAttributeModification m = new DirectoryAttributeModification();
			m.Add(value);
			m.Name = name;
			m.Operation = DirectoryAttributeOperation.Replace;

			modifications.Add(m);
		}

		protected DirectoryAttribute CreateAttribute(string name, string value)
		{
			DirectoryAttribute result = new DirectoryAttribute();
			result.Name = name;
			result.Add(value);

			return result;
		}

		protected virtual ModifyRequest GetUpdate()
		{
			string tmp = GetNewUniqueName();
			if (UniqueName != tmp)
			{
				string orignalDn = UniqueName;
				UniqueName = tmp;

				Service.UpdateDN(UniqueName, orignalDn);
			}

			ModifyRequest mr = new ModifyRequest();
			mr.DistinguishedName = UniqueName;

			SetAttribute(mr.Modifications, DESCRIPTION, Description);
			SetAttribute(mr.Modifications, NAME, Name);

			return mr;
		}

		protected void FlagForChange(string name)
		{
			if (Changes.Contains(name))
				return;

			Changes.Add(name);
		}

		protected virtual string GetNewUniqueName()
		{
			return string.Format("CN={0},{1}", Name, ContainerDn);
		}

		protected virtual AddRequest CreateNew()
		{
			AddRequest ar = new AddRequest();
			ar.DistinguishedName = GetNewUniqueName();
			ar.Attributes.Add(CreateAttribute(NAME, Name));
			ar.Attributes.Add(CreateAttribute(DESCRIPTION, Description));

			return ar;
		}
    }
}
