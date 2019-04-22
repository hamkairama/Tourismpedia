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
    public class MenuRepo : ApiResData, IMenu
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public MenuRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_MENU> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_MENU.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_MENU Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_MENU.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_MENU menu)
        {
            try
            {
                _ctx.TOURIS_TM_MENU.Add(menu);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_MENU menu)
        {
            try
            {
                TOURIS_TM_MENU menuNew = _ctx.TOURIS_TM_MENU.Find(menu.ID);
                menuNew.MENU_NAME = menu.MENU_NAME;
                menuNew.MENU_DESCRIPTION = menu.MENU_DESCRIPTION;
                menuNew.MENU_PARENT_ID = menu.MENU_PARENT_ID;
                menuNew.LAST_MODIFIED_TIME = menu.LAST_MODIFIED_TIME;
                menuNew.LAST_MODIFIED_BY = menu.LAST_MODIFIED_BY;
                _ctx.Entry(menuNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_MENU menu = _ctx.TOURIS_TM_MENU.Find(id);
                menu.LAST_MODIFIED_TIME = modifiedTime;
                menu.LAST_MODIFIED_BY = modifiedBy;
                menu.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(menu).State = EntityState.Modified;
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
