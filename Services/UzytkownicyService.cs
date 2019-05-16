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
        public bool Login(UzytkownikLoginDTO dto)
        {
            return uzytkownicyRepository.Login(dto);
        }
    }
}