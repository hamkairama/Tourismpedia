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
    public interface IDistrict
    {
        List<TOURIS_TM_DISTRICT> GridBind();
        TOURIS_TM_DISTRICT Retrieve(int id);
        ResultStatus Add(TOURIS_TM_DISTRICT district);
        ResultStatus Edit(TOURIS_TM_DISTRICT district);
        ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime);
        List<TOURIS_TM_DISTRICT> GetDistrictByCityId(int cityId); 
    }
}
