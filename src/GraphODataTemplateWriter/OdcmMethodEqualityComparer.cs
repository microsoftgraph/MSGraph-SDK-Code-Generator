using System;
using System.Collections.Generic;
using Vipr.Core.CodeModel;

namespace Microsoft.Graph.ODataTemplateWriter
{
    public class OdcmMethodEqualityComparer : IEqualityComparer<OdcmMethod>
    {
        private static readonly OdcmParameterCollectionEqualityComparer paramComparer = new OdcmParameterCollectionEqualityComparer();
        public bool Equals(OdcmMethod x, OdcmMethod y)
        {
            return x.FullName == y.FullName && paramComparer.Equals(y.Parameters, x.Parameters);
        }
        public int GetHashCode(OdcmMethod obj)
        {
            unchecked
            {
                return (obj?.FullName?.GetHashCode() ?? 0) * 23 + paramComparer.GetHashCode(obj?.Parameters);
            }
        }
    }
}
