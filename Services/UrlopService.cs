using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FreeT.DTO;
using FreeT.Repositories;

namespace FreeT.Services
{
    public class UrlopService : IUrlopService
    {
        IConfiguration configuration;
        IUrlopRepository urlopyRepository;
        public UrlopService(IConfiguration configuration, IUrlopRepository urlopyRepository)
        {
            this.configuration = configuration;
            this.urlopyRepository = urlopyRepository;
        }

        public bool Create(UrlopAddDTO dto)
        {
            return urlopyRepository.Create(dto);
        }

        public UrlopDTO Get(long Uzytkownik_Id)
        {
           return urlopyRepository.Get(Uzytkownik_Id); 
        }

        public IList<UrlopDTO> GetAll()
        {
            return urlopyRepository.GetAll();
        }
    }
}