﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.Protocols;

namespace AzAlternative.ActiveDirectory
{
	internal class AdApplicationGroup: AdBaseObject, Interfaces.IApplicationGroup
	{
		private const string GROUPTYPE = "groupType";
		private const string LDAPQUERY = "msDS-AzLDAPQuery";
		//private const string NAME2 = "name";
		private const string SAMACCOUNTNAME = "sAMAccountName";
		private const string MEMBEROF = "msDS-MembersForAzRoleBL";
		private const string NONMEMBEROF = "msDS-NonMembersBL";
		private const string MEMBERS = "member";
		private const string NONMEMBERS = "msDS-NonMembers";
		private const string CLASSNAME = "group";

		private string _LdapQuery;
		private GroupType _GroupType;

		protected override string ObjectClass
		{
			get { return CLASSNAME; }
		}

		public GroupType GroupType
		{
			get { return _GroupType; }
			set
			{
				OnPropertyChanged(GROUPTYPE, _GroupType.ToString(), value.ToString());
				_GroupType = value;
			}
		}

		public bool IsGlobalGroup
		{
			get;
			set;
		}

		public string LdapQuery
		{
			get { return _LdapQuery; }
			set
			{
				OnPropertyChanged(LDAPQUERY, _LdapQuery, value);
				_LdapQuery = value;
			}
		}

		public Collections.MemberCollection Members
		{
			get;
			set;
		}

		public Collections.MemberCollection Exclusions
		{
			get;
			set;
		}

		public Collections.ApplicationGroupCollection Groups
		{
			get;
			set;
		}

		public AdApplicationGroup(AdService service)
			: base(service)
		{ }

		public void AddMember(string name)
		{
			throw new NotImplementedException();
		}

		public void RemoveMember(string member)
		{
			throw new NotImplementedException();
		}

		public void AddGroup(ApplicationGroup group)
		{
			throw new NotImplementedException();
		}

		public void RemoveGroup(ApplicationGroup group)
		{
			throw new NotImplementedException();
		}

		public override AddRequest CreateNew()
		{
			AddRequest ar = base.CreateNew();

			ar.Attributes.Add(CreateAttribute(SAMACCOUNTNAME, "$" + Guid.NewGuid()));
			int value = 0;
			switch (GroupType)
			{
				case GroupType.Basic:
					value = 0x10;
					break;
				case GroupType.LdapQuery:
					value = 0x20;
					break;
				default:
					throw new AzException("Unknown group type.");
			}
			ar.Attributes.Add(CreateAttribute(GROUPTYPE, value.ToString()));
			if (GroupType == AzAlternative.GroupType.LdapQuery)
				ar.Attributes.Add(CreateAttribute(LDAPQUERY, LdapQuery));

			return ar;
		}

		public override ModifyRequest GetUpdate()
		{
			ModifyRequest mr = base.GetUpdate();

			SetAttribute(mr.Modifications, LDAPQUERY, LdapQuery);

			Changes.Clear();
			return mr;
		}

		public override void Load(SearchResultEntry entry)
		{
			ChangeTrackingDisabled = true;

			base.Load(entry);

			switch (GetAttribute(entry.Attributes,GROUPTYPE))
			{
				case "32":
					GroupType = AzAlternative.GroupType.LdapQuery;
					break;
				case "16":
					GroupType = AzAlternative.GroupType.Basic;
					break;
				default:
					throw new AzException("Unknown or unsupported group type");
			}

			Members = new Collections.MemberCollection(Service, Key);
			Exclusions = new Collections.MemberCollection(Service, Key, true);
			Groups = new Collections.ApplicationGroupCollection(Service, true);

			ChangeTrackingDisabled = false;
		}

		public static Collections.ApplicationGroupCollection GetCollection(AdService service, string key, bool linked)
		{
			var results = service.Load(key, "(ObjectClass=" + CLASSNAME + ")");
			var q = from i in results.Cast<SearchResultEntry>() select i;
			return new Collections.ApplicationGroupCollection(service, q.ToDictionary(x => x.Attributes["cn"][0].ToString(), x => x.DistinguishedName), linked);
		}

		public static DirectoryAttribute GetMembers(SearchResultEntry entry, bool isExclusions)
		{
			return entry.Attributes[isExclusions ? NONMEMBERS : MEMBERS];
		}
	}
}