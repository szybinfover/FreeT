using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Freet.DTO;

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
    }
}