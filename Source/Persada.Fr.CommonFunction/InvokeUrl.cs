using Newtonsoft.Json;
using Persada.Fr.ModelView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.CommonFunction
{
    public class InvokeUrl
    {
        private LoggingObject log = new LoggingObject();
        private ResultStatus rs = new ResultStatus();
        public string ReturnJson(string pathApi, string httpMethod, string request)
        {
            string response = "";
            StringContent requestContent = new StringContent("");
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            if (httpMethod == EnumList.IHttpMethod.Put.ToString())
                pathApi = pathApi + "?id=" + request;

            if (httpMethod == EnumList.IHttpMethod.Post.ToString())
                requestContent = new StringContent(request, UnicodeEncoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (httpMethod == EnumList.IHttpMethod.Get.ToString())
                    responseMessage = client.GetAsync(pathApi).Result;
                else if (httpMethod == EnumList.IHttpMethod.Post.ToString())
                    responseMessage = client.PostAsync(pathApi, requestContent).Result;
                else if (httpMethod == EnumList.IHttpMethod.Put.ToString())
                    responseMessage = client.GetAsync(pathApi).Result;

                using (HttpContent content = responseMessage.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    response = result.Result;
                }
            }

            return response;
        }
    }
}
