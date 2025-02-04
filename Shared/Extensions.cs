using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaudiaIDE
{
    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)  
        {
            var rng = new Random();
            var n = list.Count;  
            while (n > 1) {  
                n--;  
                var k = rng.Next(n + 1);  
                var value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }

    public static class VisualStudioUtility
    {

        public static string GetSolutionSettingsFileFullPath(bool checkExists = true)
        {
            try
            {
                var dte = Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;
                var path = dte?.Solution?.FileName;
                if (string.IsNullOrEmpty(path)) return null;

                // CMake or other directory project (not .sln) `path` is directory
                var dir = File.GetAttributes(path).HasFlag(FileAttributes.Directory) ? path : Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(dir)) return null;

                var configpath = Path.Combine(dir, ".claudiaideconfig");
                if (checkExists)
                {
                    return File.Exists(configpath) ? configpath : null;
                }
                else
                {
                    return configpath;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
