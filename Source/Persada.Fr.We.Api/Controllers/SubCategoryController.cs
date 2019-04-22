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
    [RoutePrefix("api/SubCategory")]
    public class SubCategoryController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        ISubCategory repo;
        public SubCategoryController()
        {
            repo = new SubCategoryRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_SUB_CATEGORY>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_SUB_CATEGORY> subCategories = repo.GridBind();
                List<TOURIS_TV_SUB_CATEGORY> subCategoryViews = new List<TOURIS_TV_SUB_CATEGORY>();

                if (subCategories.Count > 0)
                {
                    foreach (var item in subCategories)
                    {
                        TOURIS_TV_SUB_CATEGORY subCategoryView = new TOURIS_TV_SUB_CATEGORY();
                        subCategoryView.ID = item.ID;
                        subCategoryView.CATEGORY_ID = item.CATEGORY_ID;
                        subCategoryView.CATEGORY_NAME = item.TOURIS_TM_CATEGORY.CATEGORY_NAME;
                        subCategoryView.COUNTRY_ID = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.ID;
                        subCategoryView.COUNTRY_NAME = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                        subCategoryView.PROVINCE_ID = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.ID;
                        subCategoryView.PROVINCE_NAME = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                        subCategoryView.CITY_ID = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.ID;
                        subCategoryView.CITY_NAME = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.CITY_NAME;
                        subCategoryView.VILLAGE_ID = item.VILLAGE_ID;
                        subCategoryView.VILLAGE_NAME = item.TOURIS_TM_VILLAGE.VILLAGE_NAME;
                        subCategoryView.SUB_CATEGORY_NAME = item.SUB_CATEGORY_NAME;
                        subCategoryView.SUB_CATEGORY_DESCRIPTION = item.SUB_CATEGORY_DESCRIPTION;
                        subCategoryView.ADDRESS = item.ADDRESS;
                        subCategoryView.START_TIME = item.START_TIME;
                        subCategoryView.END_TIME = item.END_TIME;
                        subCategoryView.PHOTO_PATH = item.PHOTO_PATH;
                        subCategoryView.LATITUDE = item.LATITUDE;
                        subCategoryView.LONGITUDE = item.LONGITUDE;
                        subCategoryView.CREATED_BY = item.CREATED_BY;
                        subCategoryView.CREATED_TIME = item.CREATED_TIME;
                        subCategoryView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        subCategoryView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        subCategoryViews.Add(subCategoryView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { subCategoryViews }, null));
                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { rs }, new Exception(eFunc.fg.DataNf)));
                return Content(HttpStatusCode.BadGateway, resObj);
            }
        }

        [HttpGet]
        [ResponseType(typeof(TOURIS_TV_SUB_CATEGORY))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_SUB_CATEGORY subCategory = repo.Retrieve(id);
                TOURIS_TV_SUB_CATEGORY subCategoryView = new TOURIS_TV_SUB_CATEGORY();

                if (subCategory != null)
                {
                    subCategoryView.ID = subCategory.ID;
                    subCategoryView.CATEGORY_ID = subCategory.CATEGORY_ID;
                    subCategoryView.CATEGORY_NAME = subCategory.TOURIS_TM_CATEGORY.CATEGORY_NAME;
                    subCategoryView.COUNTRY_ID = subCategory.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.ID;
                    subCategoryView.COUNTRY_NAME = subCategory.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                    subCategoryView.PROVINCE_ID = subCategory.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.ID;
                    subCategoryView.PROVINCE_NAME = subCategory.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                    subCategoryView.CITY_ID = subCategory.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.ID;
                    subCategoryView.CITY_NAME = subCategory.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.CITY_NAME;
                    subCategoryView.DISTRICT_ID = subCategory.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.ID;
                    subCategoryView.DISTRICT_NAME = subCategory.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.DISTRICT_NAME;
                    subCategoryView.VILLAGE_NAME = subCategory.TOURIS_TM_VILLAGE.VILLAGE_NAME;
                    subCategoryView.VILLAGE_ID = subCategory.VILLAGE_ID;
                    subCategoryView.SUB_CATEGORY_NAME = subCategory.SUB_CATEGORY_NAME;
                    subCategoryView.SUB_CATEGORY_DESCRIPTION = subCategory.SUB_CATEGORY_DESCRIPTION;
                    subCategoryView.ADDRESS = subCategory.ADDRESS;
                    subCategoryView.START_TIME = subCategory.START_TIME;
                    subCategoryView.END_TIME = subCategory.END_TIME;
                    subCategoryView.PHOTO_PATH = subCategory.PHOTO_PATH;
                    subCategoryView.LATITUDE = subCategory.LATITUDE;
                    subCategoryView.LONGITUDE = subCategory.LONGITUDE;
                    subCategoryView.CREATED_BY = subCategory.CREATED_BY;
                    subCategoryView.CREATED_TIME = subCategory.CREATED_TIME;
                    subCategoryView.LAST_MODIFIED_BY = subCategory.LAST_MODIFIED_BY;
                    subCategoryView.LAST_MODIFIED_TIME = subCategory.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { subCategoryView }, null));

                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { rs }, new Exception(eFunc.fg.DataNf)));
                return Content(HttpStatusCode.BadGateway, resObj);
            }
        }

        [HttpPost]
        [ResponseType(typeof(ResultStatus))]
        [Route("Add")]
        public IHttpActionResult Add(TOURIS_TV_SUB_CATEGORY subCategoryView)
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

                TOURIS_TM_SUB_CATEGORY subCategory = new TOURIS_TM_SUB_CATEGORY();
                subCategory.CATEGORY_ID = subCategoryView.CATEGORY_ID;
                subCategory.VILLAGE_ID = subCategoryView.VILLAGE_ID;
                subCategory.SUB_CATEGORY_NAME = subCategoryView.SUB_CATEGORY_NAME;
                subCategory.SUB_CATEGORY_DESCRIPTION = subCategoryView.SUB_CATEGORY_DESCRIPTION;
                subCategory.START_TIME = subCategoryView.START_TIME;
                subCategory.END_TIME = subCategoryView.END_TIME;
                subCategory.PHOTO_PATH = subCategoryView.PHOTO_PATH;
                subCategory.ADDRESS = subCategoryView.ADDRESS;
                subCategory.LATITUDE = subCategoryView.LATITUDE;
                subCategory.LONGITUDE = subCategoryView.LONGITUDE;
                subCategory.CREATED_BY = subCategoryView.CREATED_BY;
                subCategory.CREATED_TIME = subCategoryView.CREATED_TIME;
                subCategory.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(subCategory);
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
        public IHttpActionResult Edit(TOURIS_TV_SUB_CATEGORY subCategoryView)
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

                TOURIS_TM_SUB_CATEGORY subCategory = new TOURIS_TM_SUB_CATEGORY();
                subCategory.ID = subCategoryView.ID;
                subCategory.CATEGORY_ID = subCategoryView.CATEGORY_ID;
                subCategory.VILLAGE_ID = subCategoryView.VILLAGE_ID;
                subCategory.SUB_CATEGORY_NAME = subCategoryView.SUB_CATEGORY_NAME;
                subCategory.SUB_CATEGORY_DESCRIPTION = subCategoryView.SUB_CATEGORY_DESCRIPTION;
                subCategory.START_TIME = subCategoryView.START_TIME;
                subCategory.END_TIME = subCategoryView.END_TIME;
                subCategory.PHOTO_PATH = subCategoryView.PHOTO_PATH;
                subCategory.ADDRESS = subCategoryView.ADDRESS;
                subCategory.LATITUDE = subCategoryView.LATITUDE;
                subCategory.LONGITUDE = subCategoryView.LONGITUDE;
                subCategory.LAST_MODIFIED_TIME = subCategoryView.LAST_MODIFIED_TIME;
                subCategory.LAST_MODIFIED_BY = subCategoryView.LAST_MODIFIED_BY;

                rs = repo.Edit(subCategory);
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

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_SUB_CATEGORY>))]
        [Route("GetSubCategoryByCategoryId")]
        public IHttpActionResult GetSubCategoryByCategoryId(int id)
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_SUB_CATEGORY> subCategories = repo.GetSubCategoryByCategoryId(id);
                List<TOURIS_TV_SUB_CATEGORY> subCategoryViews = new List<TOURIS_TV_SUB_CATEGORY>();

                if (subCategories.Count > 0)
                {
                    foreach (var item in subCategories)
                    {
                        TOURIS_TV_SUB_CATEGORY subCategoryView = new TOURIS_TV_SUB_CATEGORY();
                        subCategoryView.ID = item.ID;
                        subCategoryView.CATEGORY_ID = item.CATEGORY_ID;
                        subCategoryView.CATEGORY_NAME = item.TOURIS_TM_CATEGORY.CATEGORY_NAME;
                        subCategoryView.COUNTRY_ID = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.ID;
                        subCategoryView.COUNTRY_NAME = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                        subCategoryView.PROVINCE_ID = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.ID;
                        subCategoryView.PROVINCE_NAME = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                        subCategoryView.CITY_ID = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.ID;
                        subCategoryView.CITY_NAME = item.TOURIS_TM_VILLAGE.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.CITY_NAME;
                        subCategoryView.VILLAGE_ID = item.VILLAGE_ID;
                        subCategoryView.VILLAGE_NAME = item.TOURIS_TM_VILLAGE.VILLAGE_NAME;
                        subCategoryView.SUB_CATEGORY_NAME = item.SUB_CATEGORY_NAME;
                        subCategoryView.SUB_CATEGORY_DESCRIPTION = item.SUB_CATEGORY_DESCRIPTION;
                        subCategoryView.START_TIME = item.START_TIME;
                        subCategoryView.END_TIME = item.END_TIME;
                        subCategoryView.PHOTO_PATH = item.PHOTO_PATH;
                        subCategoryView.ADDRESS = item.ADDRESS;
                        subCategoryView.LATITUDE = item.LATITUDE;
                        subCategoryView.LONGITUDE = item.LONGITUDE;
                        subCategoryView.CREATED_BY = item.CREATED_BY;
                        subCategoryView.CREATED_TIME = item.CREATED_TIME;
                        subCategoryView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        subCategoryView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        subCategoryViews.Add(subCategoryView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { subCategoryViews }, null));
                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { rs }, new Exception(eFunc.fg.DataNf)));
                return Content(HttpStatusCode.BadGateway, resObj);
            }
        }
    }
}
