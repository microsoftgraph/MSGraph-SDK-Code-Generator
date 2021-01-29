// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class CollectionExtensions
    {
        /// <summary>
        /// Retuns an empty collection if the reference is null.
        /// Use this when iterating over a collection in the templates to handle potential null collections.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects">A collection of objects.</param>
        /// <returns>The collection or an empty collection.</returns>
        public static IEnumerable<T> OrEmptyCollectionIfNull<T>(this IEnumerable<T> objects) where T : class => objects ?? Enumerable.Empty<T>();
    }
}
