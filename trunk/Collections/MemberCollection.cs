using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	/// <summary>
	/// A collection of members
	/// </summary>
	public class MemberCollection : IEnumerable<Member>
	{
		private bool _IsExclusions;
		private ServiceBase _Service;
		private string _Key;

		internal MemberCollection(ServiceBase service, string key)
			: this(service, key, false)
		{ }

		internal MemberCollection(ServiceBase service, string key, bool isExlusions)
		{
			_Service = service;
			_Key = key;
			_IsExclusions = isExlusions;
		}

		internal void AddMember(Member member)
		{
			if (this.Contains(member))
				return;

			member.Instance.IsExclusion = _IsExclusions;
			member.Instance.Save();
		}

		internal void RemoveMember(Member member)
		{
			member.Instance.Remove();
		}


		public IEnumerator<Member> GetEnumerator()
		{
			return _Service.GetMembers(_Key, _IsExclusions);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
