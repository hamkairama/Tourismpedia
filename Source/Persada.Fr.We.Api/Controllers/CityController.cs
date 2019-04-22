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
    [RoutePrefix("api/City")]
    public class CityController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        ICity repo;
        public CityController()
        {
            repo = new CityRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_CITY>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_CITY> cities = repo.GridBind();
                List<TOURIS_TV_CITY> cityViews = new List<TOURIS_TV_CITY>();

                if (cities.Count > 0)
                {
                    foreach (var item in cities)
                    {
                        TOURIS_TV_CITY cityView = new TOURIS_TV_CITY();
                        cityView.ID = item.ID;
                        cityView.PROVINCE_ID = item.PROVINCE_ID;
                        cityView.PROVINCE_NAME = item.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                        cityView.COUNTRY_NAME = item.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                        cityView.CITY_CODE = item.CITY_CODE;
                        cityView.CITY_NAME = item.CITY_NAME;
                        cityView.CITY_DESCRIPTION = item.CITY_DESCRIPTION;
                        cityView.CREATED_BY = item.CREATED_BY;
                        cityView.CREATED_TIME = item.CREATED_TIME;
                        cityView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        cityView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        cityViews.Add(cityView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { cityViews }, null));
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
        [ResponseType(typeof(TOURIS_TV_CITY))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_CITY city = repo.Retrieve(id);
                TOURIS_TV_CITY cityView = new TOURIS_TV_CITY();

                if (city != null)
                {
                    cityView.ID = city.ID;
                    cityView.PROVINCE_ID = city.PROVINCE_ID;
                    cityView.PROVINCE_NAME = city.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                    cityView.COUNTRY_ID = city.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.ID;
                    cityView.COUNTRY_NAME = city.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                    cityView.CITY_CODE = city.CITY_CODE;
                    cityView.CITY_NAME = city.CITY_NAME;
                    cityView.CITY_DESCRIPTION = city.CITY_DESCRIPTION;
                    cityView.CREATED_BY = city.CREATED_BY;
                    cityView.CREATED_TIME = city.CREATED_TIME;
                    cityView.LAST_MODIFIED_BY = city.LAST_MODIFIED_BY;
                    cityView.LAST_MODIFIED_TIME = city.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { cityView }, null));

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
        public IHttpActionResult Add(TOURIS_TV_CITY cityView)
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

                TOURIS_TM_CITY city = new TOURIS_TM_CITY();
                city.PROVINCE_ID = cityView.PROVINCE_ID;
                city.CITY_CODE = cityView.CITY_CODE;
                city.CITY_NAME = cityView.CITY_NAME;
                city.CITY_DESCRIPTION = cityView.CITY_DESCRIPTION;
                city.CREATED_BY = cityView.CREATED_BY;
                city.CREATED_TIME = cityView.CREATED_TIME;
                city.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(city);
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
        public IHttpActionResult Edit(TOURIS_TV_CITY cityView)
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

                TOURIS_TM_CITY city = new TOURIS_TM_CITY();
                city.ID = cityView.ID;
                city.PROVINCE_ID = cityView.PROVINCE_ID;
                city.CITY_CODE = cityView.CITY_CODE;
                city.CITY_NAME = cityView.CITY_NAME;
                city.CITY_DESCRIPTION = cityView.CITY_DESCRIPTION;
                city.LAST_MODIFIED_TIME = cityView.LAST_MODIFIED_TIME;
                city.LAST_MODIFIED_BY = cityView.LAST_MODIFIED_BY;

                rs = repo.Edit(city);
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

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_CITY>))]
        [Route("GetCityByProvinceId")]
        public IHttpActionResult GetCityByProvinceId(int id)
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_CITY> cities = repo.GetCityByProvinceId(id);
                List<TOURIS_TV_CITY> cityViews = new List<TOURIS_TV_CITY>();

                if (cities.Count > 0)
                {
                    foreach (var item in cities)
                    {
                        TOURIS_TV_CITY cityView = new TOURIS_TV_CITY();
                        cityView.ID = item.ID;
                        cityView.PROVINCE_ID = item.PROVINCE_ID;
                        cityView.PROVINCE_NAME = item.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                        cityView.COUNTRY_NAME = item.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                        cityView.CITY_CODE = item.CITY_CODE;
                        cityView.CITY_NAME = item.CITY_NAME;
                        cityView.CITY_DESCRIPTION = item.CITY_DESCRIPTION;
                        cityView.CREATED_BY = item.CREATED_BY;
                        cityView.CREATED_TIME = item.CREATED_TIME;
                        cityView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        cityView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        cityViews.Add(cityView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { cityViews }, null));
                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { rs }, new Exception(eFunc.fg.DataNf)));
                return Content(HttpStatusCode.BadGateway, resObj);
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
