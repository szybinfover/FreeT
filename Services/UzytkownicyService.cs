using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Freet.DTO;
using Freet.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Freet.Services
{
    public class UzytkownicyService : IUzytkownicyService
    {
        IConfiguration configuration;
        IUzytkownicyRepository uzytkownicyRepository;
        public UzytkownicyService(IConfiguration configuration, IUzytkownicyRepository uzytkownicyRepository)
        {
            this.configuration = configuration;
            this.uzytkownicyRepository = uzytkownicyRepository;
        }

        public IList<UzytkownikDTO> GetAll()
        {
            return uzytkownicyRepository.GetAll();
        }

        public IList<UzytkownikSelectDTO> GetUser(UzytkownikSelectDTO dto)
        {
            return uzytkownicyRepository.GetUser(dto);
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public string ConvertToHash(string source)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, source);
            }
        }
        public string CheckNewUserData(UzytkownikAddDTO dto)
        {
            dto.Haslo = ConvertToHash(dto.Haslo);
            bool okFlag = false;
            string ret = "";
            string valLogin = dto.Login;
            string valHaslo = dto.Haslo;

            string valImie = dto.Imie;
            string valNazwisko = dto.Nazwisko;
            Int64 valZespolId = dto.ZespolId;

            string loginInfo = "";
            string hasloInfo = "";
            string imieInfo = "";
            string nazwiskoInfo = "";
            string zespolIdkoInfo = "";
            string isUserExistInfo = "";

            if (valLogin.Length > 0 && valLogin.Length <=50)
            {
                okFlag = true;
            }
            else
            {
                okFlag = false;
                loginInfo = "Login niepoprawna ilość znaków. ";
            }
            
            if (valHaslo.Length > 0 && valHaslo.Length <=50)
            {
                okFlag = true;
            }
            else
            {
                okFlag = false;
                hasloInfo = "Haslo niepoprawna ilość znaków. ";
            }

            if (valImie.Length > 0 && valImie.Length <=50)
            {
                okFlag = true;
            }
            else
            {
                okFlag = false;
                imieInfo = "Imie niepoprawna ilość znaków. ";
            }

            if (valNazwisko.Length > 0 && valNazwisko.Length <=200)
            {
                okFlag = true;
            }
            else
            {
                okFlag = false;
                nazwiskoInfo = "Nazwisko niepoprawna ilość znaków. ";
            }

            if (valZespolId > 0)
            {
                okFlag = true;
            }
            else
            {
                okFlag = false;
                zespolIdkoInfo = "Wartosc idZespol nie moze byc pusta lub rowna zero. ";
            }

            if (!uzytkownicyRepository.SprawdzCzyIstnieje(dto))
            {
                okFlag = true;
            }
            else
            {
                isUserExistInfo = "Uzytkownik o takim loginie juz istnieje, podaj inny login. ";
                okFlag = false;
            }

            if(okFlag)
            {
                ret = "ok";
            }
            else
            {
                ret = loginInfo +" "+imieInfo +" "+nazwiskoInfo +" "+hasloInfo +" "+zespolIdkoInfo +" "+isUserExistInfo;
            }
            return ret;
        }
        public bool Login(UzytkownikLoginDTO dto)
        {
            return uzytkownicyRepository.Login(dto);
        }
        public bool Create(UzytkownikAddDTO dto)
        {
            return uzytkownicyRepository.Create(dto);
        }
    }
}