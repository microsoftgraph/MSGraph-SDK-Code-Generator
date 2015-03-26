// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using Vipr.Core.CodeModel;

namespace T4TemplateWriter.Helpers.Swift
{
    public static class PropertyHelper
    {
        public static string GetTypeString(this OdcmProperty property)
        {
            switch (property.Type.Name)
            {
                case "String":
                    return "String";
                case "Int32":
                    return "Int";
                case "Guid":
                    return "String";
                case "DateTimeOffset":
                    return "NSDate";
                case "Binary":
                    return "Byte";
                case "Boolean":
                    return "Bool";
                default:
                    return property.Type.Name;
            }
        }

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return Char.ToLowerInvariant(property.Name[0]) + property.Name.Substring(1);
        }
    }
}
