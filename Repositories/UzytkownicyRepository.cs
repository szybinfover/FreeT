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
                SqlCommand command = new SqlCommand("SELECT ID, LOGIN, IMIE, NAZWISKO, ZESPOL_ID FROM Uzytkownik", connection);
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
                    dto.ZespolId = reader.GetInt64(4);
                    list.Add(dto);
                }
                connection.Close();
            }
            return list;
        }

        public IList<UzytkownikSelectDTO> GetUser(string login, string imie, string nazwisko, Int64 zespol_id)
        {
            List<UzytkownikSelectDTO> list = new List<UzytkownikSelectDTO>();
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand command = new SqlCommand(@"DECLARE @d_login varchar(50) = @login;
                    DECLARE @d_imie varchar(50) = @imie;
                    DECLARE @d_nazwisko varchar(200) = @nazwisko;
                    DECLARE @d_zespol_id bigint = @zespol_id;

                    SELECT * FROM Uzytkownik WHERE Login = IIF(@d_login is not null, @d_login, Login) 
                    AND Imie = IIF(@d_imie is not null, @d_imie, Imie) 
                    AND Nazwisko = IIF(@d_nazwisko is not null, @d_nazwisko, Nazwisko) 
                    AND Zespol_Id = IIF(@d_zespol_id is not null, @d_zespol_id, Zespol_Id);", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add("login", SqlDbType.VarChar);
                command.Parameters["login"].Value = login;
                command.Parameters.Add("imie", SqlDbType.VarChar);
                command.Parameters["imie"].Value = imie;
                command.Parameters.Add("nazwisko", SqlDbType.VarChar);
                command.Parameters["nazwisko"].Value = nazwisko;
                command.Parameters.Add("zespol_id", SqlDbType.BigInt);
                command.Parameters["zespol_id"].Value = zespol_id;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UzytkownikSelectDTO dtoReturn = new UzytkownikSelectDTO();
                    dtoReturn.Login = reader.GetString(0);
                    dtoReturn.Imie = reader.GetString(1);
                    dtoReturn.Nazwisko = reader.GetString(2);
                    dtoReturn.ZespolId = reader.GetInt64(3);
                    list.Add(dtoReturn);
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

        public bool SprawdzCzyIstnieje (UzytkownikAddDTO dto)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    bool loginFlag = false;
                    SqlCommand command = new SqlCommand("SELECT LOGIN FROM Uzytkownik WHERE login = @login", connection);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add("login", SqlDbType.VarChar);
                    command.Parameters["login"].Value = dto.Login;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string Login = "";
                        Login = reader.GetString(0);
                        if (dto.Login == Login)
                        {
                            Console.WriteLine("Existing user login: "+ Login);
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