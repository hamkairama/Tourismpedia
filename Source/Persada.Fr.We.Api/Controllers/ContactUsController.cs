using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Facade.Repository;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using Persada.Fr.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Persada.Fr.We.Api.Controllers
{
    [RoutePrefix("api/ContactUs")]
    public class ContactUsController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        IContactUs repo;
        public ContactUsController()
        {
            repo = new ContactUsRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_CONTACT_US>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_CONTACT_US> contactUses = repo.GridBind();
                List<TOURIS_TV_CONTACT_US> contactUsViews = new List<TOURIS_TV_CONTACT_US>();

                if (contactUses.Count > 0)
                {
                    foreach (var item in contactUses)
                    {
                        TOURIS_TV_CONTACT_US contactUsView = new TOURIS_TV_CONTACT_US();
                        contactUsView.ID = item.ID;
                        contactUsView.NAME_SENDER = item.NAME_SENDER;
                        contactUsView.EMAIL_SENDER = item.EMAIL_SENDER;
                        contactUsView.PHONE_SENDER = item.PHONE_SENDER;
                        contactUsView.DESCRIPTION = item.DESCRIPTION;
                        contactUsView.CREATED_BY = item.CREATED_BY;
                        contactUsView.CREATED_TIME = item.CREATED_TIME;
                        contactUsView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        contactUsView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        contactUsViews.Add(contactUsView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { contactUsViews }, null));
                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { rs }, new Exception(eFunc.fg.DataNf)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }
        }

        [HttpGet]
        [ResponseType(typeof(TOURIS_TV_CONTACT_US))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_CONTACT_US contactUs = repo.Retrieve(id);
                TOURIS_TV_CONTACT_US contactUsView = new TOURIS_TV_CONTACT_US();

                if (contactUs != null)
                {
                    contactUsView.ID = contactUs.ID;
                    contactUsView.NAME_SENDER = contactUs.NAME_SENDER;
                    contactUsView.EMAIL_SENDER = contactUs.EMAIL_SENDER;
                    contactUsView.PHONE_SENDER = contactUs.PHONE_SENDER;
                    contactUsView.DESCRIPTION = contactUs.DESCRIPTION;
                    contactUsView.CREATED_BY = contactUs.CREATED_BY;
                    contactUsView.CREATED_TIME = contactUs.CREATED_TIME;
                    contactUsView.LAST_MODIFIED_BY = contactUs.LAST_MODIFIED_BY;
                    contactUsView.LAST_MODIFIED_TIME = contactUs.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { contactUsView }, null));

                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { rs }, new Exception(eFunc.fg.DataNf)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }
        }

        [HttpPost]
        [ResponseType(typeof(ResultStatus))]
        [Route("Add")]
        public IHttpActionResult Add(TOURIS_TV_CONTACT_US contactUsView)
        {
            ApiResData res = new ApiResData();
            try
            {
                if (!ModelState.IsValid)
                {
                    rs.SetErrorStatus(eFunc.fg.SFailed);
                    resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Save, new Exception(eFunc.fg.DataIsntValid)));
                    return Content(HttpStatusCode.NotFound, resObj);
                }

                TOURIS_TM_CONTACT_US contactUs = new TOURIS_TM_CONTACT_US();
                contactUs.NAME_SENDER = contactUsView.NAME_SENDER;
                contactUs.EMAIL_SENDER = contactUsView.EMAIL_SENDER;
                contactUs.PHONE_SENDER = contactUsView.PHONE_SENDER;
                contactUs.DESCRIPTION = contactUsView.DESCRIPTION;
                contactUs.CREATED_BY = contactUsView.CREATED_BY;
                contactUs.CREATED_TIME = contactUsView.CREATED_TIME;
                contactUs.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(contactUs);
                if (rs.IsSuccess)
                    rs.SetSuccessStatus();
                else
                    rs.SetErrorStatus(eFunc.fg.SFailed);

                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Save, null));

                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Save, new Exception(eFunc.fg.SFailed)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }
        }

        [HttpPost]
        [ResponseType(typeof(ResultStatus))]
        [Route("Edit")]
        public IHttpActionResult Edit(TOURIS_TV_CONTACT_US contactUsView)
        {
            ApiResData res = new ApiResData();
            try
            {
                if (!ModelState.IsValid)
                {
                    rs.SetErrorStatus(eFunc.fg.SFailed);
                    resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Edit, new Exception(eFunc.fg.DataIsntValid)));
                    return Content(HttpStatusCode.NotFound, resObj);
                }

                TOURIS_TM_CONTACT_US contactUs = new TOURIS_TM_CONTACT_US();
                contactUs.ID = contactUsView.ID;
                contactUs.NAME_SENDER = contactUsView.NAME_SENDER;
                contactUs.EMAIL_SENDER = contactUsView.EMAIL_SENDER;
                contactUs.PHONE_SENDER = contactUsView.PHONE_SENDER;
                contactUs.DESCRIPTION = contactUsView.DESCRIPTION;
                contactUs.LAST_MODIFIED_TIME = contactUsView.LAST_MODIFIED_TIME;
                contactUs.LAST_MODIFIED_BY = contactUsView.LAST_MODIFIED_BY;

                rs = repo.Edit(contactUs);
                if (rs.IsSuccess)
                    rs.SetSuccessStatus();
                else
                    rs.SetErrorStatus(eFunc.fg.SFailed);

                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Edit, null));
                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Edit, new Exception(eFunc.fg.EFailed)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }
        }

        [HttpGet]
        [ResponseType(typeof(ResultStatus))]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                if (!ModelState.IsValid)
                {
                    rs.SetErrorStatus(eFunc.fg.SFailed);
                    resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Delete, new Exception(eFunc.fg.DataNf)));
                    return Content(HttpStatusCode.NotFound, resObj);
                }

                rs = repo.Delete(id, "System", CurrentUser.GetCurrentDateTime());
                if (rs.IsSuccess)
                    rs.SetSuccessStatus();
                else
                    rs.SetErrorStatus(eFunc.fg.SFailed);

                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Delete, null));
                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Delete, new Exception(eFunc.fg.DFailed)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }

        }

        private IHttpActionResult GetErrorResult(ResultStatus result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.IsSuccess)
            {
                if (result.MessageText != null)
                {
                    ModelState.AddModelError("", result.MessageText);
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
