using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.ModelView
{
    public class CUSTOM_MAIL
    {
        public string SUBJECT { get; set; }
        public string BODY { get; set; }
        public bool ISBODYHTML { get; set; }


        public string[] FROM { get; set; }
        public string[] TO { get; set; }
        public string[] CC { get; set; }
    }
}
