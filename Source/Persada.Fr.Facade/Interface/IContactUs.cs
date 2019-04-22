using Newtonsoft.Json.Linq;
using Persada.Fr.CommonFunction;
using Persada.Fr.Model;
using Persada.Fr.Model.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Facade.Interface
{
    public interface IContactUs
    {
        List<TOURIS_TM_CONTACT_US> GridBind();
        TOURIS_TM_CONTACT_US Retrieve(int id);
        ResultStatus Add(TOURIS_TM_CONTACT_US contactUs);
        ResultStatus Edit(TOURIS_TM_CONTACT_US contactUs);
        ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime);
    }
}
