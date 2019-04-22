using Persada.Fr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persada.Fr.Facade.Repository
{
    public interface IAuthenticate
    {
        ClientKey GetClientKeysDetailsbyCLientIDandClientSecert(string clientID, string clientSecert);
        bool ValidateKeys(ClientKey ClientKeys);
        bool IsTokenAlreadyExists(int CompanyID);
        int DeleteGenerateToken(int CompanyID);
        int InsertToken(TokensManager token);
        string GenerateToken(ClientKey ClientKeys, DateTime IssuedOn);
    }
}
