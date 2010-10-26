using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	/// <summary>
	/// Base class for loading store objects. Wraps up the store provider
	/// </summary>
	public abstract class FactoryBase
	{

		public string ConnectionString
		{
			get;
			protected set;
		}

		public abstract void Load();

		public virtual void Load(string connectionString)
		{
			ConnectionString = connectionString;
			Load();
		}

	}
}
