using Microsoft.AspNetCore.Mvc;
using ENTITY___LAYER;
using BUSINESS___LAYER;
using PROJECT___LAYER.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Globalization;

namespace PROJECT___LAYER.Controllers
{
    public class HomeController : Controller
    {
        /* private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        } */

        private readonly IWebHostEnvironment _WebHostEnvironment;

        public HomeController(IWebHostEnvironment WebHostEnvironment)
        {
            _WebHostEnvironment = WebHostEnvironment;
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

        public IActionResult Report_01()
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Deadline_Report();

            decimal Total = Obj_List_Class_Entity_Dashboard.Sum(Value_01 => Convert.ToDecimal(Value_01.Precio_Insumo_01 * Value_01.Stock_Insumo, new CultureInfo("es-PE")));

            string Current_Day = DateTime.Now.Day.ToString();
            string Current_Month = DateTime.Now.Month.ToString();
            string Current_Year = DateTime.Now.Year.ToString();

            int Obj_List_Class_Entity_Dashboard_Size = Obj_List_Class_Entity_Dashboard.Count;

            string ID_Usuario_String = HttpContext.Session.GetString("ID_Usuario_String");
            string Nombre_Apellido_Usuario_String = HttpContext.Session.GetString("Nombre_Apellido_Usuario_String");
            string E_Mail_Usuario_String = HttpContext.Session.GetString("E_Mail_Usuario_String");

            var Document_Alternative = Document.Create(Document =>
            {
                Document.Page(Page =>
                {
                    Page.Margin(30);

                    Page.Header().ShowOnce().Row(Row =>
                    {
                        var Image_Ubication = Path.Combine(_WebHostEnvironment.WebRootPath, "img/Report_Image.png");

                        byte[] Image_Data = System.IO.File.ReadAllBytes(Image_Ubication);

                        Row.ConstantItem(100).Image(Image_Data);

                        Row.RelativeItem().Column(Column =>
                        {
                            Column.Item().AlignCenter().Text("Sistema de Inventario SAC").Bold().FontSize(14);
                            Column.Item().AlignCenter().Text("Av. Ruiseñores, Los Milanos 161 - Santa Anita").FontSize(9);
                            Column.Item().AlignCenter().Text("959 748 008 / 998 723 316").FontSize(9);
                            Column.Item().AlignCenter().Text("JuanCin080604@gmail.com").FontSize(9);
                        });

                        Row.RelativeItem().Column(Column =>
                        {
                            Column.Item().Border(1).BorderColor("#094293").AlignCenter().Text("R.U.C N° 20040608161");
                            Column.Item().Background("#094293").Border(1).BorderColor("#094293").AlignCenter().Text("QuestPDF Companion").FontColor("#FFFFFF");
                            Column.Item().Border(1).BorderColor("#094293").AlignCenter().Text(Current_Day + "/" + Current_Month + "/" + Current_Year);
                        });
                    });

                    Page.Content().PaddingVertical(10).Column(Column_01 =>
                    {
                        Column_01.Item().Column(Column_02 =>
                        {
                            Column_02.Item().Text("Datos del Usuario").Underline().Bold();
                            Column_02.Item().Text(Text =>
                            {
                                Text.Span("Identificador: ").SemiBold().FontSize(10);
                                Text.Span(ID_Usuario_String).FontSize(10);
                            });

                            Column_02.Item().Text(Text =>
                            {
                                Text.Span("Nombres y Apellidos: ").SemiBold().FontSize(10);
                                Text.Span(Nombre_Apellido_Usuario_String).FontSize(10);
                            });

                            Column_02.Item().Text(Text =>
                            {
                                Text.Span("Correo Electrónico: ").SemiBold().FontSize(10);
                                Text.Span(E_Mail_Usuario_String).FontSize(10);
                            });
                        });

                        Column_01.Item().LineHorizontal(0.5f);

                        Column_01.Item().Table(Table =>
                        {
                            Table.ColumnsDefinition(Columns =>
                            {
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                            });

                            Table.Header(Header =>
                            {
                                Header.Cell().Background("#094293").Padding(2).Text("ID").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Nombre").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Precio").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Stock").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Fecha de Vencimiento").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Días Restantes").FontColor("#FFFFFF");
                            });

                            foreach (var Value_02 in Obj_List_Class_Entity_Dashboard)
                            {
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.ID_Insumo.ToString()).FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Nombre_Insumo_01).FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"S/. {Value_02.Precio_Insumo_01}").FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Stock_Insumo.ToString()).FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Fecha_Vencimiento_Insumo).FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Deadline.ToString()).FontSize(10).FontColor("#094293");
                            }
                        });

                        Column_01.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(Column =>
                        {
                            Column.Item().Text("Pérdidas Generadas: S/. " + Total).FontSize(15).FontColor("#FF0000");
                            Column.Item().Text("Número de Insumos por Expirar: " + Obj_List_Class_Entity_Dashboard_Size).FontSize(15).FontColor("#FF0000");
                            Column.Item().Text("Recomendaciones: ").FontSize(14);
                            Column.Item().Text("- Realiza promociones como 'compra uno y lleva otro' para productos en riesgo.").FontSize(12);
                            Column.Item().Text("- Donar productos a organizaciones benéficas o bancos de alimentos si aún están dentro de su vida útil.").FontSize(12);
                            Column.Item().Text("- Busca asociaciones con grupos comunitarios que puedan beneficiarse.").FontSize(12);
                            Column.Spacing(5);
                        });

