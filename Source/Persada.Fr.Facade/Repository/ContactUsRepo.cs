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
    public class ContactUsRepo : ApiResData, IContactUs
    {
        private EnumStatus eStat = new EnumStatus();
        private ResultStatus rs = new ResultStatus();
        private DbCtx _ctx;

        public ContactUsRepo()
        {
            _ctx = new DbCtx();
        }

        public List<TOURIS_TM_CONTACT_US> GridBind()
        {
            try
            {
                return _ctx.TOURIS_TM_CONTACT_US.Where(x=>x.ROW_STATUS == (int)EnumList.RowStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TOURIS_TM_CONTACT_US Retrieve(int id)
        {
            try
            {
                return _ctx.TOURIS_TM_CONTACT_US.Where(x => x.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultStatus Add(TOURIS_TM_CONTACT_US contactUs)
        {
            try
            {
                _ctx.TOURIS_TM_CONTACT_US.Add(contactUs);
                _ctx.SaveChanges();
                rs.SetSuccessStatus();
                
            }
            catch (Exception ex)
            {
                rs.SetErrorStatus(ex.Message);
            }

            return rs;
        }

        public ResultStatus Edit(TOURIS_TM_CONTACT_US contactUs)
        {
            try
            {
                TOURIS_TM_CONTACT_US contactUsNew = _ctx.TOURIS_TM_CONTACT_US.Find(contactUs.ID);
                contactUsNew.NAME_SENDER = contactUs.NAME_SENDER;
                contactUsNew.EMAIL_SENDER = contactUs.EMAIL_SENDER;
                contactUsNew.PHONE_SENDER = contactUs.EMAIL_SENDER;
                contactUsNew.DESCRIPTION = contactUs.DESCRIPTION;
                contactUsNew.LAST_MODIFIED_TIME = contactUs.LAST_MODIFIED_TIME;
                contactUsNew.LAST_MODIFIED_BY = contactUs.LAST_MODIFIED_BY;
                _ctx.Entry(contactUsNew).State = System.Data.Entity.EntityState.Modified;
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
                TOURIS_TM_CONTACT_US contactUs = _ctx.TOURIS_TM_CONTACT_US.Find(id);
                contactUs.LAST_MODIFIED_TIME = modifiedTime;
                contactUs.LAST_MODIFIED_BY = modifiedBy;
                contactUs.ROW_STATUS = eStat.fg.NotActive;

                _ctx.Entry(contactUs).State = EntityState.Modified;
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
