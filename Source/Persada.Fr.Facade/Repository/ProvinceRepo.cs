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
    public class ProvinceRepo : ApiResData, IProvince
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public ProvinceRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_PROVINCE> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_PROVINCE.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_PROVINCE Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_PROVINCE.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_PROVINCE province)
        {
            try
            {
                _ctx.TOURIS_TM_PROVINCE.Add(province);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_PROVINCE province)
        {
            try
            {
                TOURIS_TM_PROVINCE provinceNew = _ctx.TOURIS_TM_PROVINCE.Find(province.ID);
                provinceNew.COUNTRY_ID = province.COUNTRY_ID;
                provinceNew.PROVINCE_CODE = province.PROVINCE_CODE;
                provinceNew.PROVINCE_NAME = province.PROVINCE_NAME;
                provinceNew.PROVINCE_DESCRIPTION = province.PROVINCE_DESCRIPTION;
                provinceNew.LAST_MODIFIED_TIME = province.LAST_MODIFIED_TIME;
                provinceNew.LAST_MODIFIED_BY = province.LAST_MODIFIED_BY;
                _ctx.Entry(provinceNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_PROVINCE province =  _ctx.TOURIS_TM_PROVINCE.Find(id);
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

        public List<TOURIS_TM_PROVINCE> GetProvinceByCountryId(int countryId)
        {
            try
            {
                return _ctx.TOURIS_TM_PROVINCE.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active && x.COUNTRY_ID == countryId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
