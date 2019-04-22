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
using System.Net;

namespace Persada.Fr.Web.Controllers
{
    public class UserController : Controller
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
            List<TOURIS_TV_USER> userRes = new List<TOURIS_TV_USER>();

            userRes = JsonConvert.DeserializeObject<List<TOURIS_TV_USER>>(ParsingObject.RequestData(null, "User", "GridBind", EnumList.IHttpMethod.Get.ToString()));
            return View(userRes);
        }

        public ActionResult Create()
        {
            ViewBag.GetGenderList = Dropdown.GetGenderList();
            return View();
        }

        public ActionResult ActionCreate(TOURIS_TV_USER userView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";

                TOURIS_TV_USER_PROFILE userProfileView = new TOURIS_TV_USER_PROFILE();
                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    userView.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                    userProfileView.PHOTO_PATH = userView.PHOTO_PATH;
                }

                if (CurrentUser.GetCurrentUserId() != "")
                {
                    userView.CREATED_BY = CurrentUser.GetCurrentUserId();
                }else
                {
                    userView.CREATED_BY = userView.USER_ID;
                }
                userView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                userProfileView.GENDER = userView.GENDER;
                userProfileView.BORN = userView.BORN;
                userProfileView.ADDRESS = userView.ADDRESS;
                userProfileView.DESCRIPTION = userView.DESCRIPTION;
                userProfileView.JOB = userView.JOB;
                userProfileView.COMPANY = userView.COMPANY;
                userProfileView.HOBBY = userView.HOBBY;
                userProfileView.CREATED_BY = userView.CREATED_BY;
                userProfileView.CREATED_TIME = CurrentUser.GetCurrentDateTime();

                userView.TOURIS_TV_USER_PROFILE.Add(userProfileView);

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(userView, "User", "Add", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_USER userView = new TOURIS_TV_USER();
            TOURIS_TV_USER userRes = new TOURIS_TV_USER();

            userView.ID = id;
            ViewBag.GetGenderList = Dropdown.GetGenderList();

            userRes = JsonConvert.DeserializeObject<TOURIS_TV_USER>(ParsingObject.RequestData(id, "User", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(userRes);
        }
        public ActionResult ActionEdit(TOURIS_TV_USER userView, HttpPostedFileBase postedFile)
        {
            try
            {
                string physicalPath = "";
                TOURIS_TV_USER_PROFILE userProfileView = new TOURIS_TV_USER_PROFILE();

                if (postedFile != null)
                {
                    string ImageName = System.IO.Path.GetFileName(postedFile.FileName); //file2 to store path and url  
                    physicalPath = Server.MapPath("~" + Common.GetPathFolderImg() + ImageName);

                    userView.PHOTO_PATH = Common.GetPathFolderImg() + ImageName;
                    userProfileView.PHOTO_PATH = userView.PHOTO_PATH;
                }

                userView.LAST_MODIFIED_BY = CurrentUser.GetCurrentUserId();
                userView.LAST_MODIFIED_TIME = CurrentUser.GetCurrentDateTime();

                userProfileView.GENDER = userView.GENDER;
                userProfileView.BORN = userView.BORN;
                userProfileView.ADDRESS = userView.ADDRESS;
                userProfileView.DESCRIPTION = userView.DESCRIPTION;
                userProfileView.JOB = userView.JOB;
                userProfileView.COMPANY = userView.COMPANY;
                userProfileView.HOBBY = userView.HOBBY;

                userView.TOURIS_TV_USER_PROFILE.Add(userProfileView);

                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(userView, "User", "Edit", EnumList.IHttpMethod.Post.ToString()));
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
            TOURIS_TV_USER userView = new TOURIS_TV_USER();
            TOURIS_TV_USER userRes = new TOURIS_TV_USER();

            userView.ID = id;

            userRes = JsonConvert.DeserializeObject<TOURIS_TV_USER>(ParsingObject.RequestData(id, "User", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(userRes);
        }
        public ActionResult ActionDelete(int id)
        {
            try
            {
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(id, "User", "Delete", EnumList.IHttpMethod.Put.ToString()));
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
            TOURIS_TV_USER userView = new TOURIS_TV_USER();
            TOURIS_TV_USER userRes = new TOURIS_TV_USER();

            userView.ID = id;

            userRes = JsonConvert.DeserializeObject<TOURIS_TV_USER>(ParsingObject.RequestData(id, "User", "RetrieveData", EnumList.IHttpMethod.Put.ToString()));
            return View(userRes);
        }

        public ActionResult ActionLogin(CUSTOM_LOGIN login)
        {
            TOURIS_TV_USER userView = new TOURIS_TV_USER();
            TOURIS_TV_USER userRes = new TOURIS_TV_USER();
            
            userRes = JsonConvert.DeserializeObject<TOURIS_TV_USER>(ParsingObject.RequestData(login, "User", "Login", EnumList.IHttpMethod.Post.ToString()));

            if (userRes.ID > 0)
            {
                Session["USER_ID_ID"] = userRes.ID;
                Session["USER_ID"] = userRes.USER_ID;
                Session["USER_EMAIL"] = userRes.USER_MAIL;
                Session["IS_SUPER_ADMIN"] = userRes.IS_SUPER_ADMIN;
                Session["USER_NAME"] = userRes.USER_NAME;
            }
            else
            {
                TempData["msgError"] = "Data incorrent";
                return RedirectToAction("Login");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            return View();
        }

        public ActionResult ActionLogoff()
        {
            Session["USER_ID_ID"] = null;
            Session["USER_ID"] = null;
            Session["USER_EMAIL"] = null;
            Session["IS_SUPER_ADMIN"] = null;
            Session["USER_NAME"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword()
        {
            ViewBag.msgSuccess = TempData["msgSuccess"];
            ViewBag.msgError = TempData["msgError"];
            return View();
        }

        public ActionResult ActionChangePassword(CUSTOM_CHANGE_PASSWORD changePassword)
        {
            try
            {
                changePassword.IdUser = CurrentUser.GetCurrentUserIdId();
                rs = JsonConvert.DeserializeObject<ResultStatus>(ParsingObject.RequestData(changePassword, "User", "ChangePassword", EnumList.IHttpMethod.Post.ToString()));
                if (rs.IsSuccess)
                {
                    TempData["msgSuccess"] = rs.MessageText;
                }
                else
                {
                    TempData["msgError"] = rs.MessageText;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                rs.SetErrorStatus("Data failed to changed");
                TempData["msgError"] = rs.MessageText;
            }

            return RedirectToAction("ChangePassword");
        }



    }
}