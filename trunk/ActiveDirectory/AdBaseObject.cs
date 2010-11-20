using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.ActiveDirectory
{
    internal class AdBaseObject
    {
        protected const string GUID = "Guid";
        protected const string NAME = "Name";
        protected const string DESCRIPTION = "Description";

        public Guid Guid
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public AdBaseObject(AdService service)
        {
        }
    }
}
