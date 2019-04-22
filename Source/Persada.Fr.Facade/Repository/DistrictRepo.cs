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
    public class DistrictRepo : ApiResData, IDistrict
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public DistrictRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_DISTRICT> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_DISTRICT.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_DISTRICT Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_DISTRICT.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_DISTRICT district)
        {
            try
            {
                _ctx.TOURIS_TM_DISTRICT.Add(district);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_DISTRICT district)
        {
            try
            {
                TOURIS_TM_DISTRICT districtNew = _ctx.TOURIS_TM_DISTRICT.Find(district.ID);
                districtNew.CITY_ID = district.CITY_ID;
                districtNew.DISTRICT_CODE = district.DISTRICT_CODE;
                districtNew.DISTRICT_NAME = district.DISTRICT_NAME;
                districtNew.DISTRICT_DESCRIPTION = district.DISTRICT_DESCRIPTION;
                districtNew.LAST_MODIFIED_TIME = district.LAST_MODIFIED_TIME;
                districtNew.LAST_MODIFIED_BY = district.LAST_MODIFIED_BY;
                _ctx.Entry(districtNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_DISTRICT province =  _ctx.TOURIS_TM_DISTRICT.Find(id);
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

        public List<TOURIS_TM_DISTRICT> GetDistrictByCityId(int cityId)
        {
            try
            {
                return _ctx.TOURIS_TM_DISTRICT.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active && x.CITY_ID == cityId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
