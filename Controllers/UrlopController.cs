using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FreeT.DTO;
using FreeT.Services;

namespace FreeT.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UrlopController : ControllerBase
  {
    private IUrlopService urlopService;


    IConfiguration configuration;
    public UrlopController(IConfiguration configuration, IUrlopService urlopService)
    {
      this.configuration = configuration;
      this.urlopService = urlopService;
    }

    [HttpGet]
    public ActionResult<IList<UrlopDTO>> GetAll()
    {
        return urlopService.GetAll().ToList();
    }

    [HttpGet("{Uzytkownik_Id}")]
    public ActionResult<UrlopDTO> Get(Int64 Uzytkownik_Id)
    {
        return urlopService.Get(Uzytkownik_Id);
    }

    [HttpGet("{Uzytkownik_Id}, {Data}")]
    public ActionResult<UrlopDTO> GetFromData(Int64 Uzytkownik_Id, DateTime Data)
    {
        return urlopService.GetFromData(Uzytkownik_Id, Data);
    }

    [HttpPost]
    public IActionResult Create([FromBody]UrlopAddDTO dto)
    {
        long s_uid = dto.Uzytkownik_Id;

        if(s_uid > 0)
        {
          var result = urlopService.Create(dto);
          if (result == false)
              return BadRequest("Podane daty są nieprawidłowe");
          else
              return NoContent();
        }
        else
        {
            return BadRequest("Id użytkownika musi być większe niż 0");
        }
    }

    [HttpDelete("{UrlopId}")]
    public IActionResult Delete(Int64 UrlopId)
    {
      var result = urlopService.Delete(UrlopId);

      if (result == null)
        return NoContent();
      else
        return BadRequest(result);
    }
  }
}