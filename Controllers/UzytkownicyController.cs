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

        [HttpGet("user")]
        public ActionResult<IList<UzytkownikSelectDTO>> GetUser([FromQuery] string login, [FromQuery] string imie, [FromQuery] string nazwisko, [FromQuery] Int64 zespol_id)
        {
            UzytkownikSelectDTO dto = new UzytkownikSelectDTO();
            if(string.IsNullOrEmpty(login))
            {
                dto.Login = null;
            }
            else
            {
                dto.Login = login.Trim();
            }
            if(string.IsNullOrEmpty(imie))
            {
                dto.Imie = null;
            }
            else
            {
                dto.Imie = imie.Trim();
            }
            if(string.IsNullOrEmpty(nazwisko))
            {
                dto.Nazwisko = null;
            }
            else
            {
                dto.Nazwisko = nazwisko.Trim();
            }
            if(zespol_id > 0)
            {
                dto.ZespolId = zespol_id;
            }

            var result = uzytkownicyService.GetUser(dto).ToList();
            if(result != null)
            {
                return result;
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UzytkownikLoginDTO dto)
        {
            dto.Haslo = uzytkownicyService.ConvertToHash(dto.Haslo);
            bool okFlag = false;
            string valLogin = dto.Login;
            string valHaslo = dto.Haslo;

            string loginInfo = "";
            string hasloInfo = "";

            if (valLogin.Length > 0 && valLogin.Length <=50)
            {
                okFlag = true;
            }
            else
            {
                okFlag = false;
                loginInfo = "Login niepoprawna ilość znaków. ";
            }
            
            if (valHaslo.Length > 0 && valHaslo.Length <=50)
            {
                okFlag = true;
            }
            else
            {
                okFlag = false;
                hasloInfo = "Haslo niepoprawna ilość znaków. ";
            }

            if (okFlag)
            {
                var result = uzytkownicyService.Login(dto);
                return Ok(result);
            }
            else
            {
                return BadRequest(loginInfo+" "+hasloInfo);
            }
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] UzytkownikAddDTO dto)
        {
            var checkData = uzytkownicyService.CheckNewUserData(dto);
            if (checkData == "ok")
            {
                var result = uzytkownicyService.Create(dto);
                if (result == true)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            else
            {
                return BadRequest(checkData);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // uzytkownicyService.Create(dto);
        }
    }
}