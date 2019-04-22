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
    public interface ISosmed
    {
        List<TOURIS_TM_SOSMED> GridBind();
        TOURIS_TM_SOSMED Retrieve(int id);
        ResultStatus Add(TOURIS_TM_SOSMED sosmed);
        ResultStatus Edit(TOURIS_TM_SOSMED sosmed);
        ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime);
    }
}
