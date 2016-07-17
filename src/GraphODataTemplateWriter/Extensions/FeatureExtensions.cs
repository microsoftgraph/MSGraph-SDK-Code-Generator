using Vipr.Core.CodeModel;
using System;
using System.Linq;
using System.Collections.Generic;
using Vipr.Core.CodeModel.Vocabularies.Capabilities;

namespace Microsoft.Graph.ODataTemplateWriter.Extensions
{

    class CapabilityComparer : IEqualityComparer<OdcmCapability>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(OdcmCapability x, OdcmCapability y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.TermName.Equals(y.TermName);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(OdcmCapability capability)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(capability, null)) return 0;

            //Get hash code for the Term Name field if it is not null.
            int hashCapabilityName = capability.TermName == null ? 0 : capability.TermName.GetHashCode();

            return hashCapabilityName;
        }

    }

    public sealed class Features
    {
        private readonly OdcmProjection _projection;

        internal Features(OdcmProjection projection)
        {
            _projection = projection;

            // Remove duplicate capabilities.
            var capabilities = _projection.Capabilities.Distinct(new CapabilityComparer());
            _projection.Capabilities = capabilities.ToList();            
        }

        public bool CanCreate { get { return _projection.SupportsInsert(); } }
        public bool CanDelete { get { return _projection.SupportsDelete(); } }
        public bool CanUpdate { get { return _projection.SupportsUpdate(); } }
        public bool CanExpand { get { return _projection.SupportsExpand(); } }
        public bool CanSelect { get { return _projection.BooleanValueOf("Selectable") != false; } }
        public bool CanUseTop { get { return _projection.BooleanValueOf("TopSupported") != false; } }
        public bool CanFilter { get { return _projection.BooleanValueOf("Filterable") != false; } }
        public bool CanSkip { get { return _projection.BooleanValueOf("SkipSupported") != false; } }
        public bool CanSort { get { return _projection.BooleanValueOf("Sortable") != false; } }
    }

    public static class OdcmTypeFeatureExtensions
    {
        public static Features GetFeatures(this OdcmObject odcmObject)
        {
            return new Features(odcmObject.Projection);
        }
    }
}
