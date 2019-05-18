using System;
using System.Collections.Generic;
using FreeT.DTO;

namespace FreeT.Repositories
{
    public interface IZespolRepository
    {
        IList<ZespolDTO> GetAll();
       ZespolDTO Get(Int64 Uzytkownik_Id);
       bool Create(ZespolDTO dto);
    }
}