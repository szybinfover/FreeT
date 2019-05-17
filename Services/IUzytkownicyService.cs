using System;
using System.Collections.Generic;
using Freet.DTO;

namespace Freet.Services
{
    public interface IUzytkownicyService
    {
        IList<UzytkownikDTO> GetAll();
        bool Login(UzytkownikLogin dto);
    }
}