using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using Persada.Fr.ModelView;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Facade.Repository
{
    public class UserRepo : ApiResData, IUser
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public UserRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_USER> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_USER.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_USER Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_USER.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_USER user, TOURIS_TM_USER_PROFILE userProfile, List<TOURIS_TM_USER_PROFILE_SOSMED> userProfileSosmed)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    int userIdId = 0, userProfileId = 0;
                    rs = AddUser(user, ref userIdId);

                    if (rs.IsSuccess && userProfile.ADDRESS != "")
                    {
                        userProfile.USER_ID_ID = userIdId;
                        rs = AddUserProfile(userProfile, ref userProfileId);
                    }

                    if (rs.IsSuccess && userProfileSosmed.Count > 0)
                    {
                        List<TOURIS_TM_USER_PROFILE_SOSMED> userProfileSosmedNew = new List<TOURIS_TM_USER_PROFILE_SOSMED>();
                        foreach (var item in userProfileSosmed)
                        {
                            item.USER_PROFILE_ID = userProfileId;
                            userProfileSosmedNew.Add(item);
                        }
                        rs = AddUserProfileSosmed(userProfileSosmedNew);
                    }
                    transaction.Commit();
                    rs.SetSuccessStatus();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    rs.SetErrorStatus(ex.Message);
                }
            }
            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_USER user, TOURIS_TM_USER_PROFILE userProfile, List<TOURIS_TM_USER_PROFILE_SOSMED> userProfileSosmed)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    int userProfileId = 0;
                    rs = EditUser(user, ref userProfileId);

                    if (rs.IsSuccess && userProfile.ADDRESS != "")
                    {
                        userProfile.ID = userProfileId;
                        rs = EditUserProfile(userProfile);
                    }

                    if (rs.IsSuccess && userProfileSosmed.Count > 0)
                    {
                        List<TOURIS_TM_USER_PROFILE_SOSMED> userProfileSosmedNew = new List<TOURIS_TM_USER_PROFILE_SOSMED>();
                        foreach (var item in userProfileSosmed)
                        {
                            item.USER_PROFILE_ID = userProfileId;
                            userProfileSosmedNew.Add(item);
                        }
                        rs = AddUserProfileSosmed(userProfileSosmedNew);
                    }
                    transaction.Commit();
                    rs.SetSuccessStatus();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    rs.SetErrorStatus(ex.Message);
                }
            }


            return rs;
        }

        public ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime)
        {
            try
            {
                TOURIS_TM_USER user = _ctx.TOURIS_TM_USER.Find(id);
                user.LAST_MODIFIED_TIME = modifiedTime;
                user.LAST_MODIFIED_BY = modifiedBy;
                user.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(user).State = EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        private ResultStatus AddUser(TOURIS_TM_USER user, ref int userIdId)
        {
            _ctx.TOURIS_TM_USER.Add(user);
            _ctx.SaveChanges();

            userIdId = _ctx.TOURIS_TM_USER.Where(x => x.USER_MAIL == user.USER_MAIL).FirstOrDefault().ID;
            rs.SetSuccessStatus();

            return rs;
        }

        private ResultStatus AddUserProfile(TOURIS_TM_USER_PROFILE userProfile, ref int userProfileId)
        {
            _ctx.TOURIS_TM_USER_PROFILE.Add(userProfile);
            _ctx.SaveChanges();

            userProfileId = _ctx.TOURIS_TM_USER_PROFILE.Where(x => x.USER_ID_ID == userProfile.USER_ID_ID).FirstOrDefault().ID;
            rs.SetSuccessStatus();

            return rs;
        }

        private ResultStatus AddUserProfileSosmed(List<TOURIS_TM_USER_PROFILE_SOSMED> userProfileSosmed)
        {
            foreach (var item in userProfileSosmed)
            {
                _ctx.TOURIS_TM_USER_PROFILE_SOSMED.Add(item);
                _ctx.SaveChanges();
            }

            rs.SetSuccessStatus();

            return rs;
        }

        private ResultStatus EditUser(TOURIS_TM_USER user, ref int userProfileId)
        {
            TOURIS_TM_USER userNew = _ctx.TOURIS_TM_USER.Find(user.ID);
            userNew.USER_ID = user.USER_ID;
            userNew.USER_NAME = user.USER_NAME;
            userNew.USER_MAIL = user.USER_MAIL;
            userNew.IS_SUPER_ADMIN = user.IS_SUPER_ADMIN;
            userNew.LAST_MODIFIED_BY = user.LAST_MODIFIED_BY;
            userNew.LAST_MODIFIED_TIME = user.LAST_MODIFIED_TIME;

            userProfileId = userNew.TOURIS_TM_USER_PROFILE.FirstOrDefault().ID;

            _ctx.Entry(userNew).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            rs.SetSuccessStatus();

            return rs;
        }

        private ResultStatus EditUserProfile(TOURIS_TM_USER_PROFILE userProfile)
        {
            TOURIS_TM_USER_PROFILE userProfileNew = _ctx.TOURIS_TM_USER_PROFILE.Find(userProfile.ID);
            userProfileNew.ADDRESS = userProfile.ADDRESS;
            userProfileNew.BORN = userProfile.BORN;
            userProfileNew.DESCRIPTION = userProfile.DESCRIPTION;
            userProfileNew.GENDER = userProfile.GENDER;
            userProfileNew.COMPANY = userProfile.COMPANY;
            if (userProfile.PHOTO_PATH != null)
            {
                userProfileNew.PHOTO_PATH = userProfile.PHOTO_PATH;
            }
            userProfileNew.HOBBY = userProfile.HOBBY;
            userProfileNew.LAST_MODIFIED_BY = userProfile.LAST_MODIFIED_BY;
            userProfileNew.LAST_MODIFIED_TIME = userProfile.LAST_MODIFIED_TIME;

            _ctx.Entry(userProfileNew).State = System.Data.Entity.EntityState.Modified;
            _ctx.SaveChanges();
            rs.SetSuccessStatus();

            return rs;
        }

        private ResultStatus EditUserProfileSosmed(List<TOURIS_TM_USER_PROFILE_SOSMED> userProfileSosmed)
        {
            foreach (var item in userProfileSosmed)
            {
                rs = DeleteUserProfileSosmed(item);
            }

            if (rs.IsSuccess)
            {
                rs = AddUserProfileSosmed(userProfileSosmed);
            }

            return rs;
        }

        private ResultStatus DeleteUserProfileSosmed(TOURIS_TM_USER_PROFILE_SOSMED userProfileSosmed)
        {
            TOURIS_TM_USER_PROFILE_SOSMED userProfileSosmedDel = _ctx.TOURIS_TM_USER_PROFILE_SOSMED.Find(userProfileSosmed.ID);

            _ctx.TOURIS_TM_USER_PROFILE_SOSMED.Remove(userProfileSosmedDel);
            _ctx.SaveChanges();

            rs.SetSuccessStatus();

            return rs;
        }

        public TOURIS_TM_USER Login(string email, string password)
        {
            TOURIS_TM_USER user = _ctx.TOURIS_TM_USER.Where(x => x.USER_MAIL == email && x.PASSWORD == password).FirstOrDefault();
            return user;
        }

        public ResultStatus ChangePassword(int id, string oldPassword, string newPassword)
        {
            try
            {
                TOURIS_TM_USER user = _ctx.TOURIS_TM_USER.Find(id);

                if (user.PASSWORD == oldPassword)
                {
                    user.LAST_MODIFIED_TIME = DateTime.Now;
                    user.LAST_MODIFIED_BY = user.USER_ID;
                    user.PASSWORD = newPassword;

                    _ctx.Entry(user).State = EntityState.Modified;
                    _ctx.SaveChanges();
                    rs.SetSuccessStatus("Change password has been success");
                }
                else
                {
                    rs.SetErrorStatus("Old password is not match");
                }

            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }
    }
}
