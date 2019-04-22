using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Persada.Fr.CommonFunction;
using Persada.Fr.ModelView;
using Newtonsoft.Json;
using System.IO;

namespace Persada.Fr.CommonFunction
{
    public class Logging
    {       
        public ResultStatus CreateLogging(LoggingObject log)
        {
            ResultStatus rs = new ResultStatus();
            try
            {
                DateTime createdTime = CurrentUser.GetCurrentDateTime();
                string path = HttpContext.Current.Server.MapPath("~/Log");
                string fileName = "TourismLog_" + createdTime.ToShortDateString().Replace("/", "-") + ".txt";
                string pathFile = Path.Combine(path, fileName);

                StreamWriter sw = new StreamWriter(pathFile, true);
                sw.WriteLine("===================================================" + createdTime.ToString() + "===================================================");
                sw.WriteLine("URL\t\t\t\t\t = " + log.Url);
                sw.WriteLine("Request\t\t\t\t\t = " + log.Request);
                sw.WriteLine("Respon\t\t\t\t\t = " + log.Respon);
                sw.WriteLine("Message\t\t\t\t\t = " + log.ExceptionMessage);
                sw.WriteLine("Stack\t\t\t\t\t = " + log.ExceptionStack);
                sw.WriteLine("=========================================================================================================================");
                sw.Close();

                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }
    }
}
