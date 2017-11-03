// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Java
{
    /**
     * This class contains a list of constants that define methods that should be
     * added at generation time
     * The field name should be equal to the name of the class you wish to edit
     * This will be used in BaseModel.template.tt in the AddCustomCode() method
     */
    public class CustomMethods
    {
        public const string DriveItemRequestBuilder = 
            "    @Override\r\n" +
            "    public IDriveItemRequestBuilder getItemWithPath(final String path) {\r\n" +
            "        return new DriveItemRequestBuilder(getRequestUrl() + \":/\" + path + \":\", getClient(), null);\r\n" +
            "    }";

        public const string IDriveItemRequestBuilder =
            "    /**\r\n" +
            "     * Gets the item request builder for the specified item path\r\n" +
            "     * @param path The path to the item\r\n" +
            "     * @return The request builder for the specified item\r\n" +
            "     */\r\n" +
            "    IDriveItemRequestBuilder getItemWithPath(final String path);";

        public const string IThumbnailSetRequestBuilder =
            "    /**\r\n" +
            "     * Gets a request for a thumbnail of a specific size\r\n" +
            "     * @param size The size to request (typically: small, medium, large)\r\n" +
            "     * @return The request builder for that thumbnail size\r\n" +
            "     */\r\n" +
            "    IThumbnailRequestBuilder getThumbnailSize(final String size);";

        public const string ThumbnailSetRequestBuilder =
            "    @Override\r\n" +
            "    public IThumbnailRequestBuilder getThumbnailSize(final String size) {\r\n" +
            "        return new ThumbnailRequestBuilder(getRequestUrlWithAdditionalSegment(size), getClient(), /* options */ null);\r\n" +
            "    }";
    }
}
