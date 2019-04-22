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
    public class SosmedRepo : ApiResData, ISosmed
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public SosmedRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_SOSMED> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_SOSMED.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_SOSMED Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_SOSMED.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_SOSMED sosmed)
        {
            try
            {
                _ctx.TOURIS_TM_SOSMED.Add(sosmed);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_SOSMED sosmed)
        {
            try
            {
                TOURIS_TM_SOSMED sosmedNew = _ctx.TOURIS_TM_SOSMED.Find(sosmed.ID);
                sosmedNew.TYPE = sosmed.TYPE;
                sosmedNew.NAME = sosmed.NAME;
                sosmedNew.DESCRIPTION = sosmed.DESCRIPTION;
                sosmedNew.ICON = sosmed.ICON;
                sosmedNew.URL = sosmed.URL;
                sosmedNew.DATA_EMBED = sosmed.DATA_EMBED;
                sosmedNew.HEIGHT = sosmed.HEIGHT;
                sosmedNew.WIDTH = sosmed.WIDTH;
                sosmedNew.DATA_WIDGET_ID = sosmed.DATA_WIDGET_ID;
                sosmedNew.LAST_MODIFIED_TIME = sosmed.LAST_MODIFIED_TIME;
                sosmedNew.LAST_MODIFIED_BY = sosmed.LAST_MODIFIED_BY;
                _ctx.Entry(sosmedNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_SOSMED sosmed = _ctx.TOURIS_TM_SOSMED.Find(id);
                sosmed.LAST_MODIFIED_TIME = modifiedTime;
                sosmed.LAST_MODIFIED_BY = modifiedBy;
                sosmed.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(sosmed).State = EntityState.Modified;
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
