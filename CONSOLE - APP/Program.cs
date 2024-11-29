using ENTITY___LAYER;
using BUSINESS___LAYER;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
QuestPDF.Settings.License = LicenseType.Community;

List<Class_Entity_Insumo> Obj_List_Class_Entity_Insumo = new Class_Business_Insumo().Class_Business_Insumo_Listar(false);

List<Class_Entity_Categoria_Insumo> Obj_List_Class_Entity_Categoria_Insumo_01 = new Class_Business_Categoria_Insumo().Class_Business_Categoria_Insumo_Listar(false);

List<Class_Entity_Categoria_Insumo> Obj_List_Class_Entity_Categoria_Insumo_02 = new Class_Business_Categoria_Insumo().Class_Business_Categoria_Insumo_Listar(true);

decimal Total = Obj_List_Class_Entity_Insumo.Sum(Value_01 => Convert.ToDecimal(Value_01.Precio_Insumo * Value_01.Stock_Insumo, new CultureInfo("es-PE")));

string Current_Day = DateTime.Now.Day.ToString();
string Current_Month = DateTime.Now.Month.ToString();
string Current_Year = DateTime.Now.Year.ToString();

int Obj_List_Class_Entity_Insumo_Size = Obj_List_Class_Entity_Insumo.Count;

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
            Column_01.Item().Column(Column_02 =>
            {
                Column_02.Item().Text("Datos del Usuario").Underline().Bold();
                Column_02.Item().Text(Text =>
                {
                    Text.Span("ID: ").SemiBold().FontSize(10);
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
                    Columns.RelativeColumn();
                    Columns.RelativeColumn();
                });

                Table.Header(Header =>
                {
                    Header.Cell().Background("#094293").Padding(2).Text("ID").FontColor("#FFFFFF");
                    Header.Cell().Background("#094293").Padding(2).Text("Categoría").FontColor("#FFFFFF");
                    Header.Cell().Background("#094293").Padding(2).Text("Proveedor").FontColor("#FFFFFF");
                    Header.Cell().Background("#094293").Padding(2).Text("Nombre").FontColor("#FFFFFF");
                    Header.Cell().Background("#094293").Padding(2).Text("Precio").FontColor("#FFFFFF");
                    Header.Cell().Background("#094293").Padding(2).Text("Stock").FontColor("#FFFFFF");
                    Header.Cell().Background("#094293").Padding(2).Text("Fecha de Vencimiento").FontColor("#FFFFFF");
                    Header.Cell().Background("#094293").Padding(2).Text("Días Restantes").FontColor("#FFFFFF");

                });

                foreach (var Value_02 in Obj_List_Class_Entity_Insumo)
                {
                    Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.ID_Insumo.ToString()).FontSize(10);
                    Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Obj_Class_Entity_Categoria_Insumo.Nombre_Categoria_Insumo.ToString()).FontSize(10);
                    Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Obj_Class_Entity_Proveedor_Insumo.Nombre_Proveedor_Insumo.ToString()).FontSize(10);
                    Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Nombre_Insumo.ToString()).FontSize(10);
                    Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text($"S/. {Value_02.Precio_Insumo}").FontSize(10);
                    Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Stock_Insumo.ToString()).FontSize(10);
                    Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Fecha_Vencimiento_Insumo.ToString());
                    Table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(Value_02.Deadline.ToString()).FontSize(10).FontColor("#094293");
                }
            });

            Column_01.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(Column =>
            {
                Column.Item().Text("Pérdidas Generadas: S/. " + Total).FontSize(15).FontColor("#FF0000");
                Column.Item().Text("Número de Insumos Eliminados: " + Obj_List_Class_Entity_Insumo_Size).FontSize(15).FontColor("#FF0000");

                Column.Item().Text("Número de Insumos por Categorías: ").FontSize(15);

                foreach (var Value_03 in Obj_List_Class_Entity_Categoria_Insumo_01)
                {
                    Column.Item().Text("- " + Value_03.Nombre_Categoria_Insumo + ": " + Value_03.Supply_Number).FontSize(15).FontColor("#FF0000");
                }

                foreach (var Value_04 in Obj_List_Class_Entity_Categoria_Insumo_02)
                {
                    Column.Item().Text("- " + Value_04.Nombre_Categoria_Insumo + ": " + Value_04.Supply_Number).FontSize(15).FontColor("#008000 ");
                }

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
}).ShowInCompanion();

/* var Obj_Process = new Process();
Obj_Process.StartInfo = new ProcessStartInfo(Path.Combine(Directory.GetCurrentDirectory(), File_Name))
{
    UseShellExecute = true
};
Obj_Process.Start(); */