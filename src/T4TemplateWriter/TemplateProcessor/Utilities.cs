// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.Collections.Generic;
using System.IO;
using Vipr.T4TemplateWriter.Settings;

namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    public static class Utilities
    {

        /// <summary>
        /// Reads the templates and generates an IEnumerable of TemplateFileInfos.  If 
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="templateMapping"></param>
        /// <returns></returns>
        //public static IEnumerable<ITemplateInfo> ReadTemplateFiles(String rootPath, ITemplateInfoProvider templateInfoProvider)
        //{
        //    foreach (String path in (Directory.EnumerateFiles(Path.Combine(rootPath, ConfigurationService.Settings.TargetLanguage), "*.*.tt", SearchOption.AllDirectories)))
        //    {
        //        yield return templateInfoProvider.Create(path);
        //    }
        //}

        //public static IEnumerable<ITemplateInfo> CopyAndReadTemplateFiles(String rootPath, ITemplateInfoProvider templateInfoProvider)
        //{
        //    String tempPath = Path.Combine(Path.GetTempPath() + Guid.NewGuid().ToString("D"));
        //    CopyHelper.CopyDirectoryR(rootPath, tempPath);
        //    return ReadTemplateFiles(tempPath, templateInfoProvider);
        //}

        class CopyHelper
        {
            // from https://msdn.microsoft.com/en-us/library/system.io.directoryinfo.aspx
            public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
            {
                if (source.FullName.ToLower() == target.FullName.ToLower())
                {
                    return;
                }

                // Check if the target directory exists, if not, create it. 
                if (Directory.Exists(target.FullName) == false)
                {
                    Directory.CreateDirectory(target.FullName);
                }

                // Copy each file into it's new directory. 
                foreach (FileInfo fi in source.GetFiles())
                {
                    Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                    fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                }

                // Copy each subdirectory using recursion. 
                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir =
                        target.CreateSubdirectory(diSourceSubDir.Name);
                    CopyAll(diSourceSubDir, nextTargetSubDir);
                }
            }

            public static void CopyDirectoryR(String sourceDirectory, String targetDirectory)
            {
                DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
                DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

                CopyAll(diSource, diTarget);
            }
        }


    }
}
