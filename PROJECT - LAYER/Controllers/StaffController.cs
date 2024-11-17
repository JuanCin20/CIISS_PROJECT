using Microsoft.AspNetCore.Mvc;
using ENTITY___LAYER;
using BUSINESS___LAYER;
using PROJECT___LAYER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace PROJECT___LAYER.Controllers
{
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult User()
        {
            return View();
        }

        //+++++++++++++++++++++++Http_Request_Methods_Usuario+++++++++++++++++++++++//
        #region Http_Request_Methods_Usuario
        [HttpGet]
        public JsonResult Staff_Controller_Usuario_Listar()
        {
            List<Class_Entity_Usuario> Obj_List_Class_Entity_Usuario = new Class_Business_Usuario().Class_Business_Usuario_Listar();

            return Json(new { data = Obj_List_Class_Entity_Usuario });
        }

        [HttpGet]
        public JsonResult Staff_Controller_Usuario_Listar_Alternative(bool Estado_Usuario)
        {
            List<Class_Entity_Usuario> Obj_List_Class_Entity_Usuario = new Class_Business_Usuario().Class_Business_Usuario_Listar_Alternative(Estado_Usuario);

            return Json(new { data = Obj_List_Class_Entity_Usuario });
        }

        [HttpPost]
        public JsonResult Staff_Controller_Usuario_Registrar(string Obj_Class_Entity_Usuario, IFormFile Obj_IFormFile)
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

        [HttpPost]
        public JsonResult Staff_Controller_Usuario_Editar(string Obj_Class_Entity_Usuario, IFormFile Obj_IFormFile)
        {
            string Message = string.Empty;
            bool Successful_Operation = true;
            bool Successful_Save_Image = true;

            Class_Entity_Usuario Obj_Class_Entity_Usuario_Alternative = new Class_Entity_Usuario();
            Obj_Class_Entity_Usuario_Alternative = JsonConvert.DeserializeObject<Class_Entity_Usuario>(Obj_Class_Entity_Usuario);

            Successful_Operation = new Class_Business_Usuario().Class_Business_Usuario_Editar(Obj_Class_Entity_Usuario_Alternative, out Message);

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
            }
            return Json(new { successful_Operation = Successful_Operation, message = Message });
        }

        [HttpPost]
        public JsonResult Staff_Controller_Usuario_Imagen(int ID_Usuario)
        {
            bool Conversion = false;
            Class_Entity_Usuario Obj_Class_Entity_Usuario = new Class_Business_Usuario().Class_Business_Usuario_Listar().Where(Obj_Class_Entity_Usuario_Alternative => Obj_Class_Entity_Usuario_Alternative.ID_Usuario == ID_Usuario).FirstOrDefault();
            string Base_64_Imagen_Usuario = Class_Business_Recurso.Convert_Base_64(Path.Combine(Obj_Class_Entity_Usuario.Ruta_Imagen_Usuario, Obj_Class_Entity_Usuario.Nombre_Imagen_Usuario), out Conversion);
            return Json(new { conversion = Conversion, base_64_Imagen_Usuario = Base_64_Imagen_Usuario, extension_Imagen_Usuario = Path.GetExtension(Obj_Class_Entity_Usuario.Nombre_Imagen_Usuario) });
        }

        [HttpPost]
        public JsonResult Staff_Controller_Usuario_Reset(int ID_Usuario)
        {
            bool Result = false;
            string Message = string.Empty;

            Result = new Class_Business_Usuario().Class_Business_Usuario_Reset(ID_Usuario, out Message);

            return Json(new { result = Result, message = Message });
        }

        [HttpPost]
        public JsonResult Staff_Controller_Usuario_Eliminar(int ID_Usuario)
        {
            bool Result = false;
            string Message = string.Empty;

            Result = new Class_Business_Usuario().Class_Business_Usuario_Eliminar(ID_Usuario, out Message);

            return Json(new { result = Result, message = Message });
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}