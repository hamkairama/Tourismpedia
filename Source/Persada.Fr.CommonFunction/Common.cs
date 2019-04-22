using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.CommonFunction
{
    public class Common
    {
        public static string GetFileName(string fullPath)
        {
            string[] split = fullPath.Split(new[] { "\\" }, StringSplitOptions.None);
            string rs = split[split.Count() - 1];

            return rs;
        }

        public static string GetPathApi()
        {
            return ConfigurationSettings.AppSettings["PathAPI"];
        }

        public static string GetPathFolderImg()
        {
            return ConfigurationSettings.AppSettings["PathFolderImg"];
        }

        public static string StandardDateTime(DateTime dt)
        {
            return dt.ToString("d-MMM-yy HH:mm");
        }

    }
}
