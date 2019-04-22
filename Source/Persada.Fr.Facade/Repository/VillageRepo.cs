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
    public class VillageRepo : ApiResData, IVillage
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public VillageRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_VILLAGE> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_VILLAGE.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_VILLAGE Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_VILLAGE.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_VILLAGE village)
        {
            try
            {
                _ctx.TOURIS_TM_VILLAGE.Add(village);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_VILLAGE village)
        {
            try
            {
                TOURIS_TM_VILLAGE villageNew = _ctx.TOURIS_TM_VILLAGE.Find(village.ID);
                villageNew.DISTRICT_ID = village.DISTRICT_ID;
                villageNew.VILLAGE_CODE = village.VILLAGE_CODE;
                villageNew.VILLAGE_NAME = village.VILLAGE_NAME;
                villageNew.VILLAGE_DESCRIPTION = village.VILLAGE_DESCRIPTION;
                villageNew.LAST_MODIFIED_TIME = village.LAST_MODIFIED_TIME;
                villageNew.LAST_MODIFIED_BY = village.LAST_MODIFIED_BY;
                _ctx.Entry(villageNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_VILLAGE province =  _ctx.TOURIS_TM_VILLAGE.Find(id);
                province.LAST_MODIFIED_TIME = modifiedTime;
                province.LAST_MODIFIED_BY = modifiedBy;
                province.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(province).State = EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public List<TOURIS_TM_VILLAGE> GetVillageByDistrictId(int districtId)
        {
            try
            {
                return _ctx.TOURIS_TM_VILLAGE.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active && x.DISTRICT_ID == districtId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
