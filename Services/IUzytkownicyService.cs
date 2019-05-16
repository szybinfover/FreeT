using System;
using System.Collections.Generic;
using Freet.DTO;
using System.Security.Cryptography;
using System.Text;

namespace Freet.Services
{
    public interface IUzytkownicyService
    {
        IList<UzytkownikDTO> GetAll();
        string ConvertToHash(string source);
        bool Login(UzytkownikLoginDTO dto);
    }
}