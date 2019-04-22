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
using System.Text;
using Persada.Fr.Facade;

namespace Persada.Fr.Web.Controllers
{
    public class SlideshowController : Controller
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
            List<TOURIS_TV_SLIDESHOW> slideshowRes = new List<TOURIS_TV_SLIDESHOW>();

            slideshowRes = JsonConvert.DeserializeObject<List<TOURIS_TV_SLIDESHOW>>(ParsingObject.RequestData(null, "Slideshow", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            return View(slideshowRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetClassActiveList = Dropdown.GetClassActiveList();

            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_SLIDESHOW slideshowView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    slideshowView.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                }
                   
                slideshowView.CREATED_BY = CurrentUser.GetCurrentUserId();
                slideshowView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(slideshowView, "Slideshow", "Add", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_SLIDESHOW slideshowView = new TOURIS_TV_SLIDESHOW();
            TOURIS_TV_SLIDESHOW slideshowRes = new TOURIS_TV_SLIDESHOW();
            ViewBag.GetClassActiveList = Dropdown.GetClassActiveList();

            slideshowView.ID = id;

            slideshowRes = JsonConvert.DeserializeObject<TOURIS_TV_SLIDESHOW>(ParsingObject.RequestData(id, "Slideshow", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(slideshowRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_SLIDESHOW slideshowView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    slideshowView.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                }
              
                slideshowView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                slideshowView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(slideshowView, "Slideshow", "Edit", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_SLIDESHOW slideshowView = new TOURIS_TV_SLIDESHOW();
            TOURIS_TV_SLIDESHOW slideshowRes = new TOURIS_TV_SLIDESHOW();

            slideshowView.ID = id;

            slideshowRes = JsonConvert.DeserializeObject<TOURIS_TV_SLIDESHOW>(ParsingObject.RequestData(id, "Slideshow", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(slideshowRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "Slideshow", "Delete", EnumList.IHttpMethod.Put.ToString()));
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
            TOURIS_TV_SLIDESHOW slideshowView = new TOURIS_TV_SLIDESHOW();
            TOURIS_TV_SLIDESHOW slideshowRes = new TOURIS_TV_SLIDESHOW();

            slideshowView.ID = id;

            slideshowRes = JsonConvert.DeserializeObject<TOURIS_TV_SLIDESHOW>(ParsingObject.RequestData(id, "Slideshow", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));

            return View(slideshowRes);
        }

    }
}