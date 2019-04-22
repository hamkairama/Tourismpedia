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
    [RoutePrefix("api/District")]
    public class DistrictController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        IDistrict repo;
        public DistrictController()
        {
            repo = new DistrictRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_DISTRICT>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_DISTRICT> districts = repo.GridBind();
                List<TOURIS_TV_DISTRICT> districtViews = new List<TOURIS_TV_DISTRICT>();

                if (districts.Count > 0)
                {
                    foreach (var item  in districts)
                    {
                        TOURIS_TV_DISTRICT districtView = new TOURIS_TV_DISTRICT();
                        districtView.ID = item.ID;
                        districtView.CITY_ID = item.CITY_ID;
                        districtView.PROVINCE_NAME = item.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                        districtView.COUNTRY_NAME = item.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                        districtView.CITY_NAME = item.TOURIS_TM_CITY.CITY_NAME;
                        districtView.DISTRICT_CODE = item.DISTRICT_CODE;
                        districtView.DISTRICT_NAME = item.DISTRICT_NAME;
                        districtView.DISTRICT_DESCRIPTION = item.DISTRICT_DESCRIPTION;
                        districtView.CREATED_BY = item.CREATED_BY;
                        districtView.CREATED_TIME = item.CREATED_TIME;
                        districtView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        districtView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        districtViews.Add(districtView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { districtViews }, null));
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
        [ResponseType(typeof(TOURIS_TV_DISTRICT))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_DISTRICT district = repo.Retrieve(id);
                TOURIS_TV_DISTRICT districtView = new TOURIS_TV_DISTRICT();

                if (district != null)
                {
                    districtView.ID = district.ID;
                    districtView.COUNTRY_ID = district.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.ID;
                    districtView.COUNTRY_NAME = district.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                    districtView.PROVINCE_ID = district.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.ID;
                    districtView.PROVINCE_NAME = district.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                    districtView.CITY_ID = district.TOURIS_TM_CITY.ID;
                    districtView.CITY_NAME = district.TOURIS_TM_CITY.CITY_NAME;
                    districtView.DISTRICT_CODE = district.DISTRICT_CODE;
                    districtView.DISTRICT_NAME = district.DISTRICT_NAME;
                    districtView.DISTRICT_DESCRIPTION = district.DISTRICT_DESCRIPTION;
                    districtView.CREATED_BY = district.CREATED_BY;
                    districtView.CREATED_TIME = district.CREATED_TIME;
                    districtView.LAST_MODIFIED_BY = district.LAST_MODIFIED_BY;
                    districtView.LAST_MODIFIED_TIME = district.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { districtView }, null));

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
        public IHttpActionResult Add(TOURIS_TV_DISTRICT districtView)
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

                TOURIS_TM_DISTRICT district = new TOURIS_TM_DISTRICT();
                district.CITY_ID = districtView.CITY_ID;
                district.DISTRICT_CODE = districtView.DISTRICT_CODE;
                district.DISTRICT_NAME = districtView.DISTRICT_NAME;
                district.DISTRICT_DESCRIPTION = districtView.DISTRICT_DESCRIPTION;
                district.CREATED_BY = districtView.CREATED_BY;
                district.CREATED_TIME = districtView.CREATED_TIME;
                district.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(district);
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
        public IHttpActionResult Edit(TOURIS_TV_DISTRICT ProvinceView)
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

                TOURIS_TM_DISTRICT district = new TOURIS_TM_DISTRICT();
                district.ID = ProvinceView.ID;
                district.CITY_ID = ProvinceView.CITY_ID;
                district.DISTRICT_CODE = ProvinceView.DISTRICT_CODE;
                district.DISTRICT_NAME = ProvinceView.DISTRICT_NAME;
                district.DISTRICT_DESCRIPTION = ProvinceView.DISTRICT_DESCRIPTION;
                district.LAST_MODIFIED_TIME = ProvinceView.LAST_MODIFIED_TIME;
                district.LAST_MODIFIED_BY = ProvinceView.LAST_MODIFIED_BY;

                rs = repo.Edit(district);
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
        [ResponseType(typeof(List<TOURIS_TV_DISTRICT>))]
        [Route("GetDistrictByCityId")]
        public IHttpActionResult GetDistrictByCityId(int id)
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_DISTRICT> districts = repo.GetDistrictByCityId(id);
                List<TOURIS_TV_DISTRICT> districtViews = new List<TOURIS_TV_DISTRICT>();

                if (districts.Count > 0)
                {
                    foreach (var item in districts)
                    {
                        TOURIS_TV_DISTRICT districtView = new TOURIS_TV_DISTRICT();
                        districtView.ID = item.ID;
                        districtView.COUNTRY_ID = item.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.ID;
                        districtView.COUNTRY_NAME = item.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                        districtView.PROVINCE_ID = item.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.ID;
                        districtView.PROVINCE_NAME = item.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                        districtView.CITY_ID = item.TOURIS_TM_CITY.ID;
                        districtView.CITY_NAME = item.TOURIS_TM_CITY.CITY_NAME;
                        districtView.DISTRICT_NAME = item.DISTRICT_NAME;
                        districtView.DISTRICT_DESCRIPTION = item.DISTRICT_DESCRIPTION;
                        districtView.CREATED_BY = item.CREATED_BY;
                        districtView.CREATED_TIME = item.CREATED_TIME;
                        districtView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        districtView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        districtViews.Add(districtView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { districtViews }, null));
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
