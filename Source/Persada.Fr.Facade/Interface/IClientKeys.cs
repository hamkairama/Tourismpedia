using Persada.Fr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Facade.Repository
{
    public interface IClientKeys
    {
        bool IsUniqueKeyAlreadyGenerate(int UserID);
        void GenerateUniqueKey(out string ClientID, out string ClientSecert);
        int SaveClientIDandClientSecert(ClientKey ClientKeys);
        int UpdateClientIDandClientSecert(ClientKey ClientKeys);
        ClientKey GetGenerateUniqueKeyByUserID(int UserID);
    }
}
