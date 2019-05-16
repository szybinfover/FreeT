using System;
using System.Collections.Generic;
using FreeT.DTO;

namespace FreeT.Repositories
{
    public interface IUrlopRepository
    {
        IList<UrlopDTO> GetAll();
       UrlopDTO Get(Int64 id);
       bool Create(UrlopAddDTO dto);
    }
}