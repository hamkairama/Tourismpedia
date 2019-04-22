using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Facade.Interface;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using Persada.Fr.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Facade.Repository
{
    public class RoleRepo : ApiResData, IRole
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public RoleRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_ROLE> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_ROLE.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_ROLE Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_ROLE.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_ROLE role)
        {
            try
            {
                _ctx.TOURIS_TM_ROLE.Add(role);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_ROLE role)
        {
            try
            {
                TOURIS_TM_ROLE roleNew = _ctx.TOURIS_TM_ROLE.Find(role.ID);
                roleNew.ROLE_NAME = role.ROLE_NAME;
                roleNew.ROLE_DESCRIPTION = role.ROLE_DESCRIPTION;
                roleNew.LAST_MODIFIED_TIME = role.LAST_MODIFIED_TIME;
                roleNew.LAST_MODIFIED_BY = role.LAST_MODIFIED_BY;
                _ctx.Entry(roleNew).State = System.Data.Entity.EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime)
        {
            try
            {
                TOURIS_TM_ROLE role = _ctx.TOURIS_TM_ROLE.Find(id);
                role.LAST_MODIFIED_TIME = modifiedTime;
                role.LAST_MODIFIED_BY = modifiedBy;
                role.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(role).State = EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }     
    }
}
