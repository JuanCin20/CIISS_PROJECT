using ENTITY___LAYER;
using BUSINESS___LAYER;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
QuestPDF.Settings.License = LicenseType.Community;

string Initial_Fecha_Movimiento_Inventario = "2024-01-01";
string Final_Fecha_Movimiento_Inventario = "2024-11-30";
int ID_Movimiento_Inventario = 0;

List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Transaction_Report(Initial_Fecha_Movimiento_Inventario, Final_Fecha_Movimiento_Inventario, ID_Movimiento_Inventario);

decimal Total_01 = 0;
decimal Total_02 = 0;

string Current_Day = DateTime.Now.Day.ToString();
string Current_Month = DateTime.Now.Month.ToString();
string Current_Year = DateTime.Now.Year.ToString();

string ID_Usuario_String = "53";
string Nombre_Apellido_Usuario_String = "Juan Carlos Aronés Peña";
string E_Mail_Usuario_String = "U22208295@utp.edu.pe";

// ? Code in Your Mind Method
Document.Create(Document =>
{
    Document.Page(Page =>
    {
        Page.Margin(10);

        Page.Header().ShowOnce().Row(Row =>
        {
            var Image_Ubication = Path.Combine("E:\\JuanCin20\\DATA\\CIISS - INVENTORY MANAGEMENT\\CIISS - INVENTORY MANAGEMENT\\PROJECT - LAYER\\wwwroot\\img", "Report_Image.png");

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
                    Columns.RelativeColumn();
                    Columns.RelativeColumn();
                    Columns.RelativeColumn();
                });

                Table.Header(Header =>
                {
                    Header.Cell().Background("#FF0000").Padding(2).Text("Tipo").FontColor("#FFFFFF");
                    Header.Cell().Background("#FF0000").Padding(2).Text("ID Usuario").FontColor("#FFFFFF");
                    Header.Cell().Background("#FF0000").Padding(2).Text("Categoría").FontColor("#FFFFFF");
                    Header.Cell().Background("#FF0000").Padding(2).Text("Proveedor").FontColor("#FFFFFF");
                    Header.Cell().Background("#FF0000").Padding(2).Text("Insumo").FontColor("#FFFFFF");
                    Header.Cell().Background("#FF0000").Padding(2).Text("Precio").FontColor("#FFFFFF");
                    Header.Cell().Background("#FF0000").Padding(2).Text("Cantidad").FontColor("#FFFFFF");
                    Header.Cell().Background("#FF0000").Padding(2).Text("Subtotal").FontColor("#FFFFFF");
                    Header.Cell().Background("#FF0000").Padding(2).Text("Fecha").FontColor("#FFFFFF");

                });

                foreach (var Value_01 in Obj_List_Class_Entity_Dashboard)
                {
                    if (Value_01.Tipo_Movimiento_Inventario == "Entrada")
                    {
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_01.Tipo_Movimiento_Inventario.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_01.ID_Usuario.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_01.Nombre_Categoria_Insumo_02.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_01.Nombre_Proveedor_Insumo_02.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_01.Nombre_Insumo_02.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"S/. {Value_01.Precio_Insumo_02}").FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_01.Cantidad_Movimiento_Inventario.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_01.Total_Transaction.ToString()).FontSize(10).FontColor("#FF0000");
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_01.Fecha_Movimiento_Inventario.ToString()).FontSize(10).FontColor("#094293");

                        Total_01 += Convert.ToDecimal(Value_01.Total_Transaction, new CultureInfo("es-PE"));
                    }
                }

                Column_01.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(Column =>
                {
                    Column.Item().Text("Consumo Generado: S/. " + Total_01).FontSize(15).FontColor("#FF0000");

                    Column.Spacing(5);
                });
            });

            Column_01.Spacing(10);

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
                    Columns.RelativeColumn();
                    Columns.RelativeColumn();
                    Columns.RelativeColumn();
                });

                Table.Header(Header =>
                {
                    Header.Cell().Background("#008000").Padding(2).Text("Tipo").FontColor("#FFFFFF");
                    Header.Cell().Background("#008000").Padding(2).Text("ID Usuario").FontColor("#FFFFFF");
                    Header.Cell().Background("#008000").Padding(2).Text("Categoría").FontColor("#FFFFFF");
                    Header.Cell().Background("#008000").Padding(2).Text("Proveedor").FontColor("#FFFFFF");
                    Header.Cell().Background("#008000").Padding(2).Text("Insumo").FontColor("#FFFFFF");
                    Header.Cell().Background("#008000").Padding(2).Text("Precio").FontColor("#FFFFFF");
                    Header.Cell().Background("#008000").Padding(2).Text("Cantidad").FontColor("#FFFFFF");
                    Header.Cell().Background("#008000").Padding(2).Text("Subtotal").FontColor("#FFFFFF");
                    Header.Cell().Background("#008000").Padding(2).Text("Fecha").FontColor("#FFFFFF");

                });

                foreach (var Value_02 in Obj_List_Class_Entity_Dashboard)
                {
                    if (Value_02.Tipo_Movimiento_Inventario == "Salida")
                    {
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Tipo_Movimiento_Inventario.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.ID_Usuario.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Nombre_Categoria_Insumo_02.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Nombre_Proveedor_Insumo_02.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Nombre_Insumo_02.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"S/. {Value_02.Precio_Insumo_02}").FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Cantidad_Movimiento_Inventario.ToString()).FontSize(10);
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Total_Transaction.ToString()).FontSize(10).FontColor("#008000");
                        Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Fecha_Movimiento_Inventario.ToString()).FontSize(10).FontColor("#094293");

                        Total_02 += Convert.ToDecimal(Value_02.Total_Transaction, new CultureInfo("es-PE"));
                    }
                }

                Column_01.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(Column =>
                {
                    Column.Item().Text("Ganancia Generada: S/. " + Total_02).FontSize(15).FontColor("#008000");

                    Column.Spacing(5);
                });
            });
        });

        Page.Footer().AlignRight().Text(Text =>
        {
            Text.Span("Pagina ").FontSize(10);
            Text.CurrentPageNumber().FontSize(10);
            Text.Span(" de ").FontSize(10);
            Text.TotalPages().FontSize(10);
        });
    });
}).ShowInCompanion();

/* var Obj_Process = new Process();
Obj_Process.StartInfo = new ProcessStartInfo(Path.Combine(Directory.GetCurrentDirectory(), File_Name))
{
    UseShellExecute = true
};
Obj_Process.Start(); */