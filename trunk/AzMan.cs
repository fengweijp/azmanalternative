using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	/// <summary>
	/// Authorisation Manager (azMan) main API
	/// </summary>
	public class AzMan
	{
		/// <summary>
		/// Gets the Authorisation store container
		/// </summary>
		public AdminManager AdminManager
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the current connection string
		/// </summary>
		public string ConnectionString
		{
			get;
			private set;
		}

		/// <summary>
		/// Creates a new instance of the azman API
		/// </summary>
		public AzMan()
		{
		}

		/// <summary>
		/// Open a new azMan store using the connection string
		/// </summary>
		/// <param name="connectionString">connection string for connecting to a store. Should start with either msxml or msldap</param>
		public void Open(string connectionString)
		{

			ConnectionString = connectionString;

			if (connectionString.StartsWith("msxml"))
			{
				Xml.XmlService f = new Xml.XmlService(connectionString);

				AdminManager = f.GetAdminManager();
			}
		}
	}
}
