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
    public class ProvinceController : Controller
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
            List<TOURIS_TV_PROVINCE> provinceRes = new List<TOURIS_TV_PROVINCE>();

            provinceRes = JsonConvert.DeserializeObject<List<TOURIS_TV_PROVINCE>>(ParsingObject.RequestData(null, "Province", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            return View(provinceRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetCountryList = Dropdown.GetCountryList();
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_PROVINCE provinceView)
        {
            try
            {
                provinceView.CREATED_BY = CurrentUser.GetCurrentUserId();
                provinceView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(provinceView, "Province", "Add", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_PROVINCE provinceView = new TOURIS_TV_PROVINCE();
            TOURIS_TV_PROVINCE provinceRes = new TOURIS_TV_PROVINCE();

            provinceView.ID = id;
            provinceRes = JsonConvert.DeserializeObject<TOURIS_TV_PROVINCE>(ParsingObject.RequestData(id, "Province", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            ViewBag.GetCountryList = Dropdown.GetCountryList();

            return View(provinceRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_PROVINCE provinceView)
        {
            try
            {
                provinceView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                provinceView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(provinceView, "Province", "Edit", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_PROVINCE provinceView = new TOURIS_TV_PROVINCE();
            TOURIS_TV_PROVINCE provinceRes = new TOURIS_TV_PROVINCE();

            provinceView.ID = id;

            provinceRes = JsonConvert.DeserializeObject<TOURIS_TV_PROVINCE>(ParsingObject.RequestData(id, "Province", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(provinceRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "Province", "Delete", EnumList.IHttpMethod.Put.ToString()));
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
            TOURIS_TV_PROVINCE provinceView = new TOURIS_TV_PROVINCE();
            TOURIS_TV_PROVINCE provinceRes = new TOURIS_TV_PROVINCE();

            provinceView.ID = id;

            provinceRes = JsonConvert.DeserializeObject<TOURIS_TV_PROVINCE>(ParsingObject.RequestData(id, "Province", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(provinceRes);
        }

    }
}