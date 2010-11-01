using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
    /// <summary>
    /// Base class for the API objects. Not intended to be used directly
    /// </summary>
    public abstract class ContainerBase
    {
        /// <summary>
        /// Gets the application this object belongs to
        /// </summary>
		public Application Application
		{
			get;
			internal set;
		}

		///// <summary>
		///// Checks whether the child parameter is defined in the current application
		///// </summary>
		///// <param name="child">Object to check</param>
		///// <returns>true if the object is valid</returns>
		//protected virtual bool CheckObjectIsValid(BaseObject child)
		//{
		//    if (child == null)
		//        throw new ArgumentNullException("child");

		//    if (child.Application != null && child.Application.Guid != Application.Guid)
		//        throw new AzException("The object is not defined in the current application");

		//    return true;
		//}
    }
}