                        Column_01.Spacing(10);
                    });

                    Page.Footer().AlignRight().Text(Text =>
                    {
                        Text.Span("Pagina ").FontSize(10);
                        Text.CurrentPageNumber().FontSize(10);
                        Text.Span(" de ").FontSize(10);
                        Text.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();

            Stream Obj_Stream = new MemoryStream(Document_Alternative);

            return File(Obj_Stream, "application/pdf", "Report_01.pdf");
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

        public IActionResult Report_02()
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Chart_01();

            decimal Total = Obj_List_Class_Entity_Dashboard.Sum(Value_01 => Convert.ToDecimal(Value_01.Income_Sum, new CultureInfo("es-PE")));

            string Current_Day = DateTime.Now.Day.ToString();
            string Current_Month = DateTime.Now.Month.ToString();
            string Current_Year = DateTime.Now.Year.ToString();

            string ID_Usuario_String = HttpContext.Session.GetString("ID_Usuario_String");
            string Nombre_Apellido_Usuario_String = HttpContext.Session.GetString("Nombre_Apellido_Usuario_String");
            string E_Mail_Usuario_String = HttpContext.Session.GetString("E_Mail_Usuario_String");

            var Document_Alternative = Document.Create(Document =>
            {
                Document.Page(Page =>
                {
                    Page.Margin(30);

                    Page.Header().ShowOnce().Row(Row =>
                    {
                        var Image_Ubication = Path.Combine(_WebHostEnvironment.WebRootPath, "img/Report_Image.png");

                        byte[] Image_Data = System.IO.File.ReadAllBytes(Image_Ubication);

                        Row.ConstantItem(100).Image(Image_Data);

                        Row.RelativeItem().Column(Column =>
                        {
                            Column.Item().AlignCenter().Text("Sistema de Inventario SAC").Bold().FontSize(14);
                            Column.Item().AlignCenter().Text("Av. Ruiseñores, Los Milanos 161 - Santa Anita").FontSize(9);
                            Column.Item().AlignCenter().Text("959 748 008 / 998 723 316").FontSize(9);
                            Column.Item().AlignCenter().Text("JuanCin080604@gmail.com").FontSize(9);
                        });

                        Row.RelativeItem().Column(Column =>
                        {
                            Column.Item().Border(1).BorderColor("#094293").AlignCenter().Text("R.U.C N° 20040608161");
                            Column.Item().Background("#094293").Border(1).BorderColor("#094293").AlignCenter().Text("QuestPDF Companion").FontColor("#FFFFFF");
                            Column.Item().Border(1).BorderColor("#094293").AlignCenter().Text(Current_Day + "/" + Current_Month + "/" + Current_Year);
                        });
                    });

                    Page.Content().PaddingVertical(10).Column(Column_01 =>
                    {
                        Column_01.Item().Column(Column_02 =>
                        {
                            Column_02.Item().Text("Datos del Usuario").Underline().Bold();
                            Column_02.Item().Text(Text =>
                            {
                                Text.Span("Identificador: ").SemiBold().FontSize(10);
                                Text.Span(ID_Usuario_String).FontSize(10);
                            });

                            Column_02.Item().Text(Text =>
                            {
                                Text.Span("Nombres y Apellidos: ").SemiBold().FontSize(10);
                                Text.Span(Nombre_Apellido_Usuario_String).FontSize(10);
                            });

                            Column_02.Item().Text(Text =>
                            {
                                Text.Span("Correo Electrónico: ").SemiBold().FontSize(10);
                                Text.Span(E_Mail_Usuario_String).FontSize(10);
                            });
                        });

                        Column_01.Item().LineHorizontal(0.5f);

                        Column_01.Item().Table(Table =>
                        {
                            Table.ColumnsDefinition(Columns =>
                            {
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                            });

                            Table.Header(Header =>
                            {
                                Header.Cell().Background("#094293").Padding(2).Text("Año").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Mes").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Costo").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Número de Transacciones").FontColor("#FFFFFF");
                            });

                            foreach (var Value_02 in Obj_List_Class_Entity_Dashboard)
                            {
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Income_Year.ToString()).FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Income_Month_Name.ToString()).FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"S/. {Value_02.Income_Sum}").FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Income_Number.ToString()).FontSize(10).FontColor("#094293");
                            }
                        });

                        Column_01.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(Column =>
                        {
                            Column.Item().Text("Costos Generados: S/. " + Total).FontSize(15).FontColor("#FF0000");
                            Column.Spacing(5);
                        });

                        Column_01.Spacing(10);
                    });

                    Page.Footer().AlignRight().Text(Text =>
                    {
                        Text.Span("Pagina ").FontSize(10);
                        Text.CurrentPageNumber().FontSize(10);
                        Text.Span(" de ").FontSize(10);
                        Text.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();

            Stream Obj_Stream = new MemoryStream(Document_Alternative);

            return File(Obj_Stream, "application/pdf", "Report_02.pdf");
        }

        public IActionResult Report_03()
        {
            List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Chart_02();

            decimal Total = Obj_List_Class_Entity_Dashboard.Sum(Value_01 => Convert.ToDecimal(Value_01.Exit_Sum, new CultureInfo("es-PE")));

            string Current_Day = DateTime.Now.Day.ToString();
            string Current_Month = DateTime.Now.Month.ToString();
            string Current_Year = DateTime.Now.Year.ToString();

            string ID_Usuario_String = HttpContext.Session.GetString("ID_Usuario_String");
            string Nombre_Apellido_Usuario_String = HttpContext.Session.GetString("Nombre_Apellido_Usuario_String");
            string E_Mail_Usuario_String = HttpContext.Session.GetString("E_Mail_Usuario_String");

            var Document_Alternative = Document.Create(Document =>
            {
                Document.Page(Page =>
                {
                    Page.Margin(30);

                    Page.Header().ShowOnce().Row(Row =>
                    {
                        var Image_Ubication = Path.Combine(_WebHostEnvironment.WebRootPath, "img/Report_Image.png");

                        byte[] Image_Data = System.IO.File.ReadAllBytes(Image_Ubication);

                        Row.ConstantItem(100).Image(Image_Data);

                        Row.RelativeItem().Column(Column =>
                        {
                            Column.Item().AlignCenter().Text("Sistema de Inventario SAC").Bold().FontSize(14);
                            Column.Item().AlignCenter().Text("Av. Ruiseñores, Los Milanos 161 - Santa Anita").FontSize(9);
                            Column.Item().AlignCenter().Text("959 748 008 / 998 723 316").FontSize(9);
                            Column.Item().AlignCenter().Text("JuanCin080604@gmail.com").FontSize(9);
                        });

                        Row.RelativeItem().Column(Column =>
                        {
                            Column.Item().Border(1).BorderColor("#094293").AlignCenter().Text("R.U.C N° 20040608161");
                            Column.Item().Background("#094293").Border(1).BorderColor("#094293").AlignCenter().Text("QuestPDF Companion").FontColor("#FFFFFF");
                            Column.Item().Border(1).BorderColor("#094293").AlignCenter().Text(Current_Day + "/" + Current_Month + "/" + Current_Year);
                        });
                    });

                    Page.Content().PaddingVertical(10).Column(Column_01 =>
                    {
                        Column_01.Item().Column(Column_02 =>
                        {
                            Column_02.Item().Text("Datos del Usuario").Underline().Bold();
                            Column_02.Item().Text(Text =>
                            {
                                Text.Span("Identificador: ").SemiBold().FontSize(10);
                                Text.Span(ID_Usuario_String).FontSize(10);
                            });

                            Column_02.Item().Text(Text =>
                            {
                                Text.Span("Nombres y Apellidos: ").SemiBold().FontSize(10);
                                Text.Span(Nombre_Apellido_Usuario_String).FontSize(10);
                            });

                            Column_02.Item().Text(Text =>
                            {
                                Text.Span("Correo Electrónico: ").SemiBold().FontSize(10);
                                Text.Span(E_Mail_Usuario_String).FontSize(10);
                            });
                        });

                        Column_01.Item().LineHorizontal(0.5f);

                        Column_01.Item().Table(Table =>
                        {
                            Table.ColumnsDefinition(Columns =>
                            {
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                                Columns.RelativeColumn();
                            });

                            Table.Header(Header =>
                            {
                                Header.Cell().Background("#094293").Padding(2).Text("Año").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Mes").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Costo").FontColor("#FFFFFF");
                                Header.Cell().Background("#094293").Padding(2).Text("Número de Transacciones").FontColor("#FFFFFF");
                            });

                            foreach (var Value_02 in Obj_List_Class_Entity_Dashboard)
                            {
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Exit_Year.ToString()).FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Exit_Month_Name.ToString()).FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"S/. {Value_02.Exit_Sum}").FontSize(10);
                                Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Exit_Number.ToString()).FontSize(10).FontColor("#094293");
                            }
                        });

                        Column_01.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(Column =>
                        {
                            Column.Item().Text("Ganancias Generadas: S/. " + Total).FontSize(15).FontColor("#008000");
                            Column.Spacing(5);
                        });

                        Column_01.Spacing(10);
                    });

                    Page.Footer().AlignRight().Text(Text =>
                    {
                        Text.Span("Pagina ").FontSize(10);
                        Text.CurrentPageNumber().FontSize(10);
                        Text.Span(" de ").FontSize(10);
                        Text.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();

            Stream Obj_Stream = new MemoryStream(Document_Alternative);

            return File(Obj_Stream, "application/pdf", "Report_03.pdf");
        }

        /* public IActionResult Report_04(string Initial_Fecha_Movimiento_Inventario, string Final_Fecha_Movimiento_Inventario, int ID_Movimiento_Inventario)
        {

        } */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}