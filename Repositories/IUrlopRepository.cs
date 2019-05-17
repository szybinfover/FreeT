using System;
using System.Collections.Generic;
using FreeT.DTO;

namespace FreeT.Repositories
{
    public interface IUrlopRepository
    {
        IList<UrlopDTO> GetAll();
       UrlopDTO Get(Int64 Uzytkownik_Id);
       bool Create(UrlopAddDTO dto);
       bool SprawdzCzyIdIstnieje(Int64 Urlop_Id);
       bool Delete(Int64 Urlop_Id);
    }
}