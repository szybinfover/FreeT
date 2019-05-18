using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FreeT.DTO;

namespace FreeT.Services
{
    public interface IUrlopService
    {
       IList<UrlopDTO> GetAll();
       UrlopDTO Get(Int64 Uzytkownik_Id);
       UrlopDTO GetFromData(Int64 Uzytkownik_Id, DateTime Data);
       bool Create(UrlopAddDTO dto);
       string Delete(Int64 Urlop_Id);
    }
}