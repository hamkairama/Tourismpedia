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
    public class SubCategoryController : Controller
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

        public ActionResult Index()
        {
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            List<TOURIS_TV_SUB_CATEGORY> subCategoryRes = new List<TOURIS_TV_SUB_CATEGORY>();

            subCategoryRes = JsonConvert.DeserializeObject<List<TOURIS_TV_SUB_CATEGORY>>(ParsingObject.RequestData(null, "SubCategory", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            return View(subCategoryRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetCategoryList = Dropdown.GetCategoryList();
            ViewBag.GetCountryList = Dropdown.GetCountryList();
            ViewBag.GetNullList = Dropdown.GetNullList();
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_SUB_CATEGORY subCategoryView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    subCategoryView.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                }

                subCategoryView.CREATED_BY = CurrentUser.GetCurrentUserId();
                subCategoryView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(subCategoryView, "SubCategory", "Add", EnumList.IHttpMethod.Post.ToString()));
                if (rs.IsSuccess)
                {
                    if (physicalPath != "")
                    {
                        postedFile.SaveAs(physicalPath);
                    }
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
            TOURIS_TV_SUB_CATEGORY subCategoryView = new TOURIS_TV_SUB_CATEGORY();
            TOURIS_TV_SUB_CATEGORY subCategoryRes = new TOURIS_TV_SUB_CATEGORY();

            subCategoryView.ID = id;
            subCategoryRes = JsonConvert.DeserializeObject<TOURIS_TV_SUB_CATEGORY>(ParsingObject.RequestData(id, "SubCategory", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            ViewBag.GetCategoryList = Dropdown.GetCategoryList();
            ViewBag.GetCountryList = Dropdown.GetCountryList();
            ViewBag.GetProvinceList = Dropdown.GetProvinceListByCountryId(subCategoryRes.COUNTRY_ID);
            ViewBag.GetCityList = Dropdown.GetCityListByProvinceId(subCategoryRes.PROVINCE_ID);
            ViewBag.GetDistrictList = Dropdown.GetDistrictByCityId(subCategoryRes.CITY_ID);
            ViewBag.GetVillageList = Dropdown.GetVillageByDistrictId(subCategoryRes.DISTRICT_ID);

            return View(subCategoryRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_SUB_CATEGORY subCategoryView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    subCategoryView.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                }

                subCategoryView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                subCategoryView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(subCategoryView, "SubCategory", "Edit", EnumList.IHttpMethod.Post.ToString()));
                if (rs.IsSuccess)
                {
                    if (physicalPath != "")
                    {
                        postedFile.SaveAs(physicalPath);
                    }
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
            TOURIS_TV_SUB_CATEGORY subCategoryView = new TOURIS_TV_SUB_CATEGORY();
            TOURIS_TV_SUB_CATEGORY subCategoryRes = new TOURIS_TV_SUB_CATEGORY();

            subCategoryView.ID = id;

            subCategoryRes = JsonConvert.DeserializeObject<TOURIS_TV_SUB_CATEGORY>(ParsingObject.RequestData(id, "SubCategory", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(subCategoryRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "SubCategory", "Delete", EnumList.IHttpMethod.Put.ToString()));
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
            TOURIS_TV_SUB_CATEGORY subCategoryView = new TOURIS_TV_SUB_CATEGORY();
            TOURIS_TV_SUB_CATEGORY subCategoryRes = new TOURIS_TV_SUB_CATEGORY();

            subCategoryView.ID = id;

            subCategoryRes = JsonConvert.DeserializeObject<TOURIS_TV_SUB_CATEGORY>(ParsingObject.RequestData(id, "SubCategory", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            string mapUrl = subCategoryRes.ADDRESS + ". " + subCategoryRes.VILLAGE_NAME + ", " + subCategoryRes.DISTRICT_NAME + ". " + subCategoryRes.CITY_NAME + ", " + subCategoryRes.PROVINCE_NAME + ". " + subCategoryRes.COUNTRY_NAME;
            ViewBag.MapUrl = mapUrl.Replace(" ", "+");
            return PartialView("Detail", subCategoryRes);
        }

        public ActionResult GetVillageByDistrictId(int districtId)
        {
            SelectList selectList = Dropdown.GetVillageByDistrictId(districtId);
            return Json(selectList);
        }

    }
}