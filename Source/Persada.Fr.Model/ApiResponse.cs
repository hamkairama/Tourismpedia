using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Model
{
    public class ApiSaveRespone
    {
        public ApiSaveRespone()
        {
            Header = new ResponseHeader();
        }
        public ResponseHeader Header { get; }
        public object Body { get; set; }
        public object Footer { get; set; }
    }

    public class ResponseHeader
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class ApiGridResponse
    {
        public ApiGridResponse()
        {
            Header = new ResponseHeader();
            Body = new ResponseGrid();
        }
        public ResponseHeader Header { get; set; }
        public ResponseGrid Body { get; set; }
        public object Footer { get; set; }
    }

    public class ResponseGrid
    {
        public object Aggregates { get; set; }
        public object Data { get; set; }
        public int Total { get; set; }
    }

    public class ResponseListToObj
    {
        public object Data { get; set; }
    }

    public class ApiStatus
    {
        public ApiStatus()
        {
            res = new ResponeseStatus()
            {
                Success = "0",
                Failed = "-1",
                SaveMsg = "Simpan Data Berhasil.",
                EditMsg = "Edit Data Berhasil.",
                DeleteMsg = "Delete Data Berhasil.",
                BindData = "Success"
            };
        }
        public ResponeseStatus res { get; set; }
    }

    public class ResponeseStatus
    {
        public string Success { get; set; }
        public string Failed { get; set; }
        public string BindData { get; set; }
        public string SaveMsg { get; set; }
        public string EditMsg { get; set; }
        public string DeleteMsg { get; set; }
    }

    public class ApiResData
    {
        /// <summary>
        /// </summary>
        /// <param name="par">value dari class object</param>
        /// <param name="ex">Exception yang dikirim dari method jk masuk catch / bisa jg di harcode : new Exception(--string--)</param>
        /// <returns></returns>
        public ApiGridResponse ResGetDataTable(object[] par, Exception ex)
        {
            ApiGridResponse res = new ApiGridResponse();
            ApiStatus st = new ApiStatus();
            if (ex == null)
            {
                res.Header.Status = st.res.Success;
                res.Header.Message = st.res.BindData;                
            }
            else
            {
                res.Header.Status = st.res.Failed;
                res.Header.Message = ex.Message;
            }
            res.Body.Data = par;

            return res;
        }

        /// <summary>
        /// </summary>
        /// <param name="FnFunc">flag dari method seperti save, edit delete</param>
        /// <param name="ex">Exception yang dikirim dari method jk masuk catch / bisa jg di harcode : new Exception(--string--)</param>
        /// <returns></returns>
        public ApiGridResponse ResCUD(object[] par, string FnFunc, Exception ex)
        {
            ApiGridResponse res = new ApiGridResponse();
            ApiStatus st = new ApiStatus();
            if (ex == null)
            {
                res.Header.Status = st.res.Success;
                switch (FnFunc.ToLower())
                {
                    case "save":
                        res.Header.Message = st.res.SaveMsg;
                        break;
                    case "edit":
                        res.Header.Message = st.res.EditMsg;
                        break;
                    case "delete":
                        res.Header.Message = st.res.DeleteMsg;
                        break;
                }
            }
            else
            {
                res.Header.Status = st.res.Failed;
                res.Header.Message = ex.Message;
            }

            res.Body.Data = par;
            return res;
        }
    }
}