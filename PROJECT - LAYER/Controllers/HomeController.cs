using Microsoft.AspNetCore.Mvc;
using ENTITY___LAYER;
using BUSINESS___LAYER;
using PROJECT___LAYER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ClosedXML.Excel;

namespace PROJECT___LAYER.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "Administrador, Empleado")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Empleado")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Home_Controller_Dashboard_Tip_Report()
        {
            Class_Entity_Dashboard Obj_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Tip_Report();

            return Json(new { obj_Class_Entity_Dashboard = Obj_Class_Entity_Dashboard });
        }

        [HttpGet]
        public JsonResult Home_Controller_Dashboard_Chart_01()
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Chart_01();

            return Json(new { obj_List_Class_Entity_Dashboard = Obj_List_Class_Entity_Dashboard });
        }

        [HttpGet]
        public JsonResult Home_Controller_Dashboard_Chart_02()
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Chart_02();

            return Json(new { obj_List_Class_Entity_Dashboard = Obj_List_Class_Entity_Dashboard });
        }

        [HttpGet]
        public JsonResult Home_Controller_Dashboard_Chart_03()
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Chart_03();

            return Json(new { obj_List_Class_Entity_Dashboard = Obj_List_Class_Entity_Dashboard });
        }

        [HttpGet]
        public JsonResult Home_Controller_Dashboard_Chart_04()
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Chart_04();

            return Json(new { obj_List_Class_Entity_Dashboard = Obj_List_Class_Entity_Dashboard });
        }

        [HttpGet]
        public JsonResult Home_Controller_Dashboard_Chart_05()
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Chart_05();

            return Json(new { obj_List_Class_Entity_Dashboard = Obj_List_Class_Entity_Dashboard });
        }

        [HttpGet]
        public JsonResult Home_Controller_Dashboard_Chart_06()
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Chart_06();

            return Json(new { obj_List_Class_Entity_Dashboard = Obj_List_Class_Entity_Dashboard });
        }

        [HttpGet]
        public JsonResult Home_Controller_Dashboard_Deadline_Report()
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Deadline_Report();

            return Json(new { data = Obj_List_Class_Entity_Dashboard });
        }

        [HttpPost]
        public JsonResult Home_Controller_Dashboard_Deadline_Report_Delete(int ID_Insumo)
        {
            bool Result = false;

            Result = new Class_Business_Dashboard().Class_Business_Dashboard_Deadline_Report_Delete(ID_Insumo);

            return Json(new { result = Result });
        }

        [HttpPost]
        public JsonResult Home_Controller_Dashboard_Transaction_Report(string Initial_Fecha_Movimiento_Inventario, string Final_Fecha_Movimiento_Inventario, int ID_Movimiento_Inventario)
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Transaction_Report(Initial_Fecha_Movimiento_Inventario, Final_Fecha_Movimiento_Inventario, ID_Movimiento_Inventario);

            return Json(new { data = Obj_List_Class_Entity_Dashboard });
        }

        [HttpPost]
        public FileResult Home_Controller_Dashboard_Transaction_Report_Export(string Initial_Fecha_Movimiento_Inventario, string Final_Fecha_Movimiento_Inventario, int ID_Movimiento_Inventario)
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new List<Class_Entity_Dashboard>();
            Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Transaction_Report(Initial_Fecha_Movimiento_Inventario, Final_Fecha_Movimiento_Inventario, ID_Movimiento_Inventario);
            DataTable Obj_DataTable = new DataTable();

            Obj_DataTable.Locale = new System.Globalization.CultureInfo("es-PE");
            Obj_DataTable.Columns.Add("ID_Movimiento_Inventario", typeof(int));
            Obj_DataTable.Columns.Add("Tipo_Movimiento_Inventario", typeof(string));
            Obj_DataTable.Columns.Add("Nombre_Categoria_Insumo", typeof(string));
            Obj_DataTable.Columns.Add("Nombre_Proveedor_Insumo", typeof(string));
            Obj_DataTable.Columns.Add("Nombre_Insumo", typeof(string));
            Obj_DataTable.Columns.Add("Descripcion_Insumo", typeof(string));
            Obj_DataTable.Columns.Add("Precio_Insumo", typeof(decimal));
            Obj_DataTable.Columns.Add("Cantidad_Movimiento_Inventario", typeof(int));
            Obj_DataTable.Columns.Add("Total_Transaction", typeof(decimal));
            Obj_DataTable.Columns.Add("Fecha_Movimiento_Inventario", typeof(string));
            Obj_DataTable.Columns.Add("Usuario_Transaction", typeof(string));

            foreach (Class_Entity_Dashboard Obj_Class_Entity_Dashboard in Obj_List_Class_Entity_Dashboard)
            {
                Obj_DataTable.Rows.Add(new object[] {
                Obj_Class_Entity_Dashboard.ID_Movimiento_Inventario,
                Obj_Class_Entity_Dashboard.Tipo_Movimiento_Inventario,
                Obj_Class_Entity_Dashboard.Nombre_Categoria_Insumo_02,
                Obj_Class_Entity_Dashboard.Nombre_Proveedor_Insumo_02,
                Obj_Class_Entity_Dashboard.Nombre_Insumo_02,
                Obj_Class_Entity_Dashboard.Descripcion_Insumo_02,
                Obj_Class_Entity_Dashboard.Precio_Insumo_02,
                Obj_Class_Entity_Dashboard.Cantidad_Movimiento_Inventario,
                Obj_Class_Entity_Dashboard.Total_Transaction,
                Obj_Class_Entity_Dashboard.Fecha_Movimiento_Inventario,
                Obj_Class_Entity_Dashboard.Usuario_Transaction
            });
            }

            Obj_DataTable.TableName = "Dashboard_Transaction_Report";

            using (XLWorkbook Obj_XLWorkbook = new XLWorkbook())
            {
                Obj_XLWorkbook.Worksheets.Add(Obj_DataTable);
                using (MemoryStream Obj_MemoryStream = new MemoryStream())
                {
                    Obj_XLWorkbook.SaveAs(Obj_MemoryStream);
                    return File(Obj_MemoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Dashboard_Transaction_Report - " + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}