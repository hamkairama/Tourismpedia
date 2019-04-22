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
    public class SubCategoryPhotoRepo : ApiResData, ISubCategoryPhoto
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public SubCategoryPhotoRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_SUB_CATEGORY_PHOTO> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_SUB_CATEGORY_PHOTO.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_SUB_CATEGORY_PHOTO Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_SUB_CATEGORY_PHOTO.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_SUB_CATEGORY_PHOTO subCategoryPhoto)
        {
            try
            {
                _ctx.TOURIS_TM_SUB_CATEGORY_PHOTO.Add(subCategoryPhoto);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();

            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_SUB_CATEGORY_PHOTO subCategoryPhoto)
        {
            try
            {
                TOURIS_TM_SUB_CATEGORY_PHOTO subCategoryPhotoNew = _ctx.TOURIS_TM_SUB_CATEGORY_PHOTO.Find(subCategoryPhoto.ID);
                subCategoryPhotoNew.SUB_CATEGORY_ID = subCategoryPhoto.SUB_CATEGORY_ID;
                subCategoryPhotoNew.PHOTO_NAME = subCategoryPhoto.PHOTO_NAME;
                subCategoryPhotoNew.PHOTO_DESCRIPTION = subCategoryPhoto.PHOTO_DESCRIPTION;
                subCategoryPhotoNew.PHOTO_PATH = subCategoryPhoto.PHOTO_PATH;
                subCategoryPhotoNew.LAST_MODIFIED_TIME = subCategoryPhoto.LAST_MODIFIED_TIME;
                subCategoryPhotoNew.LAST_MODIFIED_BY = subCategoryPhoto.LAST_MODIFIED_BY;
                _ctx.Entry(subCategoryPhotoNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_SUB_CATEGORY_PHOTO subCategoryPhoto = _ctx.TOURIS_TM_SUB_CATEGORY_PHOTO.Find(id);
                subCategoryPhoto.LAST_MODIFIED_TIME = modifiedTime;
                subCategoryPhoto.LAST_MODIFIED_BY = modifiedBy;
                subCategoryPhoto.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(subCategoryPhoto).State = EntityState.Modified;
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public List<TOURIS_TM_SUB_CATEGORY_PHOTO> GetSubCategoryPhotoBySubCategoryId(int subCategoryId)
        {
            try
            {
                return _ctx.TOURIS_TM_SUB_CATEGORY_PHOTO.Where(x => x.ROW_STATUS == (int)EnumList.RowStatus.Active && x.SUB_CATEGORY_ID == subCategoryId).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
