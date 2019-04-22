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
    [RoutePrefix("api/Province")]
    public class ProvinceController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        IProvince repo;
        public ProvinceController()
        {
            repo = new ProvinceRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_PROVINCE>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_PROVINCE> countries = repo.GridBind();
                List<TOURIS_TV_PROVINCE> provinceViews = new List<TOURIS_TV_PROVINCE>();

                if (countries.Count > 0)
                {
                    foreach (var item in countries)
                    {
                        TOURIS_TV_PROVINCE provinceView = new TOURIS_TV_PROVINCE();
                        provinceView.ID = item.ID;
                        provinceView.COUNTRY_ID = item.COUNTRY_ID;
                        provinceView.COUNTRY_NAME = item.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                        provinceView.PROVINCE_CODE = item.PROVINCE_CODE;
                        provinceView.PROVINCE_NAME = item.PROVINCE_NAME;
                        provinceView.PROVINCE_DESCRIPTION = item.PROVINCE_DESCRIPTION;
                        provinceView.CREATED_BY = item.CREATED_BY;
                        provinceView.CREATED_TIME = item.CREATED_TIME;
                        provinceView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        provinceView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        provinceViews.Add(provinceView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { provinceViews }, null));
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
        [ResponseType(typeof(TOURIS_TV_PROVINCE))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_PROVINCE province = repo.Retrieve(id);
                TOURIS_TV_PROVINCE provinceView = new TOURIS_TV_PROVINCE();

                if (province != null)
                {
                    provinceView.ID = province.ID;
                    provinceView.COUNTRY_ID = province.COUNTRY_ID;
                    provinceView.COUNTRY_NAME = province.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                    provinceView.PROVINCE_CODE = province.PROVINCE_CODE;
                    provinceView.PROVINCE_NAME = province.PROVINCE_NAME;
                    provinceView.PROVINCE_DESCRIPTION = province.PROVINCE_DESCRIPTION;
                    provinceView.CREATED_BY = province.CREATED_BY;
                    provinceView.CREATED_TIME = province.CREATED_TIME;
                    provinceView.LAST_MODIFIED_BY = province.LAST_MODIFIED_BY;
                    provinceView.LAST_MODIFIED_TIME = province.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { provinceView }, null));

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
        public IHttpActionResult Add(TOURIS_TV_PROVINCE provinceView)
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

                TOURIS_TM_PROVINCE province = new TOURIS_TM_PROVINCE();
                province.COUNTRY_ID = provinceView.COUNTRY_ID;
                province.PROVINCE_CODE = provinceView.PROVINCE_CODE;
                province.PROVINCE_NAME = provinceView.PROVINCE_NAME;
                province.PROVINCE_DESCRIPTION = provinceView.PROVINCE_DESCRIPTION;
                province.CREATED_BY = provinceView.CREATED_BY;
                province.CREATED_TIME = provinceView.CREATED_TIME;
                province.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(province);
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
        public IHttpActionResult Edit(TOURIS_TV_PROVINCE provinceView)
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

                TOURIS_TM_PROVINCE province = new TOURIS_TM_PROVINCE();
                province.ID = provinceView.ID;
                province.COUNTRY_ID = provinceView.COUNTRY_ID;
                province.PROVINCE_CODE = provinceView.PROVINCE_CODE;
                province.PROVINCE_NAME = provinceView.PROVINCE_NAME;
                province.PROVINCE_DESCRIPTION = provinceView.PROVINCE_DESCRIPTION;
                province.LAST_MODIFIED_TIME = provinceView.LAST_MODIFIED_TIME;
                province.LAST_MODIFIED_BY = provinceView.LAST_MODIFIED_BY;

                rs = repo.Edit(province);
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
        [ResponseType(typeof(List<TOURIS_TV_PROVINCE>))]
        [Route("GetProvinceByCountryId")]
        public IHttpActionResult GetProvinceByCountryId(int id)
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_PROVINCE> countries = repo.GetProvinceByCountryId(id);
                List<TOURIS_TV_PROVINCE> provinceViews = new List<TOURIS_TV_PROVINCE>();

                if (countries.Count > 0)
                {
                    foreach (var item in countries)
                    {
                        TOURIS_TV_PROVINCE provinceView = new TOURIS_TV_PROVINCE();
                        provinceView.ID = item.ID;
                        provinceView.COUNTRY_ID = item.COUNTRY_ID;
                        provinceView.COUNTRY_NAME = item.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                        provinceView.PROVINCE_CODE = item.PROVINCE_CODE;
                        provinceView.PROVINCE_NAME = item.PROVINCE_NAME;
                        provinceView.PROVINCE_DESCRIPTION = item.PROVINCE_DESCRIPTION;
                        provinceView.CREATED_BY = item.CREATED_BY;
                        provinceView.CREATED_TIME = item.CREATED_TIME;
                        provinceView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        provinceView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        provinceViews.Add(provinceView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { provinceViews }, null));
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
