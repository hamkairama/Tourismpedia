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
    [RoutePrefix("api/SubCategoryPhoto")]
    public class SubCategoryPhotoController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        ISubCategoryPhoto repo;
        public SubCategoryPhotoController()
        {
            repo = new SubCategoryPhotoRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_SUB_CATEGORY_PHOTO>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_SUB_CATEGORY_PHOTO> subCategoryPhotoPhotos = repo.GridBind();
                List<TOURIS_TV_SUB_CATEGORY_PHOTO> subCategoryPhotoViews = new List<TOURIS_TV_SUB_CATEGORY_PHOTO>();

                if (subCategoryPhotoPhotos.Count > 0)
                {
                    foreach (var item in subCategoryPhotoPhotos)
                    {
                        TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoView = new TOURIS_TV_SUB_CATEGORY_PHOTO();
                        subCategoryPhotoView.ID = item.ID;
                        subCategoryPhotoView.SUB_CATEGORY_ID = item.SUB_CATEGORY_ID;
                        subCategoryPhotoView.SUB_CATEGORY_NAME = item.TOURIS_TM_SUB_CATEGORY.SUB_CATEGORY_NAME;
                        subCategoryPhotoView.PHOTO_NAME = item.PHOTO_NAME;
                        subCategoryPhotoView.PHOTO_DESCRIPTION = item.PHOTO_DESCRIPTION;
                        subCategoryPhotoView.PHOTO_PATH = item.PHOTO_PATH;                       
                        subCategoryPhotoView.CREATED_BY = item.CREATED_BY;
                        subCategoryPhotoView.CREATED_TIME = item.CREATED_TIME;
                        subCategoryPhotoView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        subCategoryPhotoView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        subCategoryPhotoViews.Add(subCategoryPhotoView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { subCategoryPhotoViews }, null));
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
        [ResponseType(typeof(TOURIS_TV_SUB_CATEGORY_PHOTO))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_SUB_CATEGORY_PHOTO subCategoryPhoto = repo.Retrieve(id);
                TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoView = new TOURIS_TV_SUB_CATEGORY_PHOTO();

                if (subCategoryPhoto != null)
                {
                    subCategoryPhotoView.ID = subCategoryPhoto.ID;
                    subCategoryPhotoView.SUB_CATEGORY_ID = subCategoryPhoto.SUB_CATEGORY_ID;
                    subCategoryPhotoView.SUB_CATEGORY_NAME = subCategoryPhoto.TOURIS_TM_SUB_CATEGORY.SUB_CATEGORY_NAME;
                    subCategoryPhotoView.PHOTO_NAME = subCategoryPhoto.PHOTO_NAME;
                    subCategoryPhotoView.PHOTO_DESCRIPTION = subCategoryPhoto.PHOTO_DESCRIPTION;
                    subCategoryPhotoView.PHOTO_PATH = subCategoryPhoto.PHOTO_PATH;
                    subCategoryPhotoView.CREATED_BY = subCategoryPhoto.CREATED_BY;
                    subCategoryPhotoView.CREATED_TIME = subCategoryPhoto.CREATED_TIME;
                    subCategoryPhotoView.LAST_MODIFIED_BY = subCategoryPhoto.LAST_MODIFIED_BY;
                    subCategoryPhotoView.LAST_MODIFIED_TIME = subCategoryPhoto.LAST_MODIFIED_TIME;
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { subCategoryPhotoView }, null));

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
        public IHttpActionResult Add(TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoView)
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

                TOURIS_TM_SUB_CATEGORY_PHOTO subCategoryPhoto = new TOURIS_TM_SUB_CATEGORY_PHOTO();
                subCategoryPhoto.SUB_CATEGORY_ID = subCategoryPhotoView.SUB_CATEGORY_ID;
                subCategoryPhoto.PHOTO_NAME = subCategoryPhotoView.PHOTO_NAME;
                subCategoryPhoto.PHOTO_DESCRIPTION = subCategoryPhotoView.PHOTO_DESCRIPTION;
                subCategoryPhoto.PHOTO_PATH = subCategoryPhotoView.PHOTO_PATH;
                subCategoryPhoto.CREATED_BY = subCategoryPhotoView.CREATED_BY;
                subCategoryPhoto.CREATED_TIME = subCategoryPhotoView.CREATED_TIME;
                subCategoryPhoto.ROW_STATUS = eStat.fg.IsActive;

                rs = repo.Add(subCategoryPhoto);
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
        public IHttpActionResult Edit(TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoView)
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

                TOURIS_TM_SUB_CATEGORY_PHOTO subCategoryPhoto = new TOURIS_TM_SUB_CATEGORY_PHOTO();
                subCategoryPhoto.ID = subCategoryPhotoView.ID;
                subCategoryPhoto.SUB_CATEGORY_ID = subCategoryPhotoView.SUB_CATEGORY_ID;
                subCategoryPhoto.PHOTO_NAME = subCategoryPhotoView.PHOTO_NAME;
                subCategoryPhoto.PHOTO_DESCRIPTION = subCategoryPhotoView.PHOTO_DESCRIPTION;
                subCategoryPhoto.PHOTO_PATH = subCategoryPhotoView.PHOTO_PATH;
                subCategoryPhoto.LAST_MODIFIED_TIME = subCategoryPhotoView.LAST_MODIFIED_TIME;
                subCategoryPhoto.LAST_MODIFIED_BY = subCategoryPhotoView.LAST_MODIFIED_BY;

                rs = repo.Edit(subCategoryPhoto);
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
        [ResponseType(typeof(List<TOURIS_TV_SUB_CATEGORY_PHOTO>))]
        [Route("GetSubCategoryPhotoBySubCategoryId")]
        public IHttpActionResult GetSubCategoryPhotoBySubCategoryId(int id)
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_SUB_CATEGORY_PHOTO> subCategoryPhotoPhotos = repo.GetSubCategoryPhotoBySubCategoryId(id);
                List<TOURIS_TV_SUB_CATEGORY_PHOTO> subCategoryPhotoViews = new List<TOURIS_TV_SUB_CATEGORY_PHOTO>();

                if (subCategoryPhotoPhotos.Count > 0)
                {
                    foreach (var item in subCategoryPhotoPhotos)
                    {
                        TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoView = new TOURIS_TV_SUB_CATEGORY_PHOTO();
                        subCategoryPhotoView.ID = item.ID;
                        subCategoryPhotoView.SUB_CATEGORY_ID = item.SUB_CATEGORY_ID;
                        subCategoryPhotoView.PHOTO_NAME = item.PHOTO_NAME;
                        subCategoryPhotoView.PHOTO_DESCRIPTION = item.PHOTO_DESCRIPTION;
                        subCategoryPhotoView.PHOTO_PATH = item.PHOTO_PATH;
                        subCategoryPhotoView.CREATED_BY = item.CREATED_BY;
                        subCategoryPhotoView.CREATED_TIME = item.CREATED_TIME;
                        subCategoryPhotoView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        subCategoryPhotoView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        subCategoryPhotoViews.Add(subCategoryPhotoView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { subCategoryPhotoViews }, null));
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
