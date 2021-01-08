// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Java
{
    /**
     * This class contains a list of constants that define code snippets that should
     * be overwritten at generation time 
     * The field name should be equal to the name of the class you wish to edit
     * The dictionary value should contain two string, the first is the search value
     * and the latter is the replace value
     * This will be used in BaseModel.template.tt in the PostProcess() method
     */
    public class CustomOverwrites
    {
        public static Dictionary<string, string> OnenotePageCollectionRequest = new Dictionary<string, string>() {
            {
                "public java.util.concurrent.Future<? super OnenotePage> futurePost(@Nonnull final OnenotePage newOnenotePage) {",
                "public java.util.concurrent.Future<? super OnenotePage> futurePost(@Nonnull final byte[] newOnenotePage) {"
            },
            {
                "public OnenotePage post(@Nonnull final OnenotePage newOnenotePage) throws ClientException {",
                "public OnenotePage post(@Nonnull final byte[] newOnenotePage) throws ClientException {"
            }
        };

        public static Dictionary<string, string> OnenotePageRequest = new Dictionary<string, string>()
        {
            {
                "public java.util.concurrent.Future<? super OnenotePage> post(@Nonnull final OnenotePage newOnenotePage) {",
                "public java.util.concurrent.Future<? super OnenotePage> post(@Nonnull final byte[] newOnenotePage) {"
            },
            {
                "public OnenotePage post(@Nonnull final OnenotePage newOnenotePage) throws ClientException {",
                "public OnenotePage post(@Nonnull final byte[] newOnenotePage) throws ClientException {"
            }
        };

        public static Dictionary<string, string> IOnenotePageCollectionRequest = new Dictionary<string, string>()
        {
            {
                "java.util.concurrent.Future<? super OnenotePage> post(@Nonnull final OnenotePage newOnenotePage);",
                "java.util.concurrent.Future<? super OnenotePage> post(@Nonnull final byte[] newOnenotePage);"
            },
            {
                "OnenotePage post(@Nonnull final OnenotePage newOnenotePage) throws ClientException;",
                "OnenotePage post(@Nonnull final byte[] newOnenotePage) throws ClientException;"
            }
        };

        public static Dictionary<string, string> IOnenotePageRequest = new Dictionary<string, string>()
        {
            {
                "java.util.concurrent.Future<? super OnenotePage> post(@Nonnull final OnenotePage newOnenotePage);",
                "java.util.concurrent.Future<? super OnenotePage> post(@Nonnull final byte[] newOnenotePage);"
            },
            {
                "OnenotePage post(@Nonnull final OnenotePage newOnenotePage) throws ClientException;",
                "OnenotePage post(@Nonnull final byte[] newOnenotePage) throws ClientException;"
            }
        };

        public static Dictionary<string, string> PlannerAssignments = new Dictionary<string, string>()
        {
            {
                "public class PlannerAssignments implements IJsonBackedObject {",
                "public class PlannerAssignments extends HashMap<String, PlannerAssignment> implements IJsonBackedObject {"
            }
        };

        public static Dictionary<string, string> PlannerChecklistItems = new Dictionary<string, string>()
        {
            {
                "public class PlannerChecklistItems implements IJsonBackedObject {",
                "public class PlannerChecklistItems extends HashMap<String, PlannerChecklistItem> implements IJsonBackedObject {"
            }
        };

        public static Dictionary<string, string> PlannerExternalReferences = new Dictionary<string, string>()
        {
            {
                "public class PlannerExternalReferences implements IJsonBackedObject {",
                "public class PlannerExternalReferences extends HashMap<String, Object> implements IJsonBackedObject {"
            }
        };

        public static Dictionary<string, string> PlannerOrderHintsByAssignee = new Dictionary<string, string>()
        {
            {
                "public class PlannerOrderHintsByAssignee implements IJsonBackedObject {",
                "public class PlannerOrderHintsByAssignee extends HashMap<String, String> implements IJsonBackedObject {"
            }
        };
    }
}
