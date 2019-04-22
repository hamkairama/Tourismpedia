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
    public class DistrictController : Controller
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
            List<TOURIS_TV_DISTRICT> districtRes = new List<TOURIS_TV_DISTRICT>();

            districtRes = JsonConvert.DeserializeObject<List<TOURIS_TV_DISTRICT>>(ParsingObject.RequestData(null, "District", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            return View(districtRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetCountryList = Dropdown.GetCountryList();
            ViewBag.GetNullList = Dropdown.GetNullList();
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_DISTRICT districtView)
        {
            try
            {
                districtView.CREATED_BY = CurrentUser.GetCurrentUserId();
                districtView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(districtView, "District", "Add", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_DISTRICT districtView = new TOURIS_TV_DISTRICT();
            TOURIS_TV_DISTRICT districtRes = new TOURIS_TV_DISTRICT();

            districtView.ID = id;
            districtRes = JsonConvert.DeserializeObject<TOURIS_TV_DISTRICT>(ParsingObject.RequestData(id, "District", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            ViewBag.GetCountryList = Dropdown.GetCountryList();
            ViewBag.GetProvinceList = Dropdown.GetProvinceListByCountryId(districtRes.COUNTRY_ID);
            ViewBag.GetCityList = Dropdown.GetCityListByProvinceId(districtRes.PROVINCE_ID);

            return View(districtRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_DISTRICT districtView)
        {
            try
            {
                districtView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                districtView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(districtView, "District", "Edit", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_DISTRICT districtView = new TOURIS_TV_DISTRICT();
            TOURIS_TV_DISTRICT districtRes = new TOURIS_TV_DISTRICT();

            districtView.ID = id;

            districtRes = JsonConvert.DeserializeObject<TOURIS_TV_DISTRICT>(ParsingObject.RequestData(id, "District", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(districtRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "District", "Delete", EnumList.IHttpMethod.Put.ToString()));
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
            TOURIS_TV_DISTRICT districtView = new TOURIS_TV_DISTRICT();
            TOURIS_TV_DISTRICT districtRes = new TOURIS_TV_DISTRICT();

            districtView.ID = id;

            districtRes = JsonConvert.DeserializeObject<TOURIS_TV_DISTRICT>(ParsingObject.RequestData(id, "District", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(districtRes);
        }

        public ActionResult GetCityListByProvinceId(int provinceId)
        {
            SelectList selectList = Dropdown.GetCityListByProvinceId(provinceId);
            return Json(selectList);
        }

    }
}