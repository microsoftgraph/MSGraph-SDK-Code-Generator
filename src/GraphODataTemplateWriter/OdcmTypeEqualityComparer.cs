using System;
using System.Collections.Generic;
using Vipr.Core.CodeModel;

namespace Microsoft.Graph.ODataTemplateWriter
{
    public class OdcmTypeEqualityComparer : IEqualityComparer<OdcmType>
    {
        public bool Equals(OdcmType x, OdcmType y)
        {
            return x?.FullName == y?.FullName;
        }

        public int GetHashCode(OdcmType obj)
        {
            unchecked
            {
                return obj?.FullName?.GetHashCode() ?? 0;
            }
        }
    }
}
