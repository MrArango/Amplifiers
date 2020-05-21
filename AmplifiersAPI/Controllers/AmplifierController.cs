using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amplifiers;
using AmplifiersAPI.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AmplifiersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmplifierController : ControllerBase
    {
        private readonly ILogger<AmplifierController> _logger;

        [HttpGet]
        public IActionResult Get()
        {
            Respon res = new Respon();
            try
            {
                using (var db = new SqlConnection(Properties.Resources.strcon))
                {
                    IEnumerable<Amplifier> amplifiers = db.Query<Amplifier>("select Id_Amplifier, Name, BassKnob, MidKnob, TrebleKnob from Amplifiers");

                    res.Valid = true;
                    res.Data = amplifiers;
                    res.Message = "Petición de Amplificadores OK";

                    _logger.LogInformation("Petición de todos los Amplificadores OK");

                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Valid = false;
                res.Data = null;

                res.Message = "La petición de Amplificadores Fallo";
                _logger.LogInformation(ex.Message);
                return Ok();
            }

        }

        public AmplifierController(ILogger<AmplifierController> logger)
        {
            _logger = logger;
        }

    }
}