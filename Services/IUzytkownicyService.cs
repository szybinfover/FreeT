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
        IList<UzytkownikSelectDTO> GetUser(string login, string imie, string nazwisko, Int64 zespol_id);
        bool Create(UzytkownikAddDTO dto);
        bool Login(UzytkownikLoginDTO dto);
    }
}