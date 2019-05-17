using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FreeT.DTO;
using System;

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

            SqlCommand command = new SqlCommand(@"SELECT U.Id, U.DataOd, U.DataDo, U.Uzytkownik_Id FROM Urlop U WHERE Uzytkownik_Id = @Uzytkownik_Id", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("uzytkownik_id", SqlDbType.BigInt);
            command.Parameters["uzytkownik_id"].Value = Uzytkownik_Id;
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                dto.Id = reader.GetInt64(0);
                dto.Data_Od = reader.GetDateTime(1);
                dto.Data_Do = reader.GetDateTime(2);
                dto.Uzytkownik_Id = reader.GetInt64(3);
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

        public UrlopDTO GetFromData(long Uzytkownik_Id, DateTime Data)
        {
        UrlopDTO dto = new UrlopDTO();
        using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            
            SqlCommand command = new SqlCommand(@"SELECT U.Id, U.DataOd, U.DataDo, U.Uzytkownik_Id FROM Urlop U WHERE Uzytkownik_Id = @Uzytkownik_Id and @Data between @Data_Od and @Data_Do", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("uzytkownik_id", SqlDbType.BigInt);
            command.Parameters["uzytkownik_id"].Value = Uzytkownik_Id;
            command.Parameters.Add("Data", SqlDbType.DateTime);
            command.Parameters["Data"].Value = Data;
            command.Parameters.Add("Data_Od", SqlDbType.DateTime);
            command.Parameters["Data_Od"].Value = Data;
            command.Parameters.Add("Data_Do", SqlDbType.DateTime);
            command.Parameters["Data_Do"].Value = Data;
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                dto.Id = reader.GetInt64(0);
                dto.Data_Od = reader.GetDateTime(1);
                dto.Data_Do = reader.GetDateTime(2);
                dto.Uzytkownik_Id = reader.GetInt64(3);
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
                dto.Id = reader.GetInt64(0);
                dto.Data_Od = reader.GetDateTime(1);
                dto.Data_Do = reader.GetDateTime(2);
                dto.Uzytkownik_Id = reader.GetInt64(3);
                list.Add(dto);
                }
                connection.Close();
            }
            return list;
        }

        public bool Delete(Int64 Urlop_Id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                SqlCommand command = new SqlCommand(@"DELETE FROM Urlop WHERE ID = @Urlop_Id", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add("Urlop_Id", SqlDbType.BigInt);
                command.Parameters["Urlop_Id"].Value = Urlop_Id;
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

        public bool SprawdzCzyIdIstnieje(Int64 Urlop_Id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {

                SqlCommand command = new SqlCommand("SELECT ID FROM Urlop WHERE ID = @Urlop_Id", connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@Urlop_Id", Urlop_Id);
                connection.Open();
                var result = command.ExecuteScalar();

                connection.Close();
                if (result == null)
                    return false;
                else
                    return true;
            }
        }

        public bool SprawdzIloscDni(DateTime Data_Do, DateTime Data_Od)
        {
            if(Data_Od <= Data_Do)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}