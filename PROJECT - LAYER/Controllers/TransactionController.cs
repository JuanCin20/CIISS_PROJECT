using Microsoft.AspNetCore.Mvc;
using ENTITY___LAYER;
using BUSINESS___LAYER;
using PROJECT___LAYER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PROJECT___LAYER.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Administrador, Empleado")]
        public IActionResult Middle()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Empleado")]
        public IActionResult Supply()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Transaction_Controller_Categoria_Insumo_Listar()
        {
            List<Class_Entity_Categoria_Insumo> Obj_List_Class_Entity_Categoria_Insumo = new Class_Business_Categoria_Insumo().Class_Business_Categoria_Insumo_Listar(true);

            return Json(new { obj_List_Class_Entity_Categoria_Insumo = Obj_List_Class_Entity_Categoria_Insumo });
        }

        [HttpPost]
        public JsonResult Transaction_Controller_Proveedor_Insumo_Listar_Alternative(int ID_Categoria_Insumo)
        {
            List<Class_Entity_Proveedor_Insumo> Obj_List_Class_Entity_Proveedor_Insumo = new Class_Business_Proveedor_Insumo().Class_Business_Proveedor_Insumo_Listar_Alternative(ID_Categoria_Insumo);

            return Json(new { obj_List_Class_Entity_Proveedor_Insumo = Obj_List_Class_Entity_Proveedor_Insumo });
        }

        [HttpPost]
        public JsonResult Transaction_Controller_Insumo_Listar(int ID_Categoria_Insumo, int ID_Proveedor_Insumo)
        {
            List<Class_Entity_Insumo> Obj_List_Class_Entity_Insumo = new List<Class_Entity_Insumo>();

            bool Conversion;

            Obj_List_Class_Entity_Insumo = new Class_Business_Insumo().Class_Business_Insumo_Listar(true).Select(Obj_List_Class_Entity_Insumo_Alternative => new Class_Entity_Insumo()
            {
                ID_Insumo = Obj_List_Class_Entity_Insumo_Alternative.ID_Insumo,
                Nombre_Insumo = Obj_List_Class_Entity_Insumo_Alternative.Nombre_Insumo,
                Descripcion_Insumo = Obj_List_Class_Entity_Insumo_Alternative.Descripcion_Insumo,
                Obj_Class_Entity_Categoria_Insumo = Obj_List_Class_Entity_Insumo_Alternative.Obj_Class_Entity_Categoria_Insumo,
                Obj_Class_Entity_Proveedor_Insumo = Obj_List_Class_Entity_Insumo_Alternative.Obj_Class_Entity_Proveedor_Insumo,
                Precio_Insumo = Obj_List_Class_Entity_Insumo_Alternative.Precio_Insumo,
                Stock_Insumo = Obj_List_Class_Entity_Insumo_Alternative.Stock_Insumo,
                Ruta_Imagen_Insumo = Obj_List_Class_Entity_Insumo_Alternative.Ruta_Imagen_Insumo,
                Base_64_Imagen_Insumo = Class_Business_Recurso.Convert_Base_64(Path.Combine(Obj_List_Class_Entity_Insumo_Alternative.Ruta_Imagen_Insumo, Obj_List_Class_Entity_Insumo_Alternative.Nombre_Imagen_Insumo), out Conversion),
                Extension_Imagen_Insumo = Path.GetExtension(Obj_List_Class_Entity_Insumo_Alternative.Nombre_Imagen_Insumo),
                Estado_Insumo = Obj_List_Class_Entity_Insumo_Alternative.Estado_Insumo
            }).Where(Obj_List_Class_Entity_Insumo_Alter =>
            Obj_List_Class_Entity_Insumo_Alter.Obj_Class_Entity_Categoria_Insumo.ID_Categoria_Insumo == (ID_Categoria_Insumo == 0 ? Obj_List_Class_Entity_Insumo_Alter.Obj_Class_Entity_Categoria_Insumo.ID_Categoria_Insumo : ID_Categoria_Insumo) &&
            Obj_List_Class_Entity_Insumo_Alter.Obj_Class_Entity_Proveedor_Insumo.ID_Proveedor_Insumo == (ID_Proveedor_Insumo == 0 ? Obj_List_Class_Entity_Insumo_Alter.Obj_Class_Entity_Proveedor_Insumo.ID_Proveedor_Insumo : ID_Proveedor_Insumo)).ToList();

            return Json(new { obj_List_Class_Entity_Insumo = Obj_List_Class_Entity_Insumo });
        }

        [HttpPost]
        public JsonResult Transaction_Controller_Middle_Listar(int ID_Insumo)
        {
            int ID_Usuario = Convert.ToInt32(HttpContext.Session.GetString("ID_Usuario_String"));

            bool Result_01 = new Class_Business_Middle().Class_Business_Middle_Listar(ID_Usuario, ID_Insumo);

            bool Result_02 = false;

            string Message = string.Empty;

            if (Result_01)
            {
                Message = "El Insumo ya se Ecuentra Agregado a Middle";
            }
            else
            {
                Result_02 = new Class_Business_Middle().Class_Business_Middle_Create_Update(ID_Usuario, ID_Insumo, true, out Message);
            }

            return Json(new { result_02 = Result_02, message = Message });
        }

        [HttpPost]
        public JsonResult Transaction_Controller_Middle_Listar_Alternative()
        {
            int ID_Usuario = Convert.ToInt32(HttpContext.Session.GetString("ID_Usuario_String"));

            List<Class_Entity_Middle> Obj_List_Class_Entity_Middle = new List<Class_Entity_Middle>();

            bool Conversion;

            Obj_List_Class_Entity_Middle = new Class_Business_Middle().Class_Business_Middle_Listar_Alternative(ID_Usuario).Select(Obj_List_Class_Entity_Middle_Alter => new Class_Entity_Middle()
            {
                Obj_Class_Entity_Insumo = new Class_Entity_Insumo()
                {
                    ID_Insumo = Obj_List_Class_Entity_Middle_Alter.Obj_Class_Entity_Insumo.ID_Insumo,
                    Nombre_Insumo = Obj_List_Class_Entity_Middle_Alter.Obj_Class_Entity_Insumo.Nombre_Insumo,
                    Obj_Class_Entity_Proveedor_Insumo = Obj_List_Class_Entity_Middle_Alter.Obj_Class_Entity_Insumo.Obj_Class_Entity_Proveedor_Insumo,
                    Precio_Insumo = Obj_List_Class_Entity_Middle_Alter.Obj_Class_Entity_Insumo.Precio_Insumo,
                    Ruta_Imagen_Insumo = Obj_List_Class_Entity_Middle_Alter.Obj_Class_Entity_Insumo.Ruta_Imagen_Insumo,
                    Base_64_Imagen_Insumo = Class_Business_Recurso.Convert_Base_64(Path.Combine(Obj_List_Class_Entity_Middle_Alter.Obj_Class_Entity_Insumo.Ruta_Imagen_Insumo, Obj_List_Class_Entity_Middle_Alter.Obj_Class_Entity_Insumo.Nombre_Imagen_Insumo), out Conversion),
                    Extension_Imagen_Insumo = Path.GetExtension(Obj_List_Class_Entity_Middle_Alter.Obj_Class_Entity_Insumo.Nombre_Imagen_Insumo)
                },
                Cantidad_Insumo_Middle = Obj_List_Class_Entity_Middle_Alter.Cantidad_Insumo_Middle
            }).ToList();

            return Json(new { obj_List_Class_Entity_Middle = Obj_List_Class_Entity_Middle });
        }

        [HttpPost]
        public JsonResult Transaction_Controller_Middle_Create_Update(int ID_Insumo, bool Boolean_Operation)
        {
            int ID_Usuario = Convert.ToInt32(HttpContext.Session.GetString("ID_Usuario_String"));

            bool Result = false;

            string Message = string.Empty;

            Result = new Class_Business_Middle().Class_Business_Middle_Create_Update(ID_Usuario, ID_Insumo, Boolean_Operation, out Message);

            return Json(new { result = Result, message = Message });
        }

        [HttpPost]
        public JsonResult Transaction_Controller_Middle_Delete(int ID_Insumo)
        {
            int ID_Usuario = Convert.ToInt32(HttpContext.Session.GetString("ID_Usuario_String"));

            bool Result = false;

            string Message = string.Empty;

            Result = new Class_Business_Middle().Class_Business_Middle_Delete(ID_Usuario, ID_Insumo);

            return Json(new { result = Result, message = Message });
        }

        [HttpPost]
        public JsonResult Transaction_Controller_Middle_Count()
        {
            int ID_Usuario = Convert.ToInt32(HttpContext.Session.GetString("ID_Usuario_String"));

            int Result = new Class_Business_Middle().Class_Business_Middle_Count(ID_Usuario);

            return Json(new { result = Result });
        }

        [HttpPost]
        public JsonResult Transaction_Controller_Venta_Registrar(Class_Entity_Movimiento_Inventario Obj_Class_Entity_Movimiento_Inventario, List<Class_Entity_Middle> Obj_List_Class_Entity_Middle)
        {
            bool Result = false;
            string Message = string.Empty;

            Class_Entity_Movimiento_Inventario Obj_Class_Entity_Movimiento_Inventario_Alternative = new Class_Entity_Movimiento_Inventario();

            Obj_Class_Entity_Movimiento_Inventario_Alternative = new Class_Entity_Movimiento_Inventario()
            {
                Obj_Class_Entity_Usuario = new Class_Entity_Usuario()
                {
                    ID_Usuario = Convert.ToInt32(HttpContext.Session.GetString("ID_Usuario_String"))
                },
                Tipo_Movimiento_Inventario = Obj_Class_Entity_Movimiento_Inventario.Tipo_Movimiento_Inventario,
                Cantidad_Insumo_Movimiento_Inventario = Obj_Class_Entity_Movimiento_Inventario.Cantidad_Insumo_Movimiento_Inventario,
                Restaurante_Movimiento_Inventario = Obj_Class_Entity_Movimiento_Inventario.Restaurante_Movimiento_Inventario,
                Telefono_Movimiento_Inventario = Obj_Class_Entity_Movimiento_Inventario.Telefono_Movimiento_Inventario,
                Direccion_Movimiento_Inventario = Obj_Class_Entity_Movimiento_Inventario.Direccion_Movimiento_Inventario,
                Obj_Class_Entity_Distrito = new Class_Entity_Distrito()
                {
                    ID_Distrito = Obj_Class_Entity_Movimiento_Inventario.Obj_Class_Entity_Distrito.ID_Distrito
                },
            };

            decimal Monto_Total_Movimiento_Inventario = 0;

            DataTable Obj_DataTable = new DataTable();
            Obj_DataTable.Locale = new System.Globalization.CultureInfo("es-PE");
            Obj_DataTable.Columns.Add("ID_Insumo", typeof(string));
            Obj_DataTable.Columns.Add("Cantidad_Insumo_Detalle_Movimiento_Inventario", typeof(int));
            Obj_DataTable.Columns.Add("Monto_Total_Detalle_Movimiento_Inventario", typeof(decimal));

            foreach (Class_Entity_Middle Obj_Class_Entity_Middle in Obj_List_Class_Entity_Middle)
            {
                decimal Monto_Total_Detalle_Movimiento_Inventario = Convert.ToDecimal(Obj_Class_Entity_Middle.Cantidad_Insumo_Middle.ToString()) * Obj_Class_Entity_Middle.Obj_Class_Entity_Insumo.Precio_Insumo;

                Monto_Total_Movimiento_Inventario += Monto_Total_Detalle_Movimiento_Inventario;

                Obj_DataTable.Rows.Add(new object[]
                {
                    Obj_Class_Entity_Middle.Obj_Class_Entity_Insumo.ID_Insumo,
                    Obj_Class_Entity_Middle.Cantidad_Insumo_Middle,
                    Monto_Total_Detalle_Movimiento_Inventario,
                });
            }

            Obj_Class_Entity_Movimiento_Inventario_Alternative.Monto_Total_Movimiento_Inventario = Monto_Total_Movimiento_Inventario;

            Result = new Class_Business_Movimiento_Inventario().Class_Business_Venta_Registrar(Obj_Class_Entity_Movimiento_Inventario_Alternative, Obj_DataTable, out Message);

            return Json(new { result = Result, message = Message });
        }

        [HttpGet]
        public JsonResult Transaction_Controller_Ubication_Departamento_Listar()
        {
            List<Class_Entity_Departamento> Obj_List_Class_Entity_Departamento = new Class_Business_Ubication().Class_Business_Ubication_Departamento_Listar();

            return Json(new { obj_List_Class_Entity_Departamento = Obj_List_Class_Entity_Departamento });
        }

        [HttpPost]
        public JsonResult Transaction_Controller_Ubication_Provincia_Listar(int ID_Departamento)
        {
            List<Class_Entity_Provincia> Obj_List_Class_Entity_Provincia = new Class_Business_Ubication().Class_Business_Ubication_Provincia_Listar(ID_Departamento);

            return Json(new { obj_List_Class_Entity_Provincia = Obj_List_Class_Entity_Provincia });
        }

        [HttpPost]
        public JsonResult Transaction_Controller_Ubication_Distrito_Listar(int ID_Provincia, int ID_Departamento)
        {
            List<Class_Entity_Distrito> Obj_List_Class_Entity_Distrito = new Class_Business_Ubication().Class_Business_Ubication_Distrito_Listar(ID_Provincia, ID_Departamento);

            return Json(new { obj_List_Class_Entity_Distrito = Obj_List_Class_Entity_Distrito });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}