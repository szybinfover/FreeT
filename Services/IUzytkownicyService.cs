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
        string CheckNewUserData(UzytkownikAddDTO dto);
        bool Create(UzytkownikAddDTO dto);
        bool Login(UzytkownikLoginDTO dto);
    }
}