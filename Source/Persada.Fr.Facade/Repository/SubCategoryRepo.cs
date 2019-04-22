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
    public class SubCategoryRepo : ApiResData, ISubCategory
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public SubCategoryRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_SUB_CATEGORY> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_SUB_CATEGORY.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_SUB_CATEGORY Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_SUB_CATEGORY.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_SUB_CATEGORY subCategory)
        {
            try
            {
                _ctx.TOURIS_TM_SUB_CATEGORY.Add(subCategory);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();

            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_SUB_CATEGORY subCategory)
        {
            try
            {
                TOURIS_TM_SUB_CATEGORY subCategoryNew = _ctx.TOURIS_TM_SUB_CATEGORY.Find(subCategory.ID);
                subCategoryNew.CATEGORY_ID = subCategory.CATEGORY_ID;
                subCategoryNew.VILLAGE_ID = subCategory.VILLAGE_ID;
                subCategoryNew.ADDRESS = subCategory.ADDRESS;
                subCategoryNew.SUB_CATEGORY_NAME = subCategory.SUB_CATEGORY_NAME;
                subCategoryNew.SUB_CATEGORY_DESCRIPTION = subCategory.SUB_CATEGORY_DESCRIPTION;
                subCategoryNew.START_TIME = subCategory.START_TIME;
                subCategoryNew.END_TIME = subCategory.END_TIME;
                subCategoryNew.PHOTO_PATH = subCategory.PHOTO_PATH;
                subCategoryNew.LAST_MODIFIED_TIME = subCategory.LAST_MODIFIED_TIME;
                subCategoryNew.LAST_MODIFIED_BY = subCategory.LAST_MODIFIED_BY;
                _ctx.Entry(subCategoryNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_SUB_CATEGORY subCategory = _ctx.TOURIS_TM_SUB_CATEGORY.Find(id);
                subCategory.LAST_MODIFIED_TIME = modifiedTime;
                subCategory.LAST_MODIFIED_BY = modifiedBy;
                subCategory.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(subCategory).State = EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public List<TOURIS_TM_SUB_CATEGORY> GetSubCategoryByCategoryId(int categoryId)
        {
            try
            {
                return _ctx.TOURIS_TM_SUB_CATEGORY.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active && x.CATEGORY_ID == categoryId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
