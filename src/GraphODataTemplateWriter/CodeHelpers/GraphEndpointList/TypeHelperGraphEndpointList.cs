// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.GraphEndpointList
{
    using Vipr.Core.CodeModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TypeHelperGraphEndpointList
    {

        public static OdcmClass FindEntityByName(this IEnumerable<OdcmClass> entities, String name)
        {
            foreach (var entity in entities)
            {
                if (entity.Name.Equals(name))
                {
                    return entity;
                }
            }
            return null;
        }

        public static List<string> GetNavigationPaths(this OdcmProperty prop, string baseString, IEnumerable<OdcmClass> entities, int depth)
        {
            List<String> paths = new List<string>();
            if (depth > 5) return paths;
            OdcmClass entity = entities.FindEntityByName(prop.Type.Name);

            string newBase;
            if (prop.IsCollection)
            {
                newBase = baseString + "/" + prop.Name + "/{" + prop.Type.Name + "Id}";
                paths.Add(baseString + "/" + prop.Name);
            }
            else
            {
                newBase = baseString + "/" + prop.Name;

            }

            paths.Add(newBase);
            if (entity.Methods.Any()) // check for actions
            {
                foreach (var method in entity.Methods)
                {
                    paths.Add(newBase + "/" + method.Name);
                }
            }

            foreach (var eprop in entity.Properties)
            {
                if (eprop.IsLink)
                {
                    paths.AddRange(GetNavigationPaths(eprop, newBase, entities, depth + 1));
                }
            }


            return paths;
        }

    }

}
