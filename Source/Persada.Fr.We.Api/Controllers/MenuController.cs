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
    [RoutePrefix("api/Menu")]
    public class MenuController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        IMenu repo;
        public MenuController()
        {
            repo = new MenuRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_MENU>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_MENU> menus = repo.GridBind();
                List<TOURIS_TV_MENU> menuViews = new List<TOURIS_TV_MENU>();

                if (menus.Count > 0)
                {
                    foreach (var item in menus)
                    {
                        TOURIS_TV_MENU menuView = new TOURIS_TV_MENU();
                        menuView.ID = item.ID;
                        menuView.MENU_NAME = item.MENU_NAME;
                        menuView.MENU_DESCRIPTION = item.MENU_DESCRIPTION;
                        menuView.MENU_PARENT_ID = item.MENU_PARENT_ID;
                        menuView.CREATED_BY = item.CREATED_BY;
                        menuView.CREATED_TIME = item.CREATED_TIME;
                        menuView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        menuView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        menuViews.Add(menuView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { menuViews }, null));
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
        [ResponseType(typeof(TOURIS_TV_MENU))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_MENU menu = repo.Retrieve(id);
                TOURIS_TV_MENU menuView = new TOURIS_TV_MENU();

                if (menu != null)
                {
                    menuView.ID = menu.ID;
                    menuView.MENU_NAME = menu.MENU_NAME;
                    menuView.MENU_DESCRIPTION = menu.MENU_DESCRIPTION;
                    menuView.MENU_PARENT_ID = menu.MENU_PARENT_ID;
                    menuView.CREATED_BY = menu.CREATED_BY;
                    menuView.CREATED_TIME = menu.CREATED_TIME;
                    menuView.LAST_MODIFIED_BY = menu.LAST_MODIFIED_BY;
                    menuView.LAST_MODIFIED_TIME = menu.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { menuView }, null));

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
        public IHttpActionResult Add(TOURIS_TV_MENU menuView)
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

                TOURIS_TM_MENU menu = new TOURIS_TM_MENU();
                menu.MENU_NAME = menuView.MENU_NAME;
                menu.MENU_DESCRIPTION = menuView.MENU_DESCRIPTION;
                menu.MENU_PARENT_ID = menuView.MENU_PARENT_ID;
                menu.CREATED_BY = menuView.CREATED_BY;
                menu.CREATED_TIME = menuView.CREATED_TIME;
                menu.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(menu);
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
        public IHttpActionResult Edit(TOURIS_TV_MENU menuView)
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

                TOURIS_TM_MENU menu = new TOURIS_TM_MENU();
                menu.MENU_NAME = menuView.MENU_NAME;
                menu.MENU_DESCRIPTION = menuView.MENU_DESCRIPTION;
                menu.MENU_PARENT_ID = menuView.MENU_PARENT_ID;
                menu.LAST_MODIFIED_TIME = menuView.LAST_MODIFIED_TIME;
                menu.LAST_MODIFIED_BY = menuView.LAST_MODIFIED_BY;

                rs = repo.Edit(menu);
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
