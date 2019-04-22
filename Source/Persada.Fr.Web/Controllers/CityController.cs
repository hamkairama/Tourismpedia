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
    public class CityController : Controller
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
            List<TOURIS_TV_CITY> cityRes = new List<TOURIS_TV_CITY>();

            cityRes = JsonConvert.DeserializeObject<List<TOURIS_TV_CITY>>(ParsingObject.RequestData(null, "City", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            return View(cityRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetCountryList = Dropdown.GetCountryList();
            ViewBag.GetNullList = Dropdown.GetNullList();
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_CITY cityView)
        {
            try
            {
                cityView.CREATED_BY = CurrentUser.GetCurrentUserId();
                cityView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(cityView, "City", "Add", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_CITY cityView = new TOURIS_TV_CITY();
            TOURIS_TV_CITY cityRes = new TOURIS_TV_CITY();

            cityView.ID = id;
            cityRes = JsonConvert.DeserializeObject<TOURIS_TV_CITY>(ParsingObject.RequestData(id, "City", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            ViewBag.GetCountryList = Dropdown.GetCountryList();
            ViewBag.GetProvinceList = Dropdown.GetProvinceListByCountryId(cityRes.COUNTRY_ID);

            return View(cityRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_CITY cityView)
        {
            try
            {
                cityView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                cityView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(cityView, "City", "Edit", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_CITY cityView = new TOURIS_TV_CITY();
            TOURIS_TV_CITY cityRes = new TOURIS_TV_CITY();

            cityView.ID = id;

            cityRes = JsonConvert.DeserializeObject<TOURIS_TV_CITY>(ParsingObject.RequestData(id, "City", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(cityRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "City", "Delete", EnumList.IHttpMethod.Put.ToString()));
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
            TOURIS_TV_CITY cityView = new TOURIS_TV_CITY();
            TOURIS_TV_CITY cityRes = new TOURIS_TV_CITY();

            cityView.ID = id;

            cityRes = JsonConvert.DeserializeObject<TOURIS_TV_CITY>(ParsingObject.RequestData(id, "City", "GetProvinceByCountryId", EnumList.IHttpMethod.Put.ToString()));
            return View(cityRes);
        }

        public ActionResult GetProvinceListByCountryId(int countryId)
        {
            SelectList selectList = Dropdown.GetProvinceListByCountryId(countryId);
            return Json(selectList);
        }

    }
}