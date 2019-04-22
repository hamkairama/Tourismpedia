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
    public class CategoryRepo : ApiResData, ICategory
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public CategoryRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_CATEGORY> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_CATEGORY.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_CATEGORY Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_CATEGORY.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_CATEGORY category)
        {
            try
            {
                _ctx.TOURIS_TM_CATEGORY.Add(category);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_CATEGORY category)
        {
            try
            {
                TOURIS_TM_CATEGORY categoryNew = _ctx.TOURIS_TM_CATEGORY.Find(category.ID);
                categoryNew.CATEGORY_CODE = category.CATEGORY_CODE;
                categoryNew.CATEGORY_NAME = category.CATEGORY_NAME;
                categoryNew.CATEGORY_DESCRIPTION = category.CATEGORY_DESCRIPTION;
                categoryNew.URL = category.URL;
                categoryNew.CLASS = category.CLASS;
                categoryNew.PHOTO_PATH = category.PHOTO_PATH;
                categoryNew.LAST_MODIFIED_TIME = category.LAST_MODIFIED_TIME;
                categoryNew.LAST_MODIFIED_BY = category.LAST_MODIFIED_BY;
                _ctx.Entry(categoryNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_CATEGORY category = _ctx.TOURIS_TM_CATEGORY.Find(id);
                category.LAST_MODIFIED_TIME = modifiedTime;
                category.LAST_MODIFIED_BY = modifiedBy;
                category.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(category).State = EntityState.Modified;
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
