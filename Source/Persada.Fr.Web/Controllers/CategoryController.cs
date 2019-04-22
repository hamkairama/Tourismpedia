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
    public class CategoryController : Controller
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
            List<TOURIS_TV_CATEGORY> categoryRes = new List<TOURIS_TV_CATEGORY>();

            categoryRes = JsonConvert.DeserializeObject<List<TOURIS_TV_CATEGORY>>(ParsingObject.RequestData(null, "Category", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            return View(categoryRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetClassButtonList = Dropdown.GetClassButtonList();
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_CATEGORY categoryView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    categoryView.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                }

                categoryView.CREATED_BY = CurrentUser.GetCurrentUserId();
                categoryView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(categoryView, "Category", "Add", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_CATEGORY categoryView = new TOURIS_TV_CATEGORY();
            TOURIS_TV_CATEGORY categoryRes = new TOURIS_TV_CATEGORY();
            ViewBag.GetClassButtonList = Dropdown.GetClassButtonList();

            categoryView.ID = id;

            categoryRes = JsonConvert.DeserializeObject<TOURIS_TV_CATEGORY>(ParsingObject.RequestData(id, "Category", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(categoryRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_CATEGORY categoryView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    categoryView.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                }

                categoryView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                categoryView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(categoryView, "Category", "Edit", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_CATEGORY categoryView = new TOURIS_TV_CATEGORY();
            TOURIS_TV_CATEGORY categoryRes = new TOURIS_TV_CATEGORY();

            categoryView.ID = id;

            categoryRes = JsonConvert.DeserializeObject<TOURIS_TV_CATEGORY>(ParsingObject.RequestData(id, "Category", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(categoryRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "Category", "Delete", EnumList.IHttpMethod.Put.ToString()));
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
            TOURIS_TV_CATEGORY categoryView = new TOURIS_TV_CATEGORY();
            TOURIS_TV_CATEGORY categoryRes = new TOURIS_TV_CATEGORY();

            categoryView.ID = id;

            categoryRes = JsonConvert.DeserializeObject<TOURIS_TV_CATEGORY>(ParsingObject.RequestData(id, "Category", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(categoryRes);
        }

    }
}