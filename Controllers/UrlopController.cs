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

    [HttpPost]
    public IActionResult Create([FromBody]UrlopAddDTO dto)
    {
        var result = urlopService.Create(dto);
        if (result == null)
            return BadRequest(result);
        else
            return NoContent();
    }
  }
}