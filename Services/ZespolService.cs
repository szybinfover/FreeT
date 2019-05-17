using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using FreeT.DTO;
using FreeT.Repositories;

namespace FreeT.Services
{
    public class ZespolService : IZespolService
    {
        IConfiguration configuration;
        IZespolRepository zespolRepository;
        public ZespolService(IConfiguration configuration, IZespolRepository zespolRepository)
        {
            this.configuration = configuration;
            this.zespolRepository = zespolRepository;
        }

        public bool Create(ZespolDTO dto)
        {
            return zespolRepository.Create(dto);
            throw new System.NotImplementedException();
        }

        public ZespolDTO Get(long Uzytkownik_Id)
        {
           return zespolRepository.Get(Uzytkownik_Id);
        }

        public IList<ZespolDTO> GetAll()
        {
            return zespolRepository.GetAll();
        }
    }
}