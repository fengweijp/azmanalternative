using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative.ActiveDirectory
{
    internal class AdOperation : AdBaseObject, Interfaces.IOperation
    {

        public int OperationId
        {
            get;
            set;
        }

        public AdOperation(AdService service)
            : base(service)
        { }
    }
}
