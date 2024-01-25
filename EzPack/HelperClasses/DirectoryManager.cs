using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EzPack.HelperClasses
{
    static class DirectoryManager
    {
        static public bool init = false;
        static public string getCurrentDir()
        {
            return Environment.CurrentDirectory;
        }
        static public string getDesktop()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
        static public string getDocuments()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
        static public string getMyGames()
        {
            return getDocuments() + @"\My Games";
        }
        public static bool FileExists(string fileName)
        {
            if (File.Exists(getCurrentDir() + $@"\{fileName}"))
            {
                return true;
            }
            return false;
        }
        public static string GetJointedFile(string fileName)
        {
            return getCurrentDir() + $@"\{fileName}";
        }
        public static string GetCurrentWorkspaceDir()
        {
            //if (init == false) {return "null";}
            string line;
            using (StreamReader sr = new StreamReader(GetJointedFile("dir.txt")))
            {
                line = sr.ReadLine();
                sr.Close();
            }

            return line;
        }
        public static string GetRelativePath(string fullPath, string customRoot)
        {
            customRoot = Path.GetFullPath(customRoot);
            fullPath = Path.GetFullPath(fullPath);

            if (!fullPath.StartsWith(customRoot, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("The custom root and full path must have the same root.");
            }
            string relativePath = fullPath.Substring(customRoot.Length);
            if (relativePath.StartsWith(Path.DirectorySeparatorChar.ToString()))
            {
                relativePath = relativePath.Substring(1);
            }

            return relativePath;
        }

    }
}
