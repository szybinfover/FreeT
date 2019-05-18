using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FreeT.DTO;

namespace FreeT.Services
{
    public interface IZespolService
    {
       IList<ZespolDTO> GetAll();
       ZespolDTO Get(Int64 Uzytkownik_Id);
       bool Create(ZespolDTO dto);
    }
}