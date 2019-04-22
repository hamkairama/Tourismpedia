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
    public class SosmedController : Controller
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
            List<TOURIS_TV_SOSMED> sosmedRes = new List<TOURIS_TV_SOSMED>();

            sosmedRes = JsonConvert.DeserializeObject<List<TOURIS_TV_SOSMED>>(ParsingObject.RequestData(null, "Sosmed", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            return View(sosmedRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetTypeSosmedList = Dropdown.GetTypeSosmedList();
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_SOSMED sosmedView)
        {
            try
            {
                sosmedView.CREATED_BY = CurrentUser.GetCurrentUserId();
                sosmedView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(sosmedView, "Sosmed", "Add", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_SOSMED sosmedView = new TOURIS_TV_SOSMED();
            TOURIS_TV_SOSMED sosmedRes = new TOURIS_TV_SOSMED();

            sosmedView.ID = id;
            ViewBag.GetTypeSosmedList = Dropdown.GetTypeSosmedList();

            sosmedRes = JsonConvert.DeserializeObject<TOURIS_TV_SOSMED>(ParsingObject.RequestData(id, "Sosmed", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(sosmedRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_SOSMED sosmedView)
        {
            try
            {
                sosmedView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                sosmedView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(sosmedView, "Sosmed", "Edit", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_SOSMED sosmedView = new TOURIS_TV_SOSMED();
            TOURIS_TV_SOSMED sosmedRes = new TOURIS_TV_SOSMED();

            sosmedView.ID = id;

            sosmedRes = JsonConvert.DeserializeObject<TOURIS_TV_SOSMED>(ParsingObject.RequestData(id, "Sosmed", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(sosmedRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "Sosmed", "Delete", EnumList.IHttpMethod.Put.ToString()));
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
            TOURIS_TV_SOSMED sosmedView = new TOURIS_TV_SOSMED();
            TOURIS_TV_SOSMED sosmedRes = new TOURIS_TV_SOSMED();

            sosmedView.ID = id;

            sosmedRes = JsonConvert.DeserializeObject<TOURIS_TV_SOSMED>(ParsingObject.RequestData(id, "Sosmed", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(sosmedRes);
        }

    }
}