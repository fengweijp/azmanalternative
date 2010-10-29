using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	/// <summary>
	/// Base class for loading store objects. Wraps up the store provider
	/// </summary>
	internal abstract class FactoryBase
	{
        /// <summary>
        /// Gets the connectionstring used by this factory object
        /// </summary>
		public string ConnectionString
		{
			get;
			protected set;
		}

        /// <summary>
        /// Loads the underlying data used by the factory
        /// </summary>
		public abstract void Load();

        /// <summary>
        /// Loads the underlying data used by the factory
        /// </summary>
        /// <param name="connectionString">Connection string to initialise the factory</param>
		public virtual void Load(string connectionString)
		{
			ConnectionString = connectionString;
			Load();
		}

	}
}
