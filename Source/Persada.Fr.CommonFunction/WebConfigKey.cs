using System;
using System.Configuration;

namespace Persada.Fr.CommonFunction
{
    class WebConfigKey
    {
        public static string ImgLoading
        {
            get { return ConfigurationSettings.AppSettings["ImgLoading"].ToString(); }
        }
        public static string KeyUrl
        {
            get { return ConfigurationSettings.AppSettings["KeyUrl"].ToString(); }
        }
        public static string templateName
        {
            get { return ConfigurationSettings.AppSettings["templateName"].ToString(); }
        }
    }
}
