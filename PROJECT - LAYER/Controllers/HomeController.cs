using Microsoft.AspNetCore.Mvc;
using ENTITY___LAYER;
using BUSINESS___LAYER;
using PROJECT___LAYER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace PROJECT___LAYER.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _host;

        public HomeController(IWebHostEnvironment host)
        {
            _host = host;
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Empleado")]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Download_PDF()
        {
            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);

                    page.Header().ShowOnce().Row(row =>
                    {
                        var rutaImagen = Path.Combine(_host.WebRootPath, "img/Report_Image.png");

                        byte[] imageData = System.IO.File.ReadAllBytes(rutaImagen);

                        // ? row.ConstantItem(140).Height(60).Placeholder();
                        row.ConstantItem(150).Image(imageData);

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("Sistema de Inventario SAC").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("Av. Ruise�ores, Los Milanos 161 - Santa Anita").FontSize(9);
                            col.Item().AlignCenter().Text("959 748 008 / 998 723 316").FontSize(9);
                            col.Item().AlignCenter().Text("jucarpe0806@gmail.com").FontSize(9);
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Border(1).BorderColor("#257272").AlignCenter().Text("RUC - 21312312312");
                            col.Item().Background("#257272").Border(1).BorderColor("#257272").AlignCenter().Text("QuestPDF Companion").FontColor("#FFFFFF");
                            col.Item().Border(1).BorderColor("#257272").AlignCenter().Text("N� 00001");
                        });
                    });

                    page.Content().PaddingVertical(10).Column(col_01 =>
                    {
                        col_01.Item().Column(col_02 =>
                        {
                            col_02.Item().Text("Datos del Usuario").Underline().Bold();
                            col_02.Item().Text(txt =>
                            {
                                txt.Span("Identificador: ").SemiBold().FontSize(10);
                                txt.Span("1").FontSize(10);
                            });

                            col_02.Item().Text(txt =>
                            {
                                txt.Span("Nombres y Apellidos: ").SemiBold().FontSize(10);
                                txt.Span("Juan Carlos Aron�s Pe�a").FontSize(10);
                            });

                            col_02.Item().Text(txt =>
                            {
                                txt.Span("Correo Electr�nico: ").SemiBold().FontSize(10);
                                txt.Span("JuanCin080604@gmail.com").FontSize(10);
                            });
                        });

                        col_01.Item().LineHorizontal(0.5f);

                        col_01.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272").Padding(2).Text("Producto").FontColor("#FFFFFF");
                                header.Cell().Background("#257272").Padding(2).Text("Precio").FontColor("#FFFFFF");
                                header.Cell().Background("#257272").Padding(2).Text("Cantidad").FontColor("#FFFFFF");
                                header.Cell().Background("#257272").Padding(2).Text("Total").FontColor("#FFFFFF");
                            });

                            foreach (var item in Enumerable.Range(1, 45))
                            {
                                var cantidad = Placeholders.Random.Next(1, 10);
                                var precio = Placeholders.Random.Next(5, 15);
                                var total = cantidad * precio;

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Placeholders.Label()).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(cantidad.ToString()).FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"S/. {precio}").FontSize(10);
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).AlignRight().Text($"S/. {total}").FontSize(10);
                            }
                        });
                        col_01.Item().AlignRight().Text("Total: S/. 1500").FontSize(12);

                        if (1 == 1)
                        {
                            col_01.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
                            {
                                column.Item().Text("Comentarios: ").FontSize(14);
                                column.Item().Text(Placeholders.LoremIpsum());
                                column.Spacing(5);
                            });
                        }

                        col_01.Spacing(10);
                    });

                    page.Footer().AlignRight().Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();

            Stream Obj_Stream = new MemoryStream(data);

            return File(Obj_Stream, "application/pdf", "Report_Test.pdf");
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