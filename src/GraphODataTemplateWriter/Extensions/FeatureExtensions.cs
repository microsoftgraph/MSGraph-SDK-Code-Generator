using Vipr.Core.CodeModel;

namespace Microsoft.Graph.ODataTemplateWriter.Extensions
{
    public sealed class Features
    {
        private readonly OdcmProjection _projection;

        internal Features(OdcmProjection projection)
        {
            _projection = projection;
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
        public bool CanCast { get { return _projection.BooleanValueOf("TypecastSegmentSupported") ?? false; } }
    }

    public static class OdcmTypeFeatureExtensions
    {
        public static Features GetFeatures(this OdcmObject odcmObject)
        {
            return new Features(odcmObject.Projection);
        }
    }
}
