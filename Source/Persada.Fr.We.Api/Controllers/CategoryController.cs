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
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        ICategory repo;
        public CategoryController()
        {
            repo = new CategoryRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_CATEGORY>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_CATEGORY> categories = repo.GridBind();
                List<TOURIS_TV_CATEGORY> categoryViews = new List<TOURIS_TV_CATEGORY>();

                if (categories.Count > 0)
                {
                    foreach (var item in categories)
                    {
                        TOURIS_TV_CATEGORY categoryView = new TOURIS_TV_CATEGORY();
                        categoryView.ID = item.ID;
                        categoryView.CATEGORY_CODE = item.CATEGORY_CODE;
                        categoryView.CATEGORY_NAME = item.CATEGORY_NAME;
                        categoryView.CATEGORY_DESCRIPTION = item.CATEGORY_DESCRIPTION;
                        categoryView.URL = item.URL;
                        categoryView.CLASS = item.CLASS;
                        categoryView.PHOTO_PATH = item.PHOTO_PATH;
                        categoryView.CREATED_BY = item.CREATED_BY;
                        categoryView.CREATED_TIME = item.CREATED_TIME;
                        categoryView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        categoryView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        categoryViews.Add(categoryView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { categoryViews }, null));
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
        [ResponseType(typeof(TOURIS_TV_CATEGORY))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_CATEGORY category = repo.Retrieve(id);
                TOURIS_TV_CATEGORY categoryView = new TOURIS_TV_CATEGORY();

                if (category != null)
                {
                    categoryView.ID = category.ID;
                    categoryView.CATEGORY_CODE = category.CATEGORY_CODE;
                    categoryView.CATEGORY_NAME = category.CATEGORY_NAME;
                    categoryView.CATEGORY_DESCRIPTION = category.CATEGORY_DESCRIPTION;
                    categoryView.URL = category.URL;
                    categoryView.CLASS = category.CLASS;
                    categoryView.PHOTO_PATH = category.PHOTO_PATH;
                    categoryView.CREATED_BY = category.CREATED_BY;
                    categoryView.CREATED_TIME = category.CREATED_TIME;
                    categoryView.LAST_MODIFIED_BY = category.LAST_MODIFIED_BY;
                    categoryView.LAST_MODIFIED_TIME = category.LAST_MODIFIED_TIME;

                    if (category.TOURIS_TM_SUB_CATEGORY.Count > 0)
                    {
                        foreach (var item in category.TOURIS_TM_SUB_CATEGORY)
                        {
                            TOURIS_TV_SUB_CATEGORY subCategory = new TOURIS_TV_SUB_CATEGORY();
                            subCategory.ID = item.ID;
                            subCategory.ADDRESS = item.ADDRESS;
                            subCategory.CATEGORY_ID = item.CATEGORY_ID;
                            subCategory.END_TIME = item.END_TIME;
                            subCategory.START_TIME = item.START_TIME;
                            subCategory.SUB_CATEGORY_DESCRIPTION = item.SUB_CATEGORY_DESCRIPTION;
                            subCategory.SUB_CATEGORY_NAME = item.SUB_CATEGORY_NAME;
                            subCategory.VILLAGE_ID = item.VILLAGE_ID;
                            subCategory.PHOTO_PATH = item.PHOTO_PATH;

                            categoryView.TOURIS_TV_SUB_CATEGORY.Add(subCategory);
                        }                        
                    }

                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { categoryView }, null));

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
        public IHttpActionResult Add(TOURIS_TV_CATEGORY categoryView)
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

                TOURIS_TM_CATEGORY category = new TOURIS_TM_CATEGORY();
                category.CATEGORY_CODE = categoryView.CATEGORY_CODE;
                category.CATEGORY_NAME = categoryView.CATEGORY_NAME;
                category.CATEGORY_DESCRIPTION = categoryView.CATEGORY_DESCRIPTION;
                category.URL = categoryView.URL;
                category.CLASS = categoryView.CLASS;
                category.PHOTO_PATH = categoryView.PHOTO_PATH;
                category.CREATED_BY = categoryView.CREATED_BY;
                category.CREATED_TIME = categoryView.CREATED_TIME;
                category.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(category);
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
        public IHttpActionResult Edit(TOURIS_TV_CATEGORY categoryView)
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

                TOURIS_TM_CATEGORY category = new TOURIS_TM_CATEGORY();
                category.ID = categoryView.ID;
                category.CATEGORY_CODE = categoryView.CATEGORY_CODE;
                category.CATEGORY_NAME = categoryView.CATEGORY_NAME;
                category.CATEGORY_DESCRIPTION = categoryView.CATEGORY_DESCRIPTION;
                category.URL = categoryView.URL;
                category.CLASS = categoryView.CLASS;
                category.PHOTO_PATH = categoryView.PHOTO_PATH;
                category.LAST_MODIFIED_TIME = categoryView.LAST_MODIFIED_TIME;
                category.LAST_MODIFIED_BY = categoryView.LAST_MODIFIED_BY;

                rs = repo.Edit(category);
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
