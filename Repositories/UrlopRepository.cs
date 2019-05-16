using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FreeT.DTO;

namespace FreeT.Repositories
{
    public class UrlopRepository : IUrlopRepository
    {
        IConfiguration configuration;
        public UrlopRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool Create(UrlopAddDTO dto)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    SqlCommand command = new SqlCommand(@"INSERT INTO Urlop (DataOd, DataDo, Uzytkownik_Id) VALUES (@data_od, @data_do, @uzytkownik_Id)", connection);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add("data_od", SqlDbType.DateTime);
                    command.Parameters["data_od"].Value = dto.Data_Od;
                    command.Parameters.Add("data_do", SqlDbType.DateTime);
                    command.Parameters["data_do"].Value = dto.Data_Do;
                    command.Parameters.Add("uzytkownik_id", SqlDbType.BigInt);
                    command.Parameters["uzytkownik_id"].Value = dto.Uzytkownik_Id;
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
                }
                    return true;
        }

        public UrlopDTO Get(long Uzytkownik_Id)
        {
        UrlopDTO dto = new UrlopDTO();
        using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {

            SqlCommand command = new SqlCommand(@"SELECT U.DataOd, U.DataDo, U.Uzytkownik_Id FROM Urlop U WHERE Uzytkownik_Id = @Uzytkownik_Id", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("uzytkownik_id", SqlDbType.BigInt);
            command.Parameters["uzytkownik_id"].Value = Uzytkownik_Id;
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                dto.Data_Od = reader.GetDateTime(0);
                dto.Data_Do = reader.GetDateTime(1);
                dto.Uzytkownik_Id = reader.GetInt64(2);
                connection.Close();
            }
            else
            {
                connection.Close();
                return null;
            }
            }
                return dto;
            }

        public IList<UrlopDTO> GetAll()
        {
            List<UrlopDTO> list = new List<UrlopDTO>();
        using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {

            SqlCommand command = new SqlCommand(@"SELECT U.Id, U.DataOd, U.DataDo, U.Uzytkownik_Id FROM Urlop U", connection);
            command.CommandType = System.Data.CommandType.Text;
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
            UrlopDTO dto = new UrlopDTO();
            dto.Data_Od = reader.GetDateTime(1);
            dto.Data_Do = reader.GetDateTime(2);
            dto.Uzytkownik_Id = reader.GetInt64(3);
            list.Add(dto);
            }
            connection.Close();
        }
        return list;
        }
    }
}