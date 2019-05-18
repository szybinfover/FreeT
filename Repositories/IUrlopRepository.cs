using System;
using System.Collections.Generic;
using FreeT.DTO;

namespace FreeT.Repositories
{
    public interface IUrlopRepository
    {
        IList<UrlopDTO> GetAll();
       UrlopDTO Get(Int64 Uzytkownik_Id);
       UrlopDTO GetFromData(Int64 Uzytkownik_Id, DateTime Data);
       bool Create(UrlopAddDTO dto);
       bool SprawdzCzyIdIstnieje(Int64 Urlop_Id);

       bool SprawdzIloscDni(DateTime Data_Do, DateTime Data_Od);
       bool Delete(Int64 Urlop_Id);
    }
}