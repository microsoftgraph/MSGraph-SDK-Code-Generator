using System;
using System.Collections.Generic;
using Vipr.Core.CodeModel;

namespace Microsoft.Graph.ODataTemplateWriter
{
    public class OdcmParameterEqualityComparer : IEqualityComparer<OdcmParameter>
    {
        private static OdcmTypeEqualityComparer typeComparer = new OdcmTypeEqualityComparer();
        public bool Equals(OdcmParameter x, OdcmParameter y)
        {
            return x?.Name == y?.Name && x?.IsCollection == y?.IsCollection && x?.IsNullable == y?.IsNullable && typeComparer.Equals(x?.Type, y?.Type);
        }

        public int GetHashCode(OdcmParameter obj)
        {
            unchecked
            {
                return obj == null ? 0 : obj.Name.GetHashCode() * 23 + obj.IsCollection.GetHashCode() * 13 + obj.IsNullable.GetHashCode() * 11 + typeComparer.GetHashCode(obj.Type);
            }
        }
    }
}
