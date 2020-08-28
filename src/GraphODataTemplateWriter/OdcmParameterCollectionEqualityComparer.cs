using System;
using System.Collections.Generic;
using System.Linq;
using Vipr.Core.CodeModel;

namespace Microsoft.Graph.ODataTemplateWriter
{
    public class OdcmParameterCollectionEqualityComparer : IEqualityComparer<IEnumerable<OdcmParameter>>
    {
        private static readonly OdcmParameterEqualityComparer paramComparer = new OdcmParameterEqualityComparer();
        public bool Equals(IEnumerable<OdcmParameter> x, IEnumerable<OdcmParameter> y)
        {
            if (x == null || y == null)
                return x == y;
            else
            {
                var orderedY = y.OrderBy(z => z.Name);
                return x.Count() == y.Count()
                        && x.OrderBy(z => z.Name).Select((z, idx) => paramComparer.Equals(z, orderedY.ElementAt(idx))).Aggregate((z, w) => z && w);
            }
        }

        public int GetHashCode(IEnumerable<OdcmParameter> obj)
        {
            unchecked
            {
                return (obj?.Count() ?? 0) * 23 + (obj != null && obj.Any() ? 
                                                    (obj?.Select(x => paramComparer.GetHashCode(x))?.Aggregate((x, y) => x ^ y) ?? 0) : 
                                                    0);
            }
        }
    }
}
