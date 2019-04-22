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
    [RoutePrefix("api/Sosmed")]
    public class SosmedController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        ISosmed repo;
        public SosmedController()
        {
            repo = new SosmedRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_SOSMED>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_SOSMED> sosmed = repo.GridBind();
                List<TOURIS_TV_SOSMED> sosmedViews = new List<TOURIS_TV_SOSMED>();

                if (sosmed.Count > 0)
                {
                    foreach (var item in sosmed)
                    {
                        TOURIS_TV_SOSMED sosmedView = new TOURIS_TV_SOSMED();
                        sosmedView.ID = item.ID;
                        sosmedView.TYPE = item.TYPE;
                        sosmedView.NAME = item.NAME;
                        sosmedView.DESCRIPTION = item.DESCRIPTION;
                        sosmedView.ICON = item.ICON;
                        sosmedView.URL = item.URL;
                        sosmedView.DATA_EMBED = item.DATA_EMBED;
                        sosmedView.HEIGHT = item.HEIGHT;
                        sosmedView.WIDTH = item.WIDTH;
                        sosmedView.DATA_WIDGET_ID = item.DATA_WIDGET_ID;
                        sosmedView.CREATED_BY = item.CREATED_BY;
                        sosmedView.CREATED_TIME = item.CREATED_TIME;
                        sosmedView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        sosmedView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        sosmedViews.Add(sosmedView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { sosmedViews }, null));
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
        [ResponseType(typeof(TOURIS_TV_SOSMED))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_SOSMED sosmed = repo.Retrieve(id);
                TOURIS_TV_SOSMED sosmedView = new TOURIS_TV_SOSMED();

                if (sosmed != null)
                {
                    sosmedView.ID = sosmed.ID;
                    sosmedView.TYPE = sosmed.TYPE;
                    sosmedView.NAME = sosmed.NAME;
                    sosmedView.DESCRIPTION = sosmed.DESCRIPTION;
                    sosmedView.ICON = sosmed.ICON;
                    sosmedView.URL = sosmed.URL;
                    sosmedView.DATA_EMBED = sosmed.DATA_EMBED;
                    sosmedView.HEIGHT = sosmed.HEIGHT;
                    sosmedView.WIDTH = sosmed.WIDTH;
                    sosmedView.DATA_WIDGET_ID = sosmed.DATA_WIDGET_ID;
                    sosmedView.CREATED_BY = sosmed.CREATED_BY;
                    sosmedView.CREATED_TIME = sosmed.CREATED_TIME;
                    sosmedView.LAST_MODIFIED_BY = sosmed.LAST_MODIFIED_BY;
                    sosmedView.LAST_MODIFIED_TIME = sosmed.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { sosmedView }, null));

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
        public IHttpActionResult Add(TOURIS_TV_SOSMED sosmedView)
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

                TOURIS_TM_SOSMED sosmed = new TOURIS_TM_SOSMED();
                sosmed.TYPE = sosmedView.TYPE;
                sosmed.NAME = sosmedView.NAME;
                sosmed.DESCRIPTION = sosmedView.DESCRIPTION;
                sosmed.ICON = sosmedView.ICON;
                sosmed.URL = sosmedView.URL;
                sosmed.DATA_EMBED = sosmedView.DATA_EMBED;
                sosmed.HEIGHT = sosmedView.HEIGHT;
                sosmed.WIDTH = sosmedView.WIDTH;
                sosmed.DATA_WIDGET_ID = sosmedView.DATA_WIDGET_ID;
                sosmed.CREATED_BY = sosmedView.CREATED_BY;
                sosmed.CREATED_TIME = sosmedView.CREATED_TIME;
                sosmed.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(sosmed);
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
        public IHttpActionResult Edit(TOURIS_TV_SOSMED sosmedView)
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

                TOURIS_TM_SOSMED sosmed = new TOURIS_TM_SOSMED();
                sosmed.ID = sosmedView.ID;
                sosmed.TYPE = sosmedView.TYPE;
                sosmed.NAME = sosmedView.NAME;
                sosmed.DESCRIPTION = sosmedView.DESCRIPTION;
                sosmed.ICON = sosmedView.ICON;
                sosmed.URL = sosmedView.URL;
                sosmed.DATA_EMBED = sosmedView.DATA_EMBED;
                sosmed.HEIGHT = sosmedView.HEIGHT;
                sosmed.WIDTH = sosmedView.WIDTH;
                sosmed.DATA_WIDGET_ID = sosmedView.DATA_WIDGET_ID;
                sosmed.LAST_MODIFIED_TIME = sosmedView.LAST_MODIFIED_TIME;
                sosmed.LAST_MODIFIED_BY = sosmedView.LAST_MODIFIED_BY;

                rs = repo.Edit(sosmed);
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
