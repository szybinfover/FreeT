using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Freet.DTO;
using Freet.Services;

namespace Freet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UzytkownicyController : ControllerBase
    {
        IConfiguration configuration;
        IUzytkownicyService uzytkownicyService;
        public UzytkownicyController(IConfiguration configuration, IUzytkownicyService uzytkownicyService)
        {
            this.configuration = configuration;
            this.uzytkownicyService = uzytkownicyService;
        }

        [HttpGet]
        public ActionResult<IList<UzytkownikDTO>> GetAll()
        {
            return uzytkownicyService.GetAll().ToList();
        }
    }
}