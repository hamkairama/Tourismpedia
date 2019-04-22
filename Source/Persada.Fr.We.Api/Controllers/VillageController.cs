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
    [RoutePrefix("api/Village")]
    public class VillageController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        IVillage repo;
        public VillageController()
        {
            repo = new VillageRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_VILLAGE>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_VILLAGE> villages = repo.GridBind();
                List<TOURIS_TV_VILLAGE> villageViews = new List<TOURIS_TV_VILLAGE>();

                if (villages.Count > 0)
                {
                    foreach (var item in villages)
                    {
                        TOURIS_TV_VILLAGE villageView = new TOURIS_TV_VILLAGE();
                        villageView.ID = item.ID;
                        villageView.DISTRICT_ID = item.DISTRICT_ID;
                        villageView.PROVINCE_NAME = item.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                        villageView.COUNTRY_NAME = item.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                        villageView.CITY_NAME = item.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.CITY_NAME;
                        villageView.DISTRICT_NAME = item.TOURIS_TM_DISTRICT.DISTRICT_NAME;
                        villageView.VILLAGE_CODE = item.VILLAGE_CODE;
                        villageView.VILLAGE_NAME = item.VILLAGE_NAME;
                        villageView.VILLAGE_DESCRIPTION = item.VILLAGE_DESCRIPTION;
                        villageView.CREATED_BY = item.CREATED_BY;
                        villageView.CREATED_TIME = item.CREATED_TIME;
                        villageView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        villageView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        villageViews.Add(villageView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { villageViews }, null));
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
        [ResponseType(typeof(TOURIS_TV_VILLAGE))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_VILLAGE village = repo.Retrieve(id);
                TOURIS_TV_VILLAGE villageView = new TOURIS_TV_VILLAGE();

                if (village != null)
                {
                    villageView.ID = village.ID;
                    villageView.COUNTRY_ID = village.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.ID;
                    villageView.COUNTRY_NAME = village.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                    villageView.PROVINCE_ID = village.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.ID;
                    villageView.PROVINCE_NAME = village.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                    villageView.CITY_ID = village.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.ID;
                    villageView.CITY_NAME = village.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.CITY_NAME;
                    villageView.DISTRICT_ID = village.DISTRICT_ID;
                    villageView.DISTRICT_NAME = village.TOURIS_TM_DISTRICT.DISTRICT_NAME;
                    villageView.VILLAGE_CODE = village.VILLAGE_CODE;
                    villageView.VILLAGE_NAME = village.VILLAGE_NAME;
                    villageView.VILLAGE_DESCRIPTION = village.VILLAGE_DESCRIPTION;
                    villageView.CREATED_BY = village.CREATED_BY;
                    villageView.CREATED_TIME = village.CREATED_TIME;
                    villageView.LAST_MODIFIED_BY = village.LAST_MODIFIED_BY;
                    villageView.LAST_MODIFIED_TIME = village.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { villageView }, null));

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
        public IHttpActionResult Add(TOURIS_TV_VILLAGE villageView)
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

                TOURIS_TM_VILLAGE village = new TOURIS_TM_VILLAGE();
                village.DISTRICT_ID = villageView.DISTRICT_ID;
                village.VILLAGE_CODE = villageView.VILLAGE_CODE;
                village.VILLAGE_NAME = villageView.VILLAGE_NAME;
                village.VILLAGE_DESCRIPTION = villageView.VILLAGE_DESCRIPTION;
                village.CREATED_BY = villageView.CREATED_BY;
                village.CREATED_TIME = villageView.CREATED_TIME;
                village.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(village);
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
        public IHttpActionResult Edit(TOURIS_TV_VILLAGE ProvinceView)
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

                TOURIS_TM_VILLAGE village = new TOURIS_TM_VILLAGE();
                village.ID = ProvinceView.ID;
                village.DISTRICT_ID = ProvinceView.DISTRICT_ID;
                village.VILLAGE_CODE = ProvinceView.VILLAGE_CODE;
                village.VILLAGE_NAME = ProvinceView.VILLAGE_NAME;
                village.VILLAGE_DESCRIPTION = ProvinceView.VILLAGE_DESCRIPTION;
                village.LAST_MODIFIED_TIME = ProvinceView.LAST_MODIFIED_TIME;
                village.LAST_MODIFIED_BY = ProvinceView.LAST_MODIFIED_BY;

                rs = repo.Edit(village);
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
        [ResponseType(typeof(List<TOURIS_TV_VILLAGE>))]
        [Route("GetVillageByDistrictId")]
        public IHttpActionResult GetVillageByDistrictId(int id)
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_VILLAGE> villages = repo.GetVillageByDistrictId(id);
                List<TOURIS_TV_VILLAGE> vilageViews = new List<TOURIS_TV_VILLAGE>();

                if (villages.Count > 0)
                {
                    foreach (var item in villages)
                    {
                        TOURIS_TV_VILLAGE villageView = new TOURIS_TV_VILLAGE();
                        villageView.ID = item.ID;
                        villageView.COUNTRY_ID = item.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.ID;
                        villageView.COUNTRY_NAME = item.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.TOURIS_TM_COUNTRY.COUNTRY_NAME;
                        villageView.PROVINCE_ID = item.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.ID;
                        villageView.PROVINCE_NAME = item.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.TOURIS_TM_PROVINCE.PROVINCE_NAME;
                        villageView.CITY_ID = item.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.ID;
                        villageView.CITY_NAME = item.TOURIS_TM_DISTRICT.TOURIS_TM_CITY.CITY_NAME;
                        villageView.DISTRICT_ID = item.TOURIS_TM_DISTRICT.ID;
                        villageView.DISTRICT_NAME = item.TOURIS_TM_DISTRICT.DISTRICT_NAME;
                        villageView.VILLAGE_NAME = item.VILLAGE_NAME;
                        villageView.VILLAGE_DESCRIPTION = item.VILLAGE_DESCRIPTION;
                        villageView.CREATED_BY = item.CREATED_BY;
                        villageView.CREATED_TIME = item.CREATED_TIME;
                        villageView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        villageView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        vilageViews.Add(villageView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { vilageViews }, null));
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
