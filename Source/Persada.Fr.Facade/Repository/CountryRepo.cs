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
    public class CountryRepo : ApiResData, ICountry
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public CountryRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_COUNTRY> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_COUNTRY.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_COUNTRY Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_COUNTRY.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_COUNTRY country)
        {
            try
            {
                _ctx.TOURIS_TM_COUNTRY.Add(country);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_COUNTRY country)
        {
            try
            {
                TOURIS_TM_COUNTRY countryNew = _ctx.TOURIS_TM_COUNTRY.Find(country.ID);
                countryNew.COUNTRY_CODE = country.COUNTRY_CODE;
                countryNew.COUNTRY_NAME = country.COUNTRY_NAME;
                countryNew.COUNTRY_DESCRIPTION = country.COUNTRY_DESCRIPTION;
                countryNew.LAST_MODIFIED_TIME = country.LAST_MODIFIED_TIME;
                countryNew.LAST_MODIFIED_BY = country.LAST_MODIFIED_BY;
                _ctx.Entry(countryNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_COUNTRY country = _ctx.TOURIS_TM_COUNTRY.Find(id);
                country.LAST_MODIFIED_TIME = modifiedTime;
                country.LAST_MODIFIED_BY = modifiedBy;
                country.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(country).State = EntityState.Modified;
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
