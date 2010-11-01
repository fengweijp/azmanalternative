using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	/// <summary>
	/// Base class for loading store objects. Wraps up the store provider
	/// </summary>
	internal abstract class ServiceBase
	{
        /// <summary>
        /// Gets the connectionstring used by this factory object
        /// </summary>
		public string ConnectionString
		{
			get;
			protected set;
		}

		public abstract AdminManager GetAdminManager();
	}
}
