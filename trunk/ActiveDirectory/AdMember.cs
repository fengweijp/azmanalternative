using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.ActiveDirectory
{
	internal class AdMember : AdBaseObject, Interfaces.IMember
	{
		protected override string ObjectClass
		{
			get { throw new NotImplementedException(); }
		}

		public string Sid
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public bool IsExclusion
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public string Parent
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public AdMember(AdService service)
			: base(service)
		{}

		public void Save()
		{
			throw new NotImplementedException();
		}

		public void Remove()
		{
			throw new NotImplementedException();
		}
	}
}
