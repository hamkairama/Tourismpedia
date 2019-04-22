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
    public interface ICountry
    {
        List<TOURIS_TM_COUNTRY> GridBind();
        TOURIS_TM_COUNTRY Retrieve(int id);
        ResultStatus Add(TOURIS_TM_COUNTRY country);
        ResultStatus Edit(TOURIS_TM_COUNTRY country);
        ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime);
    }
}
