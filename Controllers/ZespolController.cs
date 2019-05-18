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
  public class ZespolController : ControllerBase
  {
    private IZespolService zespolService;

    IConfiguration configuration;
    public ZespolController(IConfiguration configuration, IZespolService zespolService)
    {
      this.configuration = configuration;
      this.zespolService = zespolService;
    }

    [HttpGet]
    public ActionResult<IList<ZespolDTO>> GetAll()
    {
        return zespolService.GetAll().ToList();
    }

    [HttpGet("{Uzytkownik_Id}")]
    public ActionResult<ZespolDTO> Get(Int64 Uzytkownik_Id)
    {
        return zespolService.Get(Uzytkownik_Id);
    }

    [HttpPost]
    public IActionResult Create([FromBody]ZespolDTO dto)
    {
        var result = zespolService.Create(dto);
        if (result == null)
            return BadRequest(result);
        else
            return NoContent();
    }
  }
}