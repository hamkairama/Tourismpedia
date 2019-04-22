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
    public class VillageController : Controller
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
            List<TOURIS_TV_VILLAGE> villageRes = new List<TOURIS_TV_VILLAGE>();

            villageRes = JsonConvert.DeserializeObject<List<TOURIS_TV_VILLAGE>>(ParsingObject.RequestData(null, "Village", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            return View(villageRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetCountryList = Dropdown.GetCountryList();
            ViewBag.GetNullList = Dropdown.GetNullList();
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_VILLAGE villageView)
        {
            try
            {
                villageView.CREATED_BY = CurrentUser.GetCurrentUserId();
                villageView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(villageView, "Village", "Add", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_VILLAGE villageView = new TOURIS_TV_VILLAGE();
            TOURIS_TV_VILLAGE villageRes = new TOURIS_TV_VILLAGE();

            villageView.ID = id;
            villageRes = JsonConvert.DeserializeObject<TOURIS_TV_VILLAGE>(ParsingObject.RequestData(id, "Village", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            ViewBag.GetCountryList = Dropdown.GetCountryList();
            ViewBag.GetProvinceList = Dropdown.GetProvinceListByCountryId(villageRes.COUNTRY_ID);
            ViewBag.GetCityList = Dropdown.GetCityListByProvinceId(villageRes.PROVINCE_ID);
            ViewBag.GetDistrictList = Dropdown.GetDistrictByCityId(villageRes.CITY_ID);

            return View(villageRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_VILLAGE villageView)
        {
            try
            {
                villageView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                villageView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(villageView, "Village", "Edit", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_VILLAGE villageView = new TOURIS_TV_VILLAGE();
            TOURIS_TV_VILLAGE villageRes = new TOURIS_TV_VILLAGE();

            villageView.ID = id;

            villageRes = JsonConvert.DeserializeObject<TOURIS_TV_VILLAGE>(ParsingObject.RequestData(id, "Village", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(villageRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "Village", "Delete", EnumList.IHttpMethod.Put.ToString()));
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
            TOURIS_TV_VILLAGE villageView = new TOURIS_TV_VILLAGE();
            TOURIS_TV_VILLAGE villageRes = new TOURIS_TV_VILLAGE();

            villageView.ID = id;

            villageRes = JsonConvert.DeserializeObject<TOURIS_TV_VILLAGE>(ParsingObject.RequestData(id, "Village", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(villageRes);
        }

        public ActionResult GetDistrictByCityId(int cityId)
        {
            SelectList selectList = Dropdown.GetDistrictByCityId(cityId);
            return Json(selectList);
        }

    }
}