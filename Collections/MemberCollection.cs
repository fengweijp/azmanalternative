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

		internal MemberCollection(string key)
			: this(key, false)
		{ }

		internal MemberCollection(string key, bool isExlusions)
		{
			_Service = Locator.Service;
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

		//public void Remove(Member member)
		//{
		//    member.Instance.Remove();
		//}

		//public void Remove(string name)
		//{
		//    string tmp = Member.ToSid(name);
		//    Member m = this.First(item => item.Instance.Sid == tmp);

		//    RemoveMember(m);
		//}

		//public void Add(string name)
		//{
		//    Member m = new Member(new XmlMember(Service), name);
		//    m.Instance.Parent = this.Key;

		//    if (Members.Contains(m))
		//        return;

		//    m.Instance.Save();
		//}

	}
}
