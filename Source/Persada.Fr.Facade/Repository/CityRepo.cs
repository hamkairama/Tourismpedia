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
    public class CityRepo : ApiResData, ICity
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public CityRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_CITY> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_CITY.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_CITY Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_CITY.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_CITY city)
        {
            try
            {
                _ctx.TOURIS_TM_CITY.Add(city);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_CITY city)
        {
            try
            {
                TOURIS_TM_CITY cityNew = _ctx.TOURIS_TM_CITY.Find(city.ID);
                cityNew.PROVINCE_ID = city.PROVINCE_ID;
                cityNew.CITY_CODE = city.CITY_CODE;
                cityNew.CITY_NAME = city.CITY_NAME;
                cityNew.CITY_DESCRIPTION = city.CITY_DESCRIPTION;
                cityNew.LAST_MODIFIED_TIME = city.LAST_MODIFIED_TIME;
                cityNew.LAST_MODIFIED_BY = city.LAST_MODIFIED_BY;
                _ctx.Entry(cityNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_CITY province =  _ctx.TOURIS_TM_CITY.Find(id);
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

        public List<TOURIS_TM_CITY> GetCityByProvinceId(int provinceId)
        {
            try
            {
                return _ctx.TOURIS_TM_CITY.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active && x.PROVINCE_ID == provinceId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
