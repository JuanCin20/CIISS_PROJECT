using Microsoft.AspNetCore.Mvc;
using ENTITY___LAYER;
using BUSINESS___LAYER;
using PROJECT___LAYER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Globalization;

namespace PROJECT___LAYER.Controllers
{
    public class ManagementController : Controller
    {
        private readonly ILogger<ManagementController> _logger;

        public ManagementController(ILogger<ManagementController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Supplier()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Category()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Empleado")]
        public IActionResult Supply()
        {
            return View();
        }

        //+++++++++++++++++++++++Http_Request_Methods_Proveedor_Insumo+++++++++++++++++++++++//
        #region Http_Request_Methods_Proveedor_Insumo
        [HttpGet]
        public JsonResult Management_Controller_Proveedor_Insumo_Listar(bool Estado_Proveedor_Insumo)
        {
            List<Class_Entity_Proveedor_Insumo> Obj_List_Class_Entity_Proveedor_Insumo = new Class_Business_Proveedor_Insumo().Class_Business_Proveedor_Insumo_Listar(Estado_Proveedor_Insumo);

            return Json(new { data = Obj_List_Class_Entity_Proveedor_Insumo });
        }

        [HttpPost]
        public JsonResult Management_Controller_Proveedor_Insumo_Registrar(Class_Entity_Proveedor_Insumo Obj_Class_Entity_Proveedor_Insumo)
        {
            int ID_Auto_Generated = 0;
            string Message = string.Empty;

            ID_Auto_Generated = new Class_Business_Proveedor_Insumo().Class_Business_Proveedor_Insumo_Registrar(Obj_Class_Entity_Proveedor_Insumo, out Message);

            return Json(new { iD_Auto_Generated = ID_Auto_Generated, message = Message });
        }

        [HttpPost]
        public JsonResult Management_Controller_Proveedor_Insumo_Editar(Class_Entity_Proveedor_Insumo Obj_Class_Entity_Proveedor_Insumo)
        {
            bool Result = false;
            string Message = string.Empty;

            Result = new Class_Business_Proveedor_Insumo().Class_Business_Proveedor_Insumo_Editar(Obj_Class_Entity_Proveedor_Insumo, out Message);

            return Json(new { result = Result, message = Message });
        }
        #endregion

        //+++++++++++++++++++++++Http_Request_Methods_Categoria_Insumo+++++++++++++++++++++++//
        #region Http_Request_Methods_Categoria_Insumo
        [HttpGet]
        public JsonResult Management_Controller_Categoria_Insumo_Listar(bool Estado_Categoria_Insumo)
        {
            List<Class_Entity_Categoria_Insumo> Obj_List_Class_Entity_Categoria_Insumo = new Class_Business_Categoria_Insumo().Class_Business_Categoria_Insumo_Listar(Estado_Categoria_Insumo);

            return Json(new { data = Obj_List_Class_Entity_Categoria_Insumo });
        }

        [HttpPost]
        public JsonResult Management_Controller_Categoria_Insumo_Registrar(Class_Entity_Categoria_Insumo Obj_Class_Entity_Categoria_Insumo)
        {
            int ID_Auto_Generated = 0;
            string Message = string.Empty;

            ID_Auto_Generated = new Class_Business_Categoria_Insumo().Class_Business_Categoria_Insumo_Registrar(Obj_Class_Entity_Categoria_Insumo, out Message);

            return Json(new { iD_Auto_Generated = ID_Auto_Generated, message = Message });
        }

        [HttpPost]
        public JsonResult Management_Controller_Categoria_Insumo_Editar(Class_Entity_Categoria_Insumo Obj_Class_Entity_Categoria_Insumo)
        {
            bool Result = false;
            string Message = string.Empty;

            Result = new Class_Business_Categoria_Insumo().Class_Business_Categoria_Insumo_Editar(Obj_Class_Entity_Categoria_Insumo, out Message);

            return Json(new { result = Result, message = Message });
        }
        #endregion

        //+++++++++++++++++++++++Http_Request_Methods_Insumo+++++++++++++++++++++++//
        #region Http_Request_Methods_Insumo
        [HttpGet]
        public JsonResult Management_Controller_Insumo_Listar(bool Estado_Insumo)
        {
            List<Class_Entity_Insumo> Obj_List_Class_Entity_Insumo = new Class_Business_Insumo().Class_Business_Insumo_Listar(Estado_Insumo);

            return Json(new { data = Obj_List_Class_Entity_Insumo });
        }

        [HttpGet]
        public JsonResult Management_Controller_Insumo_Categoria_Insumo_Listar()
        {
            List<Class_Entity_Categoria_Insumo> Obj_List_Class_Entity_Categoria_Insumo = new Class_Business_Insumo().Class_Business_Insumo_Categoria_Insumo_Listar();

            return Json(new { data = Obj_List_Class_Entity_Categoria_Insumo });
        }

        [HttpGet]
        public JsonResult Management_Controller_Insumo_Proveedor_Insumo_Listar()
        {
            List<Class_Entity_Proveedor_Insumo> Obj_List_Class_Entity_Proveedor_Insumo = new Class_Business_Insumo().Class_Business_Insumo_Proveedor_Insumo_Listar();

            return Json(new { data = Obj_List_Class_Entity_Proveedor_Insumo });
        }

        [HttpPost]
        public JsonResult Management_Controller_Insumo_Registrar(string Obj_Class_Entity_Insumo, IFormFile Obj_IFormFile)
        {
            string Message = string.Empty;
            bool Successful_Operation = true;
            bool Successful_Save_Image = true;

            Class_Entity_Insumo Obj_Class_Entity_Insumo_Alternative = new Class_Entity_Insumo();
            Obj_Class_Entity_Insumo_Alternative = JsonConvert.DeserializeObject<Class_Entity_Insumo>(Obj_Class_Entity_Insumo);

            decimal Precio_Insumo;

            if (decimal.TryParse(Obj_Class_Entity_Insumo_Alternative.Precio_Insumo_String, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out Precio_Insumo))
            {
                Obj_Class_Entity_Insumo_Alternative.Precio_Insumo = Precio_Insumo;
            }
            else
            {
                return Json(new { successful_Operation = false, message = "Error: Precio_Insumo" });
            }

            int ID_Auto_Generated = new Class_Business_Insumo().Class_Business_Insumo_Registrar(Obj_Class_Entity_Insumo_Alternative, out Message);

            if (ID_Auto_Generated != 0)
            {
                Obj_Class_Entity_Insumo_Alternative.ID_Insumo = ID_Auto_Generated;
            }
            else
            {
                Successful_Operation = false;
            }

            if (Successful_Operation)
            {
                if (Obj_IFormFile != null)
                {
                    string Ruta_Imagen_Insumo = "E:\\JuanCin20\\DATA\\CIISS - INVENTORY MANAGEMENT\\CIISS - INVENTORY MANAGEMENT\\PROJECT - LAYER\\wwwroot\\Supply_Images";
                    string Image_Extension = Path.GetExtension(Obj_IFormFile.FileName);
                    string Nombre_Imagen_Insumo = string.Concat(Obj_Class_Entity_Insumo_Alternative.ID_Insumo.ToString(), Image_Extension);

                    try
                    {
                        string FileNameWithPath = Path.Combine(Ruta_Imagen_Insumo, Nombre_Imagen_Insumo);
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
                        Obj_Class_Entity_Insumo_Alternative.Ruta_Imagen_Insumo = Ruta_Imagen_Insumo;
                        Obj_Class_Entity_Insumo_Alternative.Nombre_Imagen_Insumo = Nombre_Imagen_Insumo;
                        bool Result = new Class_Business_Insumo().Class_Business_Insumo_Save_Image(Obj_Class_Entity_Insumo_Alternative, out Message);
                    }
                    else
                    {
                        Message = "Error: Ruta_Imagen_Insumo && Error: Nombre_Imagen_Insumo";
                    }
                }
                else
                {
                    if (Obj_IFormFile == null)
                    {
                        string Ruta_Imagen_Insumo = "E:\\JuanCin20\\DATA\\CIISS - INVENTORY MANAGEMENT\\CIISS - INVENTORY MANAGEMENT\\PROJECT - LAYER\\wwwroot\\Supply_Images";
                        string Nombre_Imagen_Insumo = "Image_Error.jpg";
                        Obj_Class_Entity_Insumo_Alternative.Ruta_Imagen_Insumo = Ruta_Imagen_Insumo;
                        Obj_Class_Entity_Insumo_Alternative.Nombre_Imagen_Insumo = Nombre_Imagen_Insumo;
                        bool Result = new Class_Business_Insumo().Class_Business_Insumo_Save_Image(Obj_Class_Entity_Insumo_Alternative, out Message);
                    }
                }
            }
            return Json(new { successful_Operation = Successful_Operation, iD_Auto_Generated = ID_Auto_Generated, message = Message });
        }

        [HttpPost]
        public JsonResult Management_Controller_Insumo_Editar(string Obj_Class_Entity_Insumo, IFormFile Obj_IFormFile)
        {
            string Message = string.Empty;
            bool Successful_Operation = true;
            bool Successful_Save_Image = true;

            Class_Entity_Insumo Obj_Class_Entity_Insumo_Alternative = new Class_Entity_Insumo();
            Obj_Class_Entity_Insumo_Alternative = JsonConvert.DeserializeObject<Class_Entity_Insumo>(Obj_Class_Entity_Insumo);

            decimal Precio_Insumo;

            if (decimal.TryParse(Obj_Class_Entity_Insumo_Alternative.Precio_Insumo_String, NumberStyles.AllowDecimalPoint, new CultureInfo("es-PE"), out Precio_Insumo))
            {
                Obj_Class_Entity_Insumo_Alternative.Precio_Insumo = Precio_Insumo;
            }
            else
            {
                return Json(new { successful_Operation = false, message = "Error: Precio_Insumo" });
            }

            Successful_Operation = new Class_Business_Insumo().Class_Business_Insumo_Editar(Obj_Class_Entity_Insumo_Alternative, out Message);

            if (Successful_Operation)
            {
                if (Obj_IFormFile != null)
                {
                    string Ruta_Imagen_Insumo = "E:\\JuanCin20\\DATA\\CIISS - INVENTORY MANAGEMENT\\CIISS - INVENTORY MANAGEMENT\\PROJECT - LAYER\\wwwroot\\Supply_Images";
                    string Image_Extension = Path.GetExtension(Obj_IFormFile.FileName);
                    string Nombre_Imagen_Insumo = string.Concat(Obj_Class_Entity_Insumo_Alternative.ID_Insumo.ToString(), Image_Extension);

                    try
                    {
                        string FileNameWithPath = Path.Combine(Ruta_Imagen_Insumo, Nombre_Imagen_Insumo);
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
                        Obj_Class_Entity_Insumo_Alternative.Ruta_Imagen_Insumo = Ruta_Imagen_Insumo;
                        Obj_Class_Entity_Insumo_Alternative.Nombre_Imagen_Insumo = Nombre_Imagen_Insumo;
                        bool Result = new Class_Business_Insumo().Class_Business_Insumo_Save_Image(Obj_Class_Entity_Insumo_Alternative, out Message);
                    }
                    else
                    {
                        Message = "Error: Ruta_Imagen_Insumo && Error: Nombre_Imagen_Insumo";
                    }
                }
            }
            return Json(new { successful_Operation = Successful_Operation, message = Message });
        }

        [HttpPost]
        public JsonResult Management_Controller_Insumo_Imagen(int ID_Insumo)
        {
            bool Conversion = false;
            Class_Entity_Insumo Obj_Class_Entity_Insumo = new Class_Business_Insumo().Class_Business_Insumo_Listar(true).Where(Obj_Class_Entity_Insumo_Alter => Obj_Class_Entity_Insumo_Alter.ID_Insumo == ID_Insumo).FirstOrDefault();
            string Base_64_Imagen_Insumo = Class_Business_Recurso.Convert_Base_64(Path.Combine(Obj_Class_Entity_Insumo.Ruta_Imagen_Insumo, Obj_Class_Entity_Insumo.Nombre_Imagen_Insumo), out Conversion);
            return Json(new { conversion = Conversion, base_64_Imagen_Insumo = Base_64_Imagen_Insumo, extension_Imagen_Insumo = Path.GetExtension(Obj_Class_Entity_Insumo.Nombre_Imagen_Insumo) });
        }

        [HttpPost]
        public JsonResult Management_Controller_Insumo_Reset(int ID_Insumo, string Fecha_Vencimiento_Insumo)
        {
            bool Result = false;
            string Message = string.Empty;

            Result = new Class_Business_Insumo().Class_Business_Insumo_Reset(ID_Insumo, Fecha_Vencimiento_Insumo, out Message);

            return Json(new { result = Result, message = Message });
        }

        [HttpPost]
        public JsonResult Management_Controller_Insumo_Eliminar(int ID_Insumo)
        {
            bool Result = false;
            string Message = string.Empty;

            Result = new Class_Business_Insumo().Class_Business_Insumo_Eliminar(ID_Insumo, out Message);

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