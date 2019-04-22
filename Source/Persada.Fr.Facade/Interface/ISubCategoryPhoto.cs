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
    public interface ISubCategoryPhoto
    {
        List<TOURIS_TM_SUB_CATEGORY_PHOTO> GridBind();
        TOURIS_TM_SUB_CATEGORY_PHOTO Retrieve(int id);
        ResultStatus Add(TOURIS_TM_SUB_CATEGORY_PHOTO subCategoryPhoto);
        ResultStatus Edit(TOURIS_TM_SUB_CATEGORY_PHOTO subCategoryPhoto);
        ResultStatus Delete(int id, string modifiedBy, DateTime modifiedTime);
        List<TOURIS_TM_SUB_CATEGORY_PHOTO> GetSubCategoryPhotoBySubCategoryId(int subCategoryId);
    }
}
