using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FreeT.DTO;

namespace FreeT.Repositories
{
    public class ZespolRepository : IZespolRepository
    {
        IConfiguration configuration;
        public ZespolRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool Create(ZespolDTO dto)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    SqlCommand command = new SqlCommand(@"INSERT INTO Zespol(Nazwa) VALUES (@Nazwa)", connection);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters["Nazwa"].Value = dto.Nazwa;
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

        public ZespolDTO Get(string Nazwa)
        {
        ZespolDTO dto = new ZespolDTO();
        using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {

            SqlCommand command = new SqlCommand(@"SELECT Z.Nazwa FROM Zespol Z WHERE ID = @ID", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("Nazwa", SqlDbType.Text);
            command.Parameters["Nazwa"].Value = Nazwa;
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                dto.Nazwa = reader.GetString(1);
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

        public ZespolDTO Get(long Uzytkownik_Id)
        {
            throw new System.NotImplementedException();
        }

        public IList<ZespolDTO> GetAll()
        {
            List<ZespolDTO> list = new List<ZespolDTO>();
        using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {

            SqlCommand command = new SqlCommand(@"SELECT U.Id, U.DataOd, U.DataDo, U.Uzytkownik_Id FROM Zespol U", connection);
            command.CommandType = System.Data.CommandType.Text;
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
            ZespolDTO dto = new ZespolDTO();
            dto.Nazwa = reader.GetString(1);
            list.Add(dto);
            }
            connection.Close();
        }
        return list;
        }
    }
}