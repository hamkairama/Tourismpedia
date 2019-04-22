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
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;

namespace Persada.Fr.We.Api.Controllers
{
    [RoutePrefix("api/Slideshow")]
    public class SlideshowController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        ISlideshow repo;
        public SlideshowController()
        {
            repo = new SlideshowRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_SLIDESHOW>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_SLIDESHOW> countries = repo.GridBind();
                List<TOURIS_TV_SLIDESHOW> slideshowViews = new List<TOURIS_TV_SLIDESHOW>();

                if (countries.Count > 0)
                {
                    foreach (var item in countries)
                    {
                        TOURIS_TV_SLIDESHOW slideshowView = new TOURIS_TV_SLIDESHOW();
                        slideshowView.ID = item.ID;
                        slideshowView.TITTLE = item.TITTLE;
                        slideshowView.CONTENT_DESCRIPTION = item.CONTENT_DESCRIPTION;
                        slideshowView.CLASS = item.CLASS;
                        slideshowView.PHOTO_PATH = item.PHOTO_PATH;
                        slideshowView.URL = item.URL;
                        slideshowView.CREATED_BY = item.CREATED_BY;
                        slideshowView.CREATED_TIME = item.CREATED_TIME;
                        slideshowView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        slideshowView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        slideshowViews.Add(slideshowView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { slideshowViews }, null));
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
        [ResponseType(typeof(TOURIS_TV_SLIDESHOW))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_SLIDESHOW slideshow = repo.Retrieve(id);
                TOURIS_TV_SLIDESHOW slideshowView = new TOURIS_TV_SLIDESHOW();

                if (slideshow != null)
                {
                    slideshowView.ID = slideshow.ID;
                    slideshowView.TITTLE = slideshow.TITTLE;
                    slideshowView.CONTENT_DESCRIPTION = slideshow.CONTENT_DESCRIPTION;
                    slideshowView.CLASS = slideshow.CLASS;
                    slideshowView.PHOTO_PATH = slideshow.PHOTO_PATH;
                    slideshowView.URL = slideshow.URL;
                    slideshowView.CREATED_BY = slideshow.CREATED_BY;
                    slideshowView.CREATED_TIME = slideshow.CREATED_TIME;
                    slideshowView.LAST_MODIFIED_BY = slideshow.LAST_MODIFIED_BY;
                    slideshowView.LAST_MODIFIED_TIME = slideshow.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { slideshowView }, null));

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
        public IHttpActionResult Add(TOURIS_TV_SLIDESHOW slideshowView)
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

                TOURIS_TM_SLIDESHOW slideshow = new TOURIS_TM_SLIDESHOW();
                slideshow.TITTLE = slideshowView.TITTLE;
                slideshow.CONTENT_DESCRIPTION = slideshowView.CONTENT_DESCRIPTION;
                slideshow.CLASS = slideshowView.CLASS;
                slideshow.PHOTO_PATH = slideshowView.PHOTO_PATH;
                slideshow.URL = slideshowView.URL;
                slideshow.CREATED_BY = slideshowView.CREATED_BY;
                slideshow.CREATED_TIME = slideshowView.CREATED_TIME;
                slideshow.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(slideshow);
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
        public IHttpActionResult Edit(TOURIS_TV_SLIDESHOW slideshowView)
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

                TOURIS_TM_SLIDESHOW slideshow = new TOURIS_TM_SLIDESHOW();
                slideshow.ID = slideshowView.ID;
                slideshow.TITTLE = slideshowView.TITTLE;
                slideshow.CONTENT_DESCRIPTION = slideshowView.CONTENT_DESCRIPTION;
                slideshow.CLASS = slideshowView.CLASS;
                slideshow.PHOTO_PATH = slideshowView.PHOTO_PATH;
                slideshow.URL = slideshowView.URL;
                slideshow.LAST_MODIFIED_TIME = slideshowView.LAST_MODIFIED_TIME;
                slideshow.LAST_MODIFIED_BY = slideshowView.LAST_MODIFIED_BY;

                rs = repo.Edit(slideshow);
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
