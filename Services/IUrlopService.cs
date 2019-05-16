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
       bool Create(UrlopAddDTO dto);
    }
}