using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.Collections
{
	public class MemberCollection : System.Collections.ObjectModel.ReadOnlyCollection<Member>
	{
		private bool _IsExclusions;

		internal MemberCollection(IList<AzAlternative.Member> members)
			: base(members)
		{ }

		internal MemberCollection(IList<Member> members, bool isExlusions)
			: this(members)
		{
			_IsExclusions = isExlusions;
		}

		internal void AddMember(Member member)
		{
			if (this.Contains(member))
				return;

			member.Instance.IsExclusion = _IsExclusions;
			member.Instance.Save();
			this.Items.Add(member);
			
		}

		internal void RemoveMember(Member member)
		{
			member.Instance.Remove();
			this.Items.Remove(member);
		}

		
	}
}
