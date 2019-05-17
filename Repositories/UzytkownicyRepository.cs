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

        public bool Login (UzytkownikLoginDTO dto)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    bool loginFlag = false;
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
                        if (dto.Login == Login)
                        {
                            Console.WriteLine("User login: "+ Login);
                            loginFlag = true;
                        }
                    }
                    return loginFlag;
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
            }
        }

        public bool Create (UzytkownikAddDTO dto)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Uzytkownik(login,imie,nazwisko,haslo,zespol_id) VALUES(@Login, @Imie, @Nazwisko, @Haslo, @ZespolId)", connection);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add("Login", SqlDbType.VarChar);
                    command.Parameters["Login"].Value = dto.Login;
                    command.Parameters.Add("Imie", SqlDbType.VarChar);
                    command.Parameters["Imie"].Value = dto.Imie;
                    command.Parameters.Add("Nazwisko", SqlDbType.VarChar);
                    command.Parameters["Nazwisko"].Value = dto.Nazwisko;
                    command.Parameters.Add("Haslo", SqlDbType.VarChar);
                    command.Parameters["Haslo"].Value = dto.Haslo;
                    command.Parameters.Add("ZespolId", SqlDbType.BigInt);
                    command.Parameters["ZespolId"].Value = dto.ZespolId;
                    connection.Open();
                    var result = command.ExecuteNonQuery();
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
            }
        }
    }
}