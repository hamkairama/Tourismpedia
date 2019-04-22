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
using System.Net.Mail;
using System.Data;

namespace Persada.Fr.Web.Controllers
{
    public class ContactUsController : Controller
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
            List<TOURIS_TV_CONTACT_US> contactUsRes = new List<TOURIS_TV_CONTACT_US>();

            contactUsRes = JsonConvert.DeserializeObject<List<TOURIS_TV_CONTACT_US>>(ParsingObject.RequestData(null, "ContactUs", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            return View(contactUsRes);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_CONTACT_US contactUsView)
        {
            try
            {
                contactUsView.CREATED_BY = CurrentUser.GetCurrentUserId();
                contactUsView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(contactUsView, "ContactUs", "Add", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_CONTACT_US contactUsView = new TOURIS_TV_CONTACT_US();
            TOURIS_TV_CONTACT_US contactUsRes = new TOURIS_TV_CONTACT_US();

            contactUsView.ID = id;

            contactUsRes = JsonConvert.DeserializeObject<TOURIS_TV_CONTACT_US>(ParsingObject.RequestData(id, "ContactUs", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(contactUsRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_CONTACT_US contactUsView)
        {
            try
            {
                contactUsView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                contactUsView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(contactUsView, "ContactUs", "Edit", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_CONTACT_US contactUsView = new TOURIS_TV_CONTACT_US();
            TOURIS_TV_CONTACT_US contactUsRes = new TOURIS_TV_CONTACT_US();

            contactUsView.ID = id;

            contactUsRes = JsonConvert.DeserializeObject<TOURIS_TV_CONTACT_US>(ParsingObject.RequestData(id, "ContactUs", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(contactUsRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "ContactUs", "Delete", EnumList.IHttpMethod.Put.ToString()));
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
            TOURIS_TV_CONTACT_US contactUsView = new TOURIS_TV_CONTACT_US();
            TOURIS_TV_CONTACT_US contactUsRes = new TOURIS_TV_CONTACT_US();

            contactUsView.ID = id;

            contactUsRes = JsonConvert.DeserializeObject<TOURIS_TV_CONTACT_US>(ParsingObject.RequestData(id, "ContactUs", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(contactUsRes);
        }

    }
}