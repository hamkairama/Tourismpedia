using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Persada.Fr.ModelView;
using System.Collections.Generic;
using Newtonsoft.Json;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade;

namespace Persada.Fr.Web.Controllers
{
    public class SubCategoryPhotoPhotoController : Controller
    {
        ResultStatus rs = new ResultStatus();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Index(int id)
        {
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            List<TOURIS_TV_SUB_CATEGORY_PHOTO> subCategoryPhotoList = new List<TOURIS_TV_SUB_CATEGORY_PHOTO>();

            subCategoryPhotoList = GetSubCategoryPhotoBySubCategoryId(id);
            return View(subCategoryPhotoList);
        }

        public ActionResult Create()
        {
            ViewBag.GetCategoryList = Dropdown.GetCategoryList();
            ViewBag.GetCountryList = Dropdown.GetCountryList();
            ViewBag.GetNullList = Dropdown.GetNullList();
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoView)
        {
            try
            {
                subCategoryPhotoView.CREATED_BY = CurrentUser.GetCurrentUserId();
                subCategoryPhotoView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(subCategoryPhotoView, "SubCategoryPhoto", "Add", EnumList.IHttpMethod.Post.ToString()));
                if (rs.IsSuccess)
                {
                    rs.SetSuccessStatus("Data has been created successfully");
                    TempData["msgSuccess"] = rs.MessageText;
                }
                else
                {
                    rs.SetErrorStatus("Data failed to created");
                    TempData["msgError"] = rs.MessageText;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                rs.SetErrorStatus("Data failed to created");
                TempData["msgError"] = rs.MessageText;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoView = new TOURIS_TV_SUB_CATEGORY_PHOTO();
            TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoRes = new TOURIS_TV_SUB_CATEGORY_PHOTO();

            subCategoryPhotoView.ID = id;
            subCategoryPhotoRes = JsonConvert.DeserializeObject<TOURIS_TV_SUB_CATEGORY_PHOTO>(ParsingObject.RequestData(id, "SubCategoryPhoto", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));

            return View(subCategoryPhotoRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoView)
        {
            try
            {
                subCategoryPhotoView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                subCategoryPhotoView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(subCategoryPhotoView, "SubCategoryPhoto", "Edit", EnumList.IHttpMethod.Post.ToString()));
                if (rs.IsSuccess)
                {
                    rs.SetSuccessStatus("Data has been edited successfully");
                    TempData["msgSuccess"] = rs.MessageText;
                }
                else
                {
                    rs.SetErrorStatus("Data failed to edited");
                    TempData["msgError"] = rs.MessageText;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                rs.SetErrorStatus("Data failed to edited");
                TempData["msgError"] = rs.MessageText;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoView = new TOURIS_TV_SUB_CATEGORY_PHOTO();
            TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoRes = new TOURIS_TV_SUB_CATEGORY_PHOTO();

            subCategoryPhotoView.ID = id;

            subCategoryPhotoRes = JsonConvert.DeserializeObject<TOURIS_TV_SUB_CATEGORY_PHOTO>(ParsingObject.RequestData(id, "SubCategoryPhoto", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(subCategoryPhotoRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "SubCategoryPhoto", "Delete", EnumList.IHttpMethod.Put.ToString()));
                if (rs.IsSuccess)
                {
                    rs.SetSuccessStatus("Data has been deleted successfully");
                    TempData["msgSuccess"] = rs.MessageText;
                }
                else
                {
                    rs.SetErrorStatus("Data failed to deleted");
                    TempData["msgError"] = rs.MessageText;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                rs.SetErrorStatus("Data failed to deleted");
                TempData["msgError"] = rs.MessageText;
            }

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoView = new TOURIS_TV_SUB_CATEGORY_PHOTO();
            TOURIS_TV_SUB_CATEGORY_PHOTO subCategoryPhotoRes = new TOURIS_TV_SUB_CATEGORY_PHOTO();

            subCategoryPhotoView.ID = id;

            subCategoryPhotoRes = JsonConvert.DeserializeObject<TOURIS_TV_SUB_CATEGORY_PHOTO>(ParsingObject.RequestData(id, "SubCategoryPhoto", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(subCategoryPhotoRes);
        }


        private List<TOURIS_TV_SUB_CATEGORY_PHOTO> GetSubCategoryPhotoBySubCategoryId(int id)
        {
            List<TOURIS_TV_SUB_CATEGORY_PHOTO> subCategoryPhotoRes = new List<TOURIS_TV_SUB_CATEGORY_PHOTO>();

            subCategoryPhotoRes = JsonConvert.DeserializeObject<List<TOURIS_TV_SUB_CATEGORY_PHOTO>>(ParsingObject.RequestData(id, "SubCategoryPhoto", "GetSubCategoryPhotoBySubCategoryId", EnumList.IHttpMethod.Put.ToString()));
            return subCategoryPhotoRes;
        }

    }
}