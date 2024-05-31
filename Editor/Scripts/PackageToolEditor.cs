using System.IO;
using UnityEditor;
using UnityEngine;

namespace PackageTool
{
    public static class PackageToolEditor
    {
        [MenuItem("PackageTool/Copy Samples", false)]
        public static void CopySamples()
        {
            string srcDirName = Path.Combine(Application.dataPath, "Samples");
            string desDirName = Path.Combine(Application.dataPath, "Package/Samples~");
            string[] filesPath = Directory.GetFiles(srcDirName, "*", SearchOption.AllDirectories);
            if (filesPath.Length <= 0)
            {
                return;
            }

            if (!Directory.Exists(desDirName))
            {
                Directory.CreateDirectory(desDirName);
            }

            foreach (string filePath in filesPath)
            {
                string desFilePath = filePath.Replace("Samples", "Package/Samples~");
                SafeCopyFile(filePath, desFilePath, true);
            }
        }

        private static string GetPackagePah()
        {
            string package = Path.GetFullPath("Packages/com.cetejs.gitcommand");
            if (Directory.Exists(package))
            {
                return package;
            }

            return Path.GetFullPath("Assets/Package");
        }

        private static void SafeCopyFile(string srcPath, string desPath, bool isOverwrite = false)
        {
            if (!File.Exists(srcPath))
            {
                return;
            }

            FileInfo fileInfo = new FileInfo(desPath);
            if (!Directory.Exists(fileInfo.DirectoryName))
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }

            File.Copy(srcPath, desPath, isOverwrite);
        }
    }
}