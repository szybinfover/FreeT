using System;
using System.Collections.Generic;
using Freet.DTO;

namespace Freet.Repositories
{
    public interface IUzytkownicyRepository
    {
        IList<UzytkownikDTO> GetAll();
        bool SprawdzCzyIstnieje (UzytkownikAddDTO dto);
        IList<UzytkownikSelectDTO> GetUser(UzytkownikSelectDTO dto);
        bool Login(UzytkownikLoginDTO dto);

        bool Create(UzytkownikAddDTO dto);
    }
}