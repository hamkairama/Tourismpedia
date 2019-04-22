using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persada.Fr.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.CommonFunction
{
    public static class ParsingObject
    {

        /// <summary>
        /// use for retrived data one record
        /// </summary>
        /// <param name="param"></param>
        /// <param name="controller"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static string RequestData(this object param, string controller, string method, string httpMethod)
        {
            LoggingObject log = new LoggingObject();
            ResultStatus rs = new ResultStatus();
            string pathApi = Common.GetPathApi() + controller + "/" + method;
            Logging logging = new Logging();
            string response = "";

            try
            {
                InvokeUrl url = new InvokeUrl();
                string qryString = "";

                if (httpMethod == EnumList.IHttpMethod.Post.ToString())
                    qryString = ObjectToDictionaryHelper.GenericObjectToString(param);
                
                if (httpMethod == EnumList.IHttpMethod.Put.ToString())
                    qryString = param.ToString();

                JObject jsonDes = JObject.Parse(url.ReturnJson(pathApi, httpMethod, qryString));
                response = jsonDes["Body"]["Data"][0].ToString();

                log.Url = pathApi;
                log.Request = qryString;
                log.Respon = response;
            }
            catch (Exception ex)
            {
                log.ExceptionMessage = ex.Message;
                log.ExceptionStack = ex.StackTrace;
            }
            
            rs = logging.CreateLogging(log);

            return response;
        }
    }
}

