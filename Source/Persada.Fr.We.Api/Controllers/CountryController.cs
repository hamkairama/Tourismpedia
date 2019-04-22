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
    [RoutePrefix("api/Country")]
    public class CountryController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        ICountry repo;
        public CountryController()
        {
            repo = new CountryRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_COUNTRY>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_COUNTRY> countries = repo.GridBind();
                List<TOURIS_TV_COUNTRY> countryViews = new List<TOURIS_TV_COUNTRY>();

                if (countries.Count > 0)
                {
                    foreach (var item in countries)
                    {
                        TOURIS_TV_COUNTRY countryView = new TOURIS_TV_COUNTRY();
                        countryView.ID = item.ID;
                        countryView.COUNTRY_CODE = item.COUNTRY_CODE;
                        countryView.COUNTRY_NAME = item.COUNTRY_NAME;
                        countryView.COUNTRY_DESCRIPTION = item.COUNTRY_DESCRIPTION;
                        countryView.CREATED_BY = item.CREATED_BY;
                        countryView.CREATED_TIME = item.CREATED_TIME;
                        countryView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        countryView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        countryViews.Add(countryView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { countryViews }, null));
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
        [ResponseType(typeof(TOURIS_TV_COUNTRY))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_COUNTRY country = repo.Retrieve(id);
                TOURIS_TV_COUNTRY countryView = new TOURIS_TV_COUNTRY();

                if (country != null)
                {
                    countryView.ID = country.ID;
                    countryView.COUNTRY_CODE = country.COUNTRY_CODE;
                    countryView.COUNTRY_NAME = country.COUNTRY_NAME;
                    countryView.COUNTRY_DESCRIPTION = country.COUNTRY_DESCRIPTION;
                    countryView.CREATED_BY = country.CREATED_BY;
                    countryView.CREATED_TIME = country.CREATED_TIME;
                    countryView.LAST_MODIFIED_BY = country.LAST_MODIFIED_BY;
                    countryView.LAST_MODIFIED_TIME = country.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { countryView }, null));

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
        public IHttpActionResult Add(TOURIS_TV_COUNTRY countryView)
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

                TOURIS_TM_COUNTRY country = new TOURIS_TM_COUNTRY();
                country.COUNTRY_CODE = countryView.COUNTRY_CODE;
                country.COUNTRY_NAME = countryView.COUNTRY_NAME;
                country.COUNTRY_DESCRIPTION = countryView.COUNTRY_DESCRIPTION;
                country.CREATED_BY = countryView.CREATED_BY;
                country.CREATED_TIME = countryView.CREATED_TIME;
                country.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(country);
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
        public IHttpActionResult Edit(TOURIS_TV_COUNTRY countryView)
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

                TOURIS_TM_COUNTRY country = new TOURIS_TM_COUNTRY();
                country.ID = countryView.ID;
                country.COUNTRY_CODE = countryView.COUNTRY_CODE;
                country.COUNTRY_NAME = countryView.COUNTRY_NAME;
                country.COUNTRY_DESCRIPTION = countryView.COUNTRY_DESCRIPTION;
                country.LAST_MODIFIED_TIME = countryView.LAST_MODIFIED_TIME;
                country.LAST_MODIFIED_BY = countryView.LAST_MODIFIED_BY;

                rs = repo.Edit(country);
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
