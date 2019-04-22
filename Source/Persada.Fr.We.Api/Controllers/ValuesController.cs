using Persada.Fr.Facade.Interface;
using Persada.Fr.Facade.Repository;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Persada.Fr.We.Api.Controllers
{
    //[Authorize]
    public class ResponseModel
    {
        public string Message { set; get; }
        public bool Status { set; get; }
        public object Data { set; get; }
    }
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
    public class ValuesController : ApiController
    {
        IMenu repo;
        public ValuesController()
        {
            repo = new MenuRepo();
        }

        //[httpget]
        //public ihttpactionresult binddata()
        //{
        //    apiresdata res = new apiresdata();
        //    list<touris_tm_menu> isget = repo.getdtmenu();
        //    res.resgetdatatable(new object[] { isget.cast<object>().toarray() }, null);
        //    return content(httpstatuscode.created, res);
        //}

        [HttpGet]
        public ApiGridResponse Get2()
        {
            //TOURIS_TM_MENU tm = new TOURIS_TM_MENU();
            //ApiResData res = new ApiResData();
            //List<TOURIS_TM_MENU> isGet = repo.GetDtMenu();

            ApiGridResponse _objResponseModel = new ApiGridResponse();
            ApiStatus st = new ApiStatus();

            List<Student> students = new List<Student>();
            students.Add(new Student
            {
                ID = 101,
                Name = "Seam",
                Email = "seam@gmail.com",
                Address = "Dhaka,Bangladesh"
            });
            students.Add(new Student
            {
                ID = 102,
                Name = "Mitila",
                Email = "mitila@gmail.com",
                Address = "Dhaka,Bangladesh"
            });
            students.Add(new Student
            {
                ID = 104,
                Name = "Popy",
                Email = "popy@yahoo.com",
                Address = "Dhaka,Bangladesh"
            });

            _objResponseModel.Header.Status = st.res.Success;
            _objResponseModel.Header.Message = st.res.BindData;
            _objResponseModel.Body.Data = students;
            return _objResponseModel;
        }

        //[HttpGet]
        //public ResponseModel Get2()
        //{
        //    //TOURIS_TM_MENU tm = new TOURIS_TM_MENU();
        //    //ApiResData res = new ApiResData();
        //    //List<TOURIS_TM_MENU> isGet = repo.GetDtMenu();

        //    ResponseModel _objResponseModel = new ResponseModel();
        //    List<Student> students = new List<Student>();
        //    students.Add(new Student
        //    {
        //        ID = 101,
        //        Name = "Seam",
        //        Email = "seam@gmail.com",
        //        Address = "Dhaka,Bangladesh"
        //    });
        //    students.Add(new Student
        //    {
        //        ID = 102,
        //        Name = "Mitila",
        //        Email = "mitila@gmail.com",
        //        Address = "Dhaka,Bangladesh"
        //    });
        //    students.Add(new Student
        //    {
        //        ID = 104,
        //        Name = "Popy",
        //        Email = "popy@yahoo.com",
        //        Address = "Dhaka,Bangladesh"
        //    });

        //    _objResponseModel.Data = students;
        //    _objResponseModel.Status = true;
        //    _objResponseModel.Message = "Data Received successfully";

        //    return _objResponseModel;
        //}

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
