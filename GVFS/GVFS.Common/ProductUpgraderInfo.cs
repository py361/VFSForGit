using System;
using System.Collections.Generic;
using System.IO;

namespace GVFS.Common
{
    public class ProductUpgraderInfo
    {
        public const string UpgradeDirectoryName = "GVFS.Upgrade";
        public const string LogDirectory = "Logs";
        public const string DownloadDirectory = "Downloads";
        public const string GVFSInstallerFileNamePrefix = "SetupGVFS";
        public const string VFSForGitInstallerFileNamePrefix = "VFSForGit";
        public const string RootDirectory = UpgradeDirectoryName;

        public static bool IsLocalUpgradeAvailable(string installerExtension)
        {
            string downloadDirectory = GetAssetDownloadsPath();
            if (Directory.Exists(downloadDirectory))
            {
                HashSet<string> installerNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    GVFSInstallerFileNamePrefix,
                    VFSForGitInstallerFileNamePrefix
                };

                foreach (string file in Directory.EnumerateFiles(downloadDirectory, "*", SearchOption.TopDirectoryOnly))
                {
                    string[] components = Path.GetFileName(file).Split('.');
                    int length = components.Length;
                    if (length >= 2 &&
                        installerNames.Contains(components[0]) &&
                        installerExtension.Equals(components[length - 1], StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetUpgradesDirectoryPath()
        {
            return Paths.GetServiceDataRoot(RootDirectory);
        }

        public static string GetLogDirectoryPath()
        {
            return Path.Combine(Paths.GetServiceDataRoot(RootDirectory), LogDirectory);
        }

        public static string GetAssetDownloadsPath()
        {
            return Path.Combine(
                Paths.GetServiceDataRoot(RootDirectory),
                DownloadDirectory);
        }
    }
}
