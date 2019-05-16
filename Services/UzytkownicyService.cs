using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Freet.DTO;
using Freet.Repositories;

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
    }
}