using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace AzAlternative
{
	public class Member : IEquatable<Member>
	{
		private string _Name;

		internal Interfaces.IMember Instance
		{
			get;
			private set;
		}

		public string Name
		{
			get
			{
				if (_Name != null)
					return _Name;
				try
				{
					SecurityIdentifier s = new SecurityIdentifier(Instance.Sid);
					_Name = s.Translate(typeof(NTAccount)).Value;
				}
				catch (Exception ex)
				{
					_Name = Instance.Sid;
				}
				return _Name;
			}
		}

		public bool IsExclusion
		{
			get { return Instance.IsExclusion; }
		}

		internal Member(Interfaces.IMember instance)
		{
			Instance = instance;
		}

		internal Member(Interfaces.IMember instance, string name)
			: this(instance)
		{
			try
			{
				Instance.Sid = ToSid(name);
				_Name = name;
			}
			catch(Exception ex)
			{
				throw new AzException("The username could not be converted to a SID", ex);
			}
		}

		public bool Equals(Member other)
		{
			return other.Instance.Sid == Instance.Sid;
		}

		internal static string ToSid(string name)
		{
			NTAccount a = new NTAccount(name);
			return a.Translate(typeof(SecurityIdentifier)).Value;
		}
	}

	//internal class MemberComparer : IEqualityComparer<Member>
	//{
	//    public bool Equals(Member x, Member y)
	//    {
	//        return x.Name.ToUpper() == y.Name.ToUpper();
	//    }

	//    public int GetHashCode(Member obj)
	//    {
	//        return obj.GetHashCode();
	//    }
	//}
}
