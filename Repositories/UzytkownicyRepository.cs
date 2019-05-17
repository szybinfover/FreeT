using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Freet.DTO;
using System.Security.Cryptography;
using System.Text;

namespace Freet.Repositories
{
    public class UzytkownicyRepository : IUzytkownicyRepository
    {
        IConfiguration configuration;
        public UzytkownicyRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IList<UzytkownikDTO> GetAll()
        {
            List<UzytkownikDTO> list = new List<UzytkownikDTO>();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {

                SqlCommand command = new SqlCommand("SELECT ID, LOGIN, IMIE, NAZWISKO FROM Uzytkownik", connection);
                command.CommandType = System.Data.CommandType.Text;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UzytkownikDTO dto = new UzytkownikDTO();
                    dto.Id = reader.GetInt64(0);
                    dto.Login = reader.GetString(1);
                    dto.Imie = reader.GetString(2);
                    dto.Nazwisko = reader.GetString(3);
                    list.Add(dto);
                }
                connection.Close();
            }
            return list;
        }

        public bool Login (UzytkownikLogin dto)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT LOGIN FROM Uzytkownik WHERE login = @login AND haslo = @haslo", connection);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add("login", SqlDbType.VarChar);
                    command.Parameters["login"].Value = dto.Login;
                    command.Parameters.Add("haslo", SqlDbType.VarChar);
                    command.Parameters["haslo"].Value = dto.Haslo;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string Login = "";
                        Login = reader.GetString(0);
                        Console.WriteLine("User login: "+ Login);
                    }
                    return true;
                }
                catch
                {
                    connection.Close();
                    return false;
                }
                finally
                {
                    connection.Close();
                }
                return true;
            }
        }
    }
}