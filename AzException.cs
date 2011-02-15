using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	/// <summary>
	/// Exception used when AzMan throws an API related exception
	/// </summary>
	public class AzException : Exception
	{
		public AzException()
			: base()
		{ }

		public AzException(string message)
			: base(message)
		{ }

		public AzException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
