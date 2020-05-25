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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            Respon res = new Respon();
            try
            {
                using (var db = new MySqlConnection(Properties.Resources.strcon))
                {
                    IEnumerable<User> users = db.Query<User>("select Id_User, Username, Pass from users");

                    if (users.Count() > 0)
                    {
                        res.Valid = true;
                        res.Data = users;
                        res.Message = "Todos los usuarios";
                    }
                    else
                    {
                        res.Valid = true;
                        res.Data = null;
                        res.Message = "No existen Usuarios";
                    }

                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Valid = false;
                res.Data = null;
                res.Message = "La petición de usuarios Fallo" + ex.Message;

                return Ok(res);
            }
        }

        // GET: api/User/Find
        [HttpGet("Find")]
        public IActionResult Get(int id)
        {
            Respon res = new Respon();

            try
            {
                using (var db = new MySqlConnection(Properties.Resources.strcon))
                {
                    IEnumerable<User> users = db.Query<User>("select Id_User, Username, Pass from users where Id_User=@id", new { id });

                    if (users.Count() > 0)
                    {
                        res.Valid = true;
                        res.Data = users;
                        res.Message = "Usuario con ID: " + id.ToString();
                    }
                    else
                    {
                        res.Valid = true;
                        res.Data = null;
                        res.Message = "No existe ese usuario con esa ID";
                    }
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Valid = false;
                res.Data = null;
                res.Message = ex.Message;
                return Ok(res);
            }
        }

        //GET api/User/Login
        [HttpGet("Login")]
        public IActionResult Login(string Username, string Pass)
        {
            Respon res = new Respon();

            try
            {
                using (var db = new MySqlConnection(Properties.Resources.strcon))
                {
                    IEnumerable<User> users = db.Query<User>("select Id_User, Username, Pass from users where Username=@Username and Pass=@Pass", new { Username, Pass });

                    if (users.Count() > 0)
                    {
                        res.Valid = true;
                        res.Data = users;
                        res.Message = "El usuario inició Sesión";
                    }
                    else
                    {
                        res.Valid = true;
                        res.Data = null;
                        res.Message = "El usuario o la contraseña es incorrecta";
                    }
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Valid = false;
                res.Data = null;
                res.Message = "Ah ocurrido un error: " + ex.Message;
                return Ok(res);
            }
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post(string Username, string Pass)
        {
            Respon res = new Respon();

            if (!DuplicateUsername(Username))
            {
                try
                {
                    using (var db = new MySqlConnection(Properties.Resources.strcon))
                    {
                        var result = db.Execute("Insert into users (Username, Pass) Values(@Username, @Pass)", new { Username, Pass });

                        if (result > 0)
                        {
                            res.Valid = true;
                            res.Data = null;
                            res.Message = "El nuevo usuario se ha agregado con éxito";
                        }
                        else
                        {
                            res.Valid = false;
                            res.Data = null;
                            res.Message = "Ah ocurrido un error con al agregar el nuevo usuario";
                        }
                        return Ok(res);
                    }
                }
                catch (Exception ex)
                {
                    res.Valid = false;
                    res.Data = null;
                    res.Message = ex.Message;
                    return Ok(res);
                }
            }
            else
            {
                res.Valid = true;
                res.Data = null;
                res.Message = "Ya existe un usuario con ese nombre";

                return Ok(res);
            }
        }

        // DELETE: api/User
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Respon res = new Respon();
            try
            {
                using (var db = new MySqlConnection(Properties.Resources.strcon))
                {
                    var result = db.Execute("Delete from users where Id_User =@id", new { id });

                    if (result > 0)
                    {
                        res.Valid = true;
                        res.Data = null;
                        res.Message = "El usuario se ha eliminado con éxito";
                    }
                    else
                    {
                        res.Valid = false;
                        res.Data = null;
                        res.Message = "El usuario con ese ID ya fue eliminado o no existe";
                    }
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Valid = false;
                res.Data = null;
                res.Message = ex.Message;
            }

            return Ok(res);
        }

        // PUT: api/User
        [HttpPut]
        public IActionResult ChangePassword(int id, string Pass)
        {
            Respon res = new Respon();
            try
            {
                using (var db = new MySqlConnection(Properties.Resources.strcon))
                {
                    var result = db.Execute("update users set Pass=@Pass where Id_User=@id", new { id, Pass });

                    if (result > 0)
                    {
                        res.Valid = true;
                        res.Data = null;
                        res.Message = "El usuario ha cambiado de contraseña con éxito";
                    }
                    else
                    {
                        res.Valid = false;
                        res.Data = null;
                        res.Message = "El usuario no cambio de contraseña, ocurrio un error en la base de datos";
                    }
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                res.Valid = false;
                res.Data = null;
                res.Message = ex.Message;
            }

            return Ok(res);
        }



        public Respon FindByUsername(string Username)
        {
            Respon resp = new Respon();

            try
            {
                using (var db = new MySqlConnection(Properties.Resources.strcon))
                {
                    var result = db.Query<User>("select Username from users where Username = @Username", new { Username });

                    if (result.Count() > 0)
                    {
                        resp.Data = result;
                        resp.Valid = true;
                        resp.Message = "Se encontro un usuario con el nombre";
                    }
                    else
                    {
                        resp.Data = null;
                        resp.Valid = true;
                        resp.Message = "No existe un usuario con ese nombre";
                    }
                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp.Data = null;
                resp.Valid = false;
                resp.Message = ex.Message;

                return resp;
            }
        }

        public bool DuplicateUsername(string Username)
        {
            var usernames = FindByUsername(Username);
            if (usernames.Valid)
            {
                if (usernames.Data != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}