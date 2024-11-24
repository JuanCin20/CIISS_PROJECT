using Microsoft.AspNetCore.Mvc;
using ENTITY___LAYER;
using BUSINESS___LAYER;
using PROJECT___LAYER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace PROJECT___LAYER.Controllers
{
    public class AccessController : Controller
    {
        private readonly ILogger<AccessController> _logger;

        public AccessController(ILogger<AccessController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "True")]
        public IActionResult Change_Password()
        {
            return View();
        }

        public IActionResult Log_In()
        {
            return View();
        }

        public IActionResult Reset_Password()
        {
            return View();
        }

        public IActionResult Sign_Up()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Access_Controller_Change_Password(string Password_Usuario, string Password_Usuario_01, string Password_Usuario_02)
        {
            int ID_Usuario = Convert.ToInt32(HttpContext.Session.GetString("ID_Usuario_String"));
            Class_Entity_Usuario Obj_Class_Entity_Usuario = new Class_Entity_Usuario();

            Obj_Class_Entity_Usuario = new Class_Business_Usuario().Class_Business_Usuario_Listar().Where(Obj_Class_Entity_Usuario_Alter => Obj_Class_Entity_Usuario_Alter.ID_Usuario == ID_Usuario).FirstOrDefault();

            if (Obj_Class_Entity_Usuario.Password_Usuario != Class_Business_Recurso.Convert_SHA_256(Password_Usuario))
            {
                ViewBag.Error = "La Contraseña Actual es Incorrecta";
                return View("Change_Password", "Access");
            }

            Password_Usuario_01 = Class_Business_Recurso.Convert_SHA_256(Password_Usuario_01);

            string Message = string.Empty;
            bool Result = new Class_Business_Usuario().Class_Business_Usuario_Change_Password(ID_Usuario, Password_Usuario_01, out Message);

            if (Result)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                ViewBag.Error = null;
                return RedirectToAction("Log_In", "Access");
            }
            else
            {
                ViewBag.Error = Message;
                return View("Change_Password", "Access");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Access_Controller_Log_In(string E_Mail_Usuario, string Password_Usuario)
        {
            Class_Entity_Usuario Obj_Class_Entity_Usuario = new Class_Entity_Usuario();

            Obj_Class_Entity_Usuario = new Class_Business_Usuario().Class_Business_Usuario_Listar().Where(Obj_Class_Entity_Usuario_Alter => Obj_Class_Entity_Usuario_Alter.E_Mail_Usuario == E_Mail_Usuario && Obj_Class_Entity_Usuario_Alter.Password_Usuario == Class_Business_Recurso.Convert_SHA_256(Password_Usuario)).FirstOrDefault();

            if (Obj_Class_Entity_Usuario == null)
            {
                ViewBag.Error = "Verifique sus Credenciales";
                return View("Log_In", "Access");
            }
            else
            {
                if (Obj_Class_Entity_Usuario.Reestablecer_Password_Usuario)
                {
                    var Claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, Obj_Class_Entity_Usuario.Nombre_Usuario),
                        new Claim("E_Mail_Usuario", Obj_Class_Entity_Usuario.E_Mail_Usuario)
                    };

                    string Reestablecer_Password_Usuario = Obj_Class_Entity_Usuario.Reestablecer_Password_Usuario.ToString();

                    Claims.Add(new Claim(ClaimTypes.Role, Reestablecer_Password_Usuario));

                    var Claims_Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Claims_Identity));

                    string ID_Usuario_String = "ID_Usuario_String";

                    HttpContext.Session.SetString(ID_Usuario_String, Obj_Class_Entity_Usuario.ID_Usuario.ToString());

                    ViewBag.Error = null;
                    return RedirectToAction("Change_Password", "Access");
                }
                else
                {
                    if (Obj_Class_Entity_Usuario.Obj_Class_Entity_Tipo_Usuario.ID_Tipo_Usuario == 1)
                    {
                        var Claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, Obj_Class_Entity_Usuario.Nombre_Usuario),
                            new Claim("E_Mail_Usuario", Obj_Class_Entity_Usuario.E_Mail_Usuario)
                        };

                        foreach (string Obj_Class_Entity_Tipo_Usuario in Obj_Class_Entity_Usuario.Obj_Class_Entity_Tipo_Usuario.Nombre_Tipo_Usuario.Split(","))
                        {
                            Claims.Add(new Claim(ClaimTypes.Role, Obj_Class_Entity_Tipo_Usuario));
                        }

                        var Claims_Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Claims_Identity));

                        string ID_Usuario_String = "ID_Usuario_String";
                        string ID_Tipo_Usuario_String = "ID_Tipo_Usuario_String";
                        string Nombre_Apellido_Usuario_String = "Nombre_Apellido_Usuario_String";
                        string E_Mail_Usuario_String = "E_Mail_Usuario_String";
                        string Imagen_Usuario_String = "Imagen_Usuario_String";

                        HttpContext.Session.SetString(ID_Usuario_String, Obj_Class_Entity_Usuario.ID_Usuario.ToString());
                        HttpContext.Session.SetString(ID_Tipo_Usuario_String, Obj_Class_Entity_Usuario.Obj_Class_Entity_Tipo_Usuario.ID_Tipo_Usuario.ToString());
                        HttpContext.Session.SetString(Nombre_Apellido_Usuario_String, Obj_Class_Entity_Usuario.Nombre_Usuario + " " + Obj_Class_Entity_Usuario.Apellido_Usuario);
                        HttpContext.Session.SetString(E_Mail_Usuario_String, Obj_Class_Entity_Usuario.E_Mail_Usuario);
                        HttpContext.Session.SetString(Imagen_Usuario_String, Obj_Class_Entity_Usuario.Nombre_Imagen_Usuario);

                        ViewBag.Error = null;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var Claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, Obj_Class_Entity_Usuario.Nombre_Usuario),
                            new Claim("E_Mail_Usuario", Obj_Class_Entity_Usuario.E_Mail_Usuario)
                        };

                        foreach (string Obj_Class_Entity_Tipo_Usuario in Obj_Class_Entity_Usuario.Obj_Class_Entity_Tipo_Usuario.Nombre_Tipo_Usuario.Split(","))
                        {
                            Claims.Add(new Claim(ClaimTypes.Role, Obj_Class_Entity_Tipo_Usuario));
                        }

                        var Claims_Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Claims_Identity));

                        string ID_Usuario_String = "ID_Usuario_String";
                        string ID_Tipo_Usuario_String = "ID_Tipo_Usuario_String";
                        string Nombre_Apellido_Usuario_String = "Nombre_Apellido_Usuario_String";
                        string E_Mail_Usuario_String = "E_Mail_Usuario_String";
                        string Imagen_Usuario_String = "Imagen_Usuario_String";

                        HttpContext.Session.SetString(ID_Usuario_String, Obj_Class_Entity_Usuario.ID_Usuario.ToString());
                        HttpContext.Session.SetString(ID_Tipo_Usuario_String, Obj_Class_Entity_Usuario.Obj_Class_Entity_Tipo_Usuario.ID_Tipo_Usuario.ToString());
                        HttpContext.Session.SetString(Nombre_Apellido_Usuario_String, Obj_Class_Entity_Usuario.Nombre_Usuario + " " + Obj_Class_Entity_Usuario.Apellido_Usuario);
                        HttpContext.Session.SetString(E_Mail_Usuario_String, Obj_Class_Entity_Usuario.E_Mail_Usuario);
                        HttpContext.Session.SetString(Imagen_Usuario_String, Obj_Class_Entity_Usuario.Nombre_Imagen_Usuario);

                        ViewBag.Error = null;
                        return RedirectToAction("Supply", "Management");
                    }
                }
            }
        }

        [HttpPost]
        public JsonResult Access_Controller_Reset_Password(string E_Mail_Usuario)
        {
            string Message = string.Empty;
            Class_Entity_Usuario Obj_Class_Entity_Usuario = new Class_Entity_Usuario();

            Obj_Class_Entity_Usuario = new Class_Business_Usuario().Class_Business_Usuario_Listar().Where(Obj_Class_Entity_Usuario_Alter => Obj_Class_Entity_Usuario_Alter.E_Mail_Usuario == E_Mail_Usuario).FirstOrDefault();

            if (Obj_Class_Entity_Usuario == null)
            {
                Message = "No se Encontró a un Usuario Relacionado con este Correo";
                return Json(new { message = Message });
            }

            bool Result = new Class_Business_Usuario().Class_Business_Usuario_Reset_Password(Obj_Class_Entity_Usuario.ID_Usuario, E_Mail_Usuario, out Message);

            if (Result)
            {
                Message = "Success!";
                return Json(new { message = Message });
            }
            else
            {
                Message = "Fail!";
                return Json(new { message = Message });
            }
        }

        [HttpPost]
        public JsonResult Access_Controller_Sign_Up(string Obj_Class_Entity_Usuario, IFormFile Obj_IFormFile)
        {
            string Message = string.Empty;
            bool Successful_Operation = true;
            bool Successful_Save_Image = true;

            Class_Entity_Usuario Obj_Class_Entity_Usuario_Alternative = new Class_Entity_Usuario();
            Obj_Class_Entity_Usuario_Alternative = JsonConvert.DeserializeObject<Class_Entity_Usuario>(Obj_Class_Entity_Usuario);

            int ID_Auto_Generated = new Class_Business_Usuario().Class_Business_Usuario_Registrar(Obj_Class_Entity_Usuario_Alternative, out Message);

            if (ID_Auto_Generated != 0)
            {
                Obj_Class_Entity_Usuario_Alternative.ID_Usuario = ID_Auto_Generated;
            }
            else
            {
                Successful_Operation = false;
            }

            if (Successful_Operation)
            {
                if (Obj_IFormFile != null)
                {
                    string Ruta_Imagen_Usuario = "E:\\JuanCin20\\DATA\\CIISS - INVENTORY MANAGEMENT\\CIISS - INVENTORY MANAGEMENT\\PROJECT - LAYER\\wwwroot\\User_Images";
                    string Image_Extension = Path.GetExtension(Obj_IFormFile.FileName);
                    string Nombre_Imagen_Usuario = string.Concat(Obj_Class_Entity_Usuario_Alternative.ID_Usuario.ToString(), Image_Extension);

                    try
                    {
                        string FileNameWithPath = Path.Combine(Ruta_Imagen_Usuario, Nombre_Imagen_Usuario);
                        using (var Stream = new FileStream(FileNameWithPath, FileMode.Create))
                        {
                            Obj_IFormFile.CopyTo(Stream);
                        }
                    }
                    catch (Exception Error)
                    {
                        Console.WriteLine(Error.Message);
                        Message = Error.Message;
                        Successful_Save_Image = false;
                    }

                    if (Successful_Save_Image)
                    {
                        Obj_Class_Entity_Usuario_Alternative.Ruta_Imagen_Usuario = Ruta_Imagen_Usuario;
                        Obj_Class_Entity_Usuario_Alternative.Nombre_Imagen_Usuario = Nombre_Imagen_Usuario;
                        bool Result = new Class_Business_Usuario().Class_Business_Usuario_Save_Image(Obj_Class_Entity_Usuario_Alternative, out Message);
                    }
                    else
                    {
                        Message = "Error: Ruta_Imagen_Usuario && Error: Nombre_Imagen_Usuario";
                    }
                }
                else
                {
                    if (Obj_IFormFile == null)
                    {
                        string Ruta_Imagen_Usuario = "E:\\JuanCin20\\DATA\\CIISS - INVENTORY MANAGEMENT\\CIISS - INVENTORY MANAGEMENT\\PROJECT - LAYER\\wwwroot\\User_Images";
                        string Nombre_Imagen_Usuario = "Image_Error.jpg";
                        Obj_Class_Entity_Usuario_Alternative.Ruta_Imagen_Usuario = Ruta_Imagen_Usuario;
                        Obj_Class_Entity_Usuario_Alternative.Nombre_Imagen_Usuario = Nombre_Imagen_Usuario;
                        bool Result = new Class_Business_Usuario().Class_Business_Usuario_Save_Image(Obj_Class_Entity_Usuario_Alternative, out Message);
                    }
                }
            }
            return Json(new { successful_Operation = Successful_Operation, iD_Auto_Generated = ID_Auto_Generated, message = Message });
        }

        public async Task<IActionResult> Access_Controller_Log_Out()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Log_In", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}