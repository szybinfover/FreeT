using System;
using System.Collections.Generic;
using FreeT.DTO;

namespace FreeT.Services
{
    public interface IUrlopService
    {
       IList<UrlopDTO> GetAll();
       UrlopDTO Get(Int64 id);
       bool Create(UrlopAddDTO dto);
    }
}