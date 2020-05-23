using System;
using System.Collections.Generic;
using AmplifiersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq;

namespace AmplifiersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmplifierController : ControllerBase
    {
        private readonly ILogger<AmplifierController> _logger;

        // GET: api/Amplifier
        [HttpGet]
        public IActionResult Get()
        {
            Respon res = new Respon();

            try
            {
                using (var db = new MySqlConnection(Properties.Resources.strcon))
                {
                    IEnumerable<Amplifier> amplifiers = db.Query<Amplifier>("select Id_Amplifier, Name, Bass, Mid, Treble from amplifiers");

                    if (amplifiers.Count() > 0)
                    {
                        res.Valid = true;
                        res.Data = amplifiers;
                        res.Message = "La Petición de Amplificadores OK";
                    }
                    else
                    {
                        res.Valid = true;
                        res.Data = null;
                        res.Message = "No existen Amplificadores";
                    }
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Valid = false;
                res.Data = null;
                res.Message = "La petición de Amplificadores Fallo" + ex.Message;
                return Ok();
            }
        }

        // POST: api/Amplifier
        [HttpPost]
        public IActionResult Post(string Name, int Bass, int Mid, int Treble)
        {
            Respon res = new Respon();

            try
            {
                using (var db = new MySqlConnection(Properties.Resources.strcon))
                {
                    var result = db.Execute("Insert into amplifiers (Name, Bass, Mid, Treble) Values(@Name, @Bass, @Mid, @Treble)", new { Name, Bass, Mid, Treble });

                    if (result > 0)
                    {
                        res.Valid = true;
                        res.Data = null;
                        res.Message = "El nuevo amplificador ha sido agregado con éxito";
                    }
                    else
                    {
                        res.Valid = false;
                        res.Data = null;
                        res.Message = "Ah ocurrido un error con al agregar el nuevo amplificador";
                    }
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Valid = false;
                res.Data = null;
                res.Message = "La inserción de un nuevo amplificador Fallo" + ex.Message;
                return Ok();
            }

        }
        /*
        public void DELETE_Amplifier(int ID)
        {
            using (var db = new SqlConnection(Properties.Resources.strcon))
            {
                var result = db.Execute("delete from Amplifiers where Id_Amplifier = @ID", new { ID });
            }
        }
         * */

        public AmplifierController(ILogger<AmplifierController> logger)
        {
            _logger = logger;
        }

    }
}