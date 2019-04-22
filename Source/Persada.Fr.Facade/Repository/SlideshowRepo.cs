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
    public class SlideshowRepo : ApiResData, ISlideshow
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public SlideshowRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_SLIDESHOW> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_SLIDESHOW.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_SLIDESHOW Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_SLIDESHOW.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_SLIDESHOW slideshow)
        {
            try
            {
                _ctx.TOURIS_TM_SLIDESHOW.Add(slideshow);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_SLIDESHOW slideshow)
        {
            try
            {
                TOURIS_TM_SLIDESHOW slideshowNew = _ctx.TOURIS_TM_SLIDESHOW.Find(slideshow.ID);
                slideshowNew.TITTLE = slideshow.TITTLE;
                slideshowNew.CONTENT_DESCRIPTION = slideshow.CONTENT_DESCRIPTION;
                slideshowNew.CLASS = slideshow.CLASS;
                slideshowNew.PHOTO_PATH = slideshow.PHOTO_PATH;
                slideshowNew.URL = slideshow.URL;
                slideshowNew.LAST_MODIFIED_TIME = slideshow.LAST_MODIFIED_TIME;
                slideshowNew.LAST_MODIFIED_BY = slideshow.LAST_MODIFIED_BY;
                _ctx.Entry(slideshowNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_SLIDESHOW slideshow = _ctx.TOURIS_TM_SLIDESHOW.Find(id);
                slideshow.LAST_MODIFIED_TIME = modifiedTime;
                slideshow.LAST_MODIFIED_BY = modifiedBy;
                slideshow.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(slideshow).State = EntityState.Modified;
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
