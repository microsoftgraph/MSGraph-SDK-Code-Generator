using System;
using System.Collections.Generic;
using System.Linq;
using Vipr.Core.CodeModel;

namespace Microsoft.Graph.ODataTemplateWriter
{
    public class OdcmMethodEqualityComparer : IEqualityComparer<OdcmMethod>
    {
        public bool CompareParameters
        {
            get; set;
        } = true;
        public bool CompareParametersCount
        {
            get; set;
        } = false;
        public bool CompareHasParameters
        {
            get; set;
        } = false;
        public bool CompareReturnType
        {
            get; set;
        } = false;
        private static readonly OdcmParameterCollectionEqualityComparer paramComparer = new OdcmParameterCollectionEqualityComparer();
        public bool Equals(OdcmMethod x, OdcmMethod y)
        {
            return x.FullName == y.FullName && x.Class.FullName == y.Class.FullName &&
                (!CompareParameters || paramComparer.Equals(y?.Parameters, x?.Parameters)) &&
                (!CompareParametersCount || y?.Parameters?.Count == x?.Parameters?.Count) &&
                (!CompareHasParameters || y?.Parameters?.Any() == x?.Parameters?.Any()) &&
                (!CompareReturnType || y?.ReturnType?.FullName == x?.ReturnType?.FullName);
        }
        public int GetHashCode(OdcmMethod obj)
        {
            unchecked
            {
                return (obj?.FullName?.GetHashCode() ?? 0) * 23 + 
                    (CompareParameters ? paramComparer.GetHashCode(obj?.Parameters) : 0) + 
                    (CompareParametersCount ? obj?.Parameters?.Count ?? 0 : 0) +
                    (CompareHasParameters ? (obj?.Parameters?.Any() ?? false).GetHashCode() : 0);
            }
        }
    }
}
