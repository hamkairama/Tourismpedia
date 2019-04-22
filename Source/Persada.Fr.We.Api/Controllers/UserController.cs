using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Facade.Repository;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using Persada.Fr.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Persada.Fr.We.Api.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private EnumStatus eStat = new EnumStatus();
        private EnumFuncStatus eFunc = new EnumFuncStatus();
        private ResultStatus rs = new ResultStatus();
        private JObject resObj = new JObject();

        Facade.Interface.IUser repo;
        public UserController()
        {
            repo = new UserRepo();
        }

        [HttpGet]
        [ResponseType(typeof(List<TOURIS_TV_USER>))]
        [Route("GridBind")]
        public IHttpActionResult GridBind()
        {
            ApiResData res = new ApiResData();

            try
            {
                List<TOURIS_TM_USER> users = repo.GridBind();
                List<TOURIS_TV_USER> userViews = new List<TOURIS_TV_USER>();

                if (users.Count > 0)
                {
                    foreach (var item in users)
                    {
                        TOURIS_TV_USER userView = new TOURIS_TV_USER();
                        userView.ID = item.ID;
                        userView.USER_ID = item.USER_ID;
                        userView.USER_MAIL = item.USER_MAIL;
                        userView.USER_NAME = item.USER_NAME;
                        userView.IS_SUPER_ADMIN = item.IS_SUPER_ADMIN;
                        userView.LAST_LOGIN = item.LAST_LOGIN;
                        userView.PASSWORD = item.PASSWORD;
                        userView.CREATED_BY = item.CREATED_BY;
                        userView.CREATED_TIME = item.CREATED_TIME;
                        userView.LAST_MODIFIED_BY = item.LAST_MODIFIED_BY;
                        userView.LAST_MODIFIED_TIME = item.LAST_MODIFIED_TIME;

                        if (item.TOURIS_TM_USER_PROFILE.Count > 0)
                        {
                            TOURIS_TV_USER_PROFILE userProfileView = new TOURIS_TV_USER_PROFILE();
                            userProfileView.ID = item.ID;
                            userProfileView.USER_ID_ID = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().USER_ID_ID;
                            userProfileView.PHOTO_PATH = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().PHOTO_PATH;
                            userProfileView.GENDER = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().GENDER;
                            userProfileView.BORN = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().BORN;
                            userProfileView.ADDRESS = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().ADDRESS;
                            userProfileView.DESCRIPTION = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().DESCRIPTION;
                            userProfileView.JOB = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().JOB;
                            userProfileView.COMPANY = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().COMPANY;
                            userProfileView.HOBBY = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().HOBBY;
                            userProfileView.CREATED_BY = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().CREATED_BY;
                            userProfileView.CREATED_TIME = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().CREATED_TIME;
                            userProfileView.LAST_MODIFIED_BY = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().LAST_MODIFIED_BY;
                            userProfileView.LAST_MODIFIED_TIME = item.TOURIS_TM_USER_PROFILE.FirstOrDefault().LAST_MODIFIED_TIME;

                            if (item.TOURIS_TM_USER_PROFILE.FirstOrDefault().TOURIS_TM_USER_PROFILE_SOSMED.Count > 0)
                            {
                                foreach (var itemSosmed in item.TOURIS_TM_USER_PROFILE.FirstOrDefault().TOURIS_TM_USER_PROFILE_SOSMED)
                                {
                                    TOURIS_TV_USER_PROFILE_SOSMED userProfileSosmedView = new TOURIS_TV_USER_PROFILE_SOSMED();
                                    userProfileSosmedView.ID = item.ID;
                                    userProfileSosmedView.USER_PROFILE_ID = itemSosmed.USER_PROFILE_ID;
                                    userProfileSosmedView.SOSMED_NAME = itemSosmed.SOSMED_NAME;
                                    userProfileSosmedView.SOSMED_PATH = itemSosmed.SOSMED_PATH;
                                    userProfileSosmedView.CREATED_BY = itemSosmed.CREATED_BY;
                                    userProfileSosmedView.CREATED_TIME = itemSosmed.CREATED_TIME;
                                    userProfileSosmedView.LAST_MODIFIED_BY = itemSosmed.LAST_MODIFIED_BY;
                                    userProfileSosmedView.LAST_MODIFIED_TIME = itemSosmed.LAST_MODIFIED_TIME;

                                    userProfileView.TOURIS_TV_USER_PROFILE_SOSMED.Add(userProfileSosmedView);
                                }
                            }                           

                            userView.TOURIS_TV_USER_PROFILE.Add(userProfileView);
                        }                        

                        userViews.Add(userView);
                    }
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { userViews }, null));
                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { rs }, new Exception(eFunc.fg.DataNf)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }
        }

        [HttpGet]
        [ResponseType(typeof(TOURIS_TV_USER))]
        [Route("RetrieveData")]
        public IHttpActionResult RetrieveData(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                TOURIS_TM_USER user = repo.Retrieve(id);
                TOURIS_TV_USER userView = new TOURIS_TV_USER();

                if (user != null)
                {
                    userView.ID = user.ID;
                    userView.USER_ID = user.USER_ID;
                    userView.USER_MAIL = user.USER_MAIL;
                    userView.USER_NAME = user.USER_NAME;
                    userView.IS_SUPER_ADMIN = user.IS_SUPER_ADMIN;
                    userView.LAST_LOGIN = user.LAST_LOGIN;
                    userView.PASSWORD = user.PASSWORD;
                    userView.PHOTO_PATH = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().PHOTO_PATH;
                    userView.GENDER = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().GENDER;
                    userView.BORN = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().BORN;
                    userView.ADDRESS = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().ADDRESS;
                    userView.DESCRIPTION = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().DESCRIPTION;
                    userView.JOB = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().JOB;
                    userView.COMPANY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().COMPANY;
                    userView.HOBBY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().HOBBY;
                    userView.CREATED_BY = user.CREATED_BY;
                    userView.CREATED_TIME = user.CREATED_TIME;
                    userView.LAST_MODIFIED_BY = user.LAST_MODIFIED_BY;
                    userView.LAST_MODIFIED_TIME = user.LAST_MODIFIED_TIME;

                    if (user.TOURIS_TM_USER_PROFILE != null)
                    {
                        TOURIS_TV_USER_PROFILE userProfileView = new TOURIS_TV_USER_PROFILE();
                        userProfileView.ID = user.ID;
                        userProfileView.USER_ID_ID = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().USER_ID_ID;
                        userProfileView.PHOTO_PATH = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().PHOTO_PATH;
                        userProfileView.GENDER = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().GENDER;
                        userProfileView.BORN = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().BORN;
                        userProfileView.ADDRESS = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().ADDRESS;
                        userProfileView.DESCRIPTION = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().DESCRIPTION;
                        userProfileView.JOB = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().JOB;
                        userProfileView.COMPANY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().COMPANY;
                        userProfileView.HOBBY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().HOBBY;
                        userProfileView.CREATED_BY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().CREATED_BY;
                        userProfileView.CREATED_TIME = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().CREATED_TIME;
                        userProfileView.LAST_MODIFIED_BY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().LAST_MODIFIED_BY;
                        userProfileView.LAST_MODIFIED_TIME = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().LAST_MODIFIED_TIME;

                        if (user.TOURIS_TM_USER_PROFILE.FirstOrDefault().TOURIS_TM_USER_PROFILE_SOSMED.Count > 0)
                        {
                            foreach (var itemSosmed in user.TOURIS_TM_USER_PROFILE.FirstOrDefault().TOURIS_TM_USER_PROFILE_SOSMED)
                            {
                                TOURIS_TV_USER_PROFILE_SOSMED userProfileSosmedView = new TOURIS_TV_USER_PROFILE_SOSMED();
                                userProfileSosmedView.ID = user.ID;
                                userProfileSosmedView.USER_PROFILE_ID = itemSosmed.USER_PROFILE_ID;
                                userProfileSosmedView.SOSMED_NAME = itemSosmed.SOSMED_NAME;
                                userProfileSosmedView.SOSMED_PATH = itemSosmed.SOSMED_PATH;
                                userProfileSosmedView.CREATED_BY = itemSosmed.CREATED_BY;
                                userProfileSosmedView.CREATED_TIME = itemSosmed.CREATED_TIME;
                                userProfileSosmedView.LAST_MODIFIED_BY = itemSosmed.LAST_MODIFIED_BY;
                                userProfileSosmedView.LAST_MODIFIED_TIME = itemSosmed.LAST_MODIFIED_TIME;

                                userProfileView.TOURIS_TV_USER_PROFILE_SOSMED.Add(userProfileSosmedView);
                            }
                        }
                        
                        userView.TOURIS_TV_USER_PROFILE.Add(userProfileView);
                    }                 
                                        
                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { userView }, null));

                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { rs }, new Exception(eFunc.fg.DataNf)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }
        }

        [HttpPost]
        [ResponseType(typeof(ResultStatus))]
        [Route("Add")]
        public IHttpActionResult Add(TOURIS_TV_USER userView)
        {
            ApiResData res = new ApiResData();
            TOURIS_TM_USER user = new TOURIS_TM_USER();
            TOURIS_TM_USER_PROFILE userProfile = new TOURIS_TM_USER_PROFILE();
            List<TOURIS_TM_USER_PROFILE_SOSMED> userProfileSosmeds = new List<TOURIS_TM_USER_PROFILE_SOSMED>();
            try
            {
                if (!ModelState.IsValid)
                {
                    rs.SetErrorStatus(eFunc.fg.SFailed);
                    resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Save, new Exception(eFunc.fg.DataIsntValid)));
                    return Content(HttpStatusCode.NotFound, resObj);
                }

                if (userView != null)
                {
                    user.ID = userView.ID;
                    user.USER_ID = userView.USER_ID;
                    user.USER_MAIL = userView.USER_MAIL;
                    user.USER_NAME = userView.USER_NAME;
                    user.IS_SUPER_ADMIN = userView.IS_SUPER_ADMIN;
                    user.LAST_LOGIN = userView.LAST_LOGIN;
                    user.PASSWORD = userView.PASSWORD;
                    user.CREATED_BY = userView.CREATED_BY;
                    user.CREATED_TIME = userView.CREATED_TIME;
                    user.LAST_MODIFIED_BY = userView.LAST_MODIFIED_BY;
                    user.LAST_MODIFIED_TIME = userView.LAST_MODIFIED_TIME;

                    if (userView.TOURIS_TV_USER_PROFILE != null)
                    {
                        userProfile.ID = userView.ID;
                        userProfile.USER_ID_ID = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().USER_ID_ID;
                        userProfile.PHOTO_PATH = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().PHOTO_PATH;
                        userProfile.GENDER = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().GENDER;
                        userProfile.BORN = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().BORN;
                        userProfile.ADDRESS = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().ADDRESS;
                        userProfile.DESCRIPTION = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().DESCRIPTION;
                        userProfile.JOB = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().JOB;
                        userProfile.COMPANY = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().COMPANY;
                        userProfile.HOBBY = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().HOBBY;
                        userProfile.CREATED_BY = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().CREATED_BY;
                        userProfile.CREATED_TIME = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().CREATED_TIME;
                        userProfile.LAST_MODIFIED_BY = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().LAST_MODIFIED_BY;
                        userProfile.LAST_MODIFIED_TIME = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().LAST_MODIFIED_TIME;

                        if (userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().TOURIS_TV_USER_PROFILE_SOSMED.Count > 0)
                        {
                            foreach (var itemSosmed in userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().TOURIS_TV_USER_PROFILE_SOSMED)
                            {
                                TOURIS_TM_USER_PROFILE_SOSMED userProfileSosmed = new TOURIS_TM_USER_PROFILE_SOSMED();
                                userProfileSosmed.ID = user.ID;
                                userProfileSosmed.USER_PROFILE_ID = itemSosmed.USER_PROFILE_ID;
                                userProfileSosmed.SOSMED_NAME = itemSosmed.SOSMED_NAME;
                                userProfileSosmed.SOSMED_PATH = itemSosmed.SOSMED_PATH;
                                userProfileSosmed.CREATED_BY = itemSosmed.CREATED_BY;
                                userProfileSosmed.CREATED_TIME = itemSosmed.CREATED_TIME;
                                userProfileSosmed.LAST_MODIFIED_BY = itemSosmed.LAST_MODIFIED_BY;
                                userProfileSosmed.LAST_MODIFIED_TIME = itemSosmed.LAST_MODIFIED_TIME;

                                userProfileSosmeds.Add(userProfileSosmed);
                            }
                        }
                       
                    }                   
                }
              

                rs = repo.Add(user, userProfile, userProfileSosmeds);
                if (rs.IsSuccess)
                    rs.SetSuccessStatus();
                else
                    rs.SetErrorStatus(eFunc.fg.SFailed);

                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Save, null));

                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Save, new Exception(eFunc.fg.SFailed)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }
        }

        [HttpPost]
        [ResponseType(typeof(ResultStatus))]
        [Route("Edit")]
        public IHttpActionResult Edit(TOURIS_TV_USER userView)
        {
            ApiResData res = new ApiResData();
            TOURIS_TM_USER user = new TOURIS_TM_USER();
            TOURIS_TM_USER_PROFILE userProfile = new TOURIS_TM_USER_PROFILE();
            List<TOURIS_TM_USER_PROFILE_SOSMED> userProfileSosmeds = new List<TOURIS_TM_USER_PROFILE_SOSMED>();

            try
            {
                //if (!ModelState.IsValid)
                //{
                //    rs.SetErrorStatus(eFunc.fg.SFailed);
                //    resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Edit, new Exception(eFunc.fg.DataIsntValid)));
                //    return Content(HttpStatusCode.NotFound, resObj);
                //}

                if (userView != null)
                {
                    user.ID = userView.ID;
                    user.USER_ID = userView.USER_ID;
                    user.USER_MAIL = userView.USER_MAIL;
                    user.USER_NAME = userView.USER_NAME;
                    user.IS_SUPER_ADMIN = userView.IS_SUPER_ADMIN;
                    user.LAST_MODIFIED_BY = userView.LAST_MODIFIED_BY;
                    user.LAST_MODIFIED_TIME = userView.LAST_MODIFIED_TIME;

                    if (userView.TOURIS_TV_USER_PROFILE != null)
                    {
                        userProfile.USER_ID_ID = userView.ID;
                        if (userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().PHOTO_PATH != null)
                        {
                            userProfile.PHOTO_PATH = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().PHOTO_PATH;
                        }
                        userProfile.GENDER = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().GENDER;
                        userProfile.BORN = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().BORN;
                        userProfile.ADDRESS = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().ADDRESS;
                        userProfile.DESCRIPTION = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().DESCRIPTION;
                        userProfile.JOB = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().JOB;
                        userProfile.COMPANY = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().COMPANY;
                        userProfile.HOBBY = userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().HOBBY;
                        userProfile.LAST_MODIFIED_BY = userView.LAST_MODIFIED_BY;
                        userProfile.LAST_MODIFIED_TIME = userView.LAST_MODIFIED_TIME;

                        if (userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().TOURIS_TV_USER_PROFILE_SOSMED.Count > 0)
                        {
                            foreach (var itemSosmed in userView.TOURIS_TV_USER_PROFILE.FirstOrDefault().TOURIS_TV_USER_PROFILE_SOSMED)
                            {
                                TOURIS_TM_USER_PROFILE_SOSMED userProfileSosmed = new TOURIS_TM_USER_PROFILE_SOSMED();
                                userProfileSosmed.ID = user.ID;
                                userProfileSosmed.USER_PROFILE_ID = itemSosmed.USER_PROFILE_ID;
                                userProfileSosmed.SOSMED_NAME = itemSosmed.SOSMED_NAME;
                                userProfileSosmed.SOSMED_PATH = itemSosmed.SOSMED_PATH;
                                userProfileSosmed.CREATED_BY = itemSosmed.CREATED_BY;
                                userProfileSosmed.CREATED_TIME = itemSosmed.CREATED_TIME;
                                userProfileSosmed.LAST_MODIFIED_BY = itemSosmed.LAST_MODIFIED_BY;
                                userProfileSosmed.LAST_MODIFIED_TIME = itemSosmed.LAST_MODIFIED_TIME;

                                userProfileSosmeds.Add(userProfileSosmed);
                            }
                        }                        
                    }
                   
                }

                rs = repo.Edit(user, userProfile, userProfileSosmeds);
                if (rs.IsSuccess)
                    rs.SetSuccessStatus();
                else
                    rs.SetErrorStatus(eFunc.fg.SFailed);

                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Edit, null));
                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Edit, new Exception(eFunc.fg.EFailed)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }
        }

        [HttpGet]
        [ResponseType(typeof(ResultStatus))]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            ApiResData res = new ApiResData();
            try
            {
                if (!ModelState.IsValid)
                {
                    rs.SetErrorStatus(eFunc.fg.SFailed);
                    resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Delete, new Exception(eFunc.fg.DataNf)));
                    return Content(HttpStatusCode.NotFound, resObj);
                }

                rs = repo.Delete(id, "System", CurrentUser.GetCurrentDateTime());
                if (rs.IsSuccess)
                    rs.SetSuccessStatus();
                else
                    rs.SetErrorStatus(eFunc.fg.SFailed);

                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Delete, null));
                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Delete, new Exception(eFunc.fg.DFailed)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }

        }

        [HttpPost]
        [ResponseType(typeof(TOURIS_TV_USER))]
        [Route("Login")]
        public IHttpActionResult Login(CUSTOM_LOGIN login)
        {
            ApiResData res = new ApiResData();
            try
            {
                if (!ModelState.IsValid)
                {
                    rs.SetErrorStatus(eFunc.fg.SFailed);
                    resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.DataNf, new Exception(eFunc.fg.DataIsntValid)));
                    return Content(HttpStatusCode.NotFound, resObj);
                }

                TOURIS_TM_USER user = repo.Login(login.Email, login.Password);
                TOURIS_TV_USER userView = new TOURIS_TV_USER();

                if (user != null)
                {
                    userView.ID = user.ID;
                    userView.USER_ID = user.USER_ID;
                    userView.USER_MAIL = user.USER_MAIL;
                    userView.USER_NAME = user.USER_NAME;
                    userView.IS_SUPER_ADMIN = user.IS_SUPER_ADMIN;
                    userView.LAST_LOGIN = user.LAST_LOGIN;
                    userView.PASSWORD = user.PASSWORD;
                    userView.PHOTO_PATH = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().PHOTO_PATH;
                    userView.GENDER = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().GENDER;
                    userView.BORN = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().BORN;
                    userView.ADDRESS = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().ADDRESS;
                    userView.DESCRIPTION = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().DESCRIPTION;
                    userView.JOB = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().JOB;
                    userView.COMPANY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().COMPANY;
                    userView.HOBBY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().HOBBY;
                    userView.CREATED_BY = user.CREATED_BY;
                    userView.CREATED_TIME = user.CREATED_TIME;
                    userView.LAST_MODIFIED_BY = user.LAST_MODIFIED_BY;
                    userView.LAST_MODIFIED_TIME = user.LAST_MODIFIED_TIME;

                    if (user.TOURIS_TM_USER_PROFILE != null)
                    {
                        TOURIS_TV_USER_PROFILE userProfileView = new TOURIS_TV_USER_PROFILE();
                        userProfileView.ID = user.ID;
                        userProfileView.USER_ID_ID = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().USER_ID_ID;
                        userProfileView.PHOTO_PATH = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().PHOTO_PATH;
                        userProfileView.GENDER = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().GENDER;
                        userProfileView.BORN = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().BORN;
                        userProfileView.ADDRESS = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().ADDRESS;
                        userProfileView.DESCRIPTION = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().DESCRIPTION;
                        userProfileView.JOB = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().JOB;
                        userProfileView.COMPANY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().COMPANY;
                        userProfileView.HOBBY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().HOBBY;
                        userProfileView.CREATED_BY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().CREATED_BY;
                        userProfileView.CREATED_TIME = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().CREATED_TIME;
                        userProfileView.LAST_MODIFIED_BY = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().LAST_MODIFIED_BY;
                        userProfileView.LAST_MODIFIED_TIME = user.TOURIS_TM_USER_PROFILE.FirstOrDefault().LAST_MODIFIED_TIME;

                        if (user.TOURIS_TM_USER_PROFILE.FirstOrDefault().TOURIS_TM_USER_PROFILE_SOSMED.Count > 0)
                        {
                            foreach (var itemSosmed in user.TOURIS_TM_USER_PROFILE.FirstOrDefault().TOURIS_TM_USER_PROFILE_SOSMED)
                            {
                                TOURIS_TV_USER_PROFILE_SOSMED userProfileSosmedView = new TOURIS_TV_USER_PROFILE_SOSMED();
                                userProfileSosmedView.ID = user.ID;
                                userProfileSosmedView.USER_PROFILE_ID = itemSosmed.USER_PROFILE_ID;
                                userProfileSosmedView.SOSMED_NAME = itemSosmed.SOSMED_NAME;
                                userProfileSosmedView.SOSMED_PATH = itemSosmed.SOSMED_PATH;
                                userProfileSosmedView.CREATED_BY = itemSosmed.CREATED_BY;
                                userProfileSosmedView.CREATED_TIME = itemSosmed.CREATED_TIME;
                                userProfileSosmedView.LAST_MODIFIED_BY = itemSosmed.LAST_MODIFIED_BY;
                                userProfileSosmedView.LAST_MODIFIED_TIME = itemSosmed.LAST_MODIFIED_TIME;

                                userProfileView.TOURIS_TV_USER_PROFILE_SOSMED.Add(userProfileSosmedView);
                            }
                        }

                        userView.TOURIS_TV_USER_PROFILE.Add(userProfileView);
                    }

                    rs.SetSuccessStatus();
                }
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { userView }, null));

                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { rs }, new Exception(eFunc.fg.DataNf)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }
        }

        [HttpPost]
        [ResponseType(typeof(ResultStatus))]
        [Route("ChangePassword")]
        public IHttpActionResult ChangePassword(CUSTOM_CHANGE_PASSWORD changePassword)
        {
            ApiResData res = new ApiResData();
            try
            {
                if (!ModelState.IsValid)
                {
                    rs.SetErrorStatus(eFunc.fg.SFailed);
                    resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.DataNf, new Exception(eFunc.fg.DataIsntValid)));
                    return Content(HttpStatusCode.NotFound, resObj);
                }

                rs = repo.ChangePassword(changePassword.IdUser, changePassword.OldPassword, changePassword.NewPassword);

                resObj = JObject.FromObject(res.ResCUD(new object[] { rs }, eFunc.fg.Delete, null));
                return Content(HttpStatusCode.OK, resObj);
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
                resObj = JObject.FromObject(res.ResGetDataTable(new object[] { rs }, new Exception(eFunc.fg.DataNf)));
                return Content(HttpStatusCode.BadRequest, resObj);
            }
        }


        private IHttpActionResult GetErrorResult(ResultStatus result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.IsSuccess)
            {
                if (result.MessageText != null)
                {
                    ModelState.AddModelError("", result.MessageText);
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
