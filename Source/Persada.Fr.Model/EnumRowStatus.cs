using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Model
{
    public class EnumFunctionStatus
    {
        public string Save { get; set; }
        public string Edit { get; set; }
        public string Delete { get; set; }

        public string DataNf { get; set; }
        public string DataIsntValid { get; set; }

        public string SFailed { get; set; }
        public string EFailed { get; set; }
        public string DFailed { get; set; }
    }

    public class EnumFuncStatus
    {
        public EnumFuncStatus()
        {
            fg = new EnumFunctionStatus()
            {
                Save = "save",
                Edit = "edit",
                Delete = "delete",
                DataNf= "Data Tidak Ditemukan.",
                DataIsntValid= "Data tidak valid.",
                SFailed = "Simpan Data Gagal.",
                EFailed= "Edit Data Gagal.",
                DFailed= "Hapus Data Gagal."
            };
        }
        public EnumFunctionStatus fg { get; set; }
    }

    public class EnumRowStatus
    {
        public int IsActive { get; set; }
        public int NotActive { get; set; }
    }

    public class EnumStatus
    {
        public EnumStatus()
        {
            fg = new EnumRowStatus()
            {
                IsActive = 0,
                NotActive = -1
            };
        }

        public EnumRowStatus fg { get; set; }
    }
}
