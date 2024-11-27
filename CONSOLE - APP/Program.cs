using ENTITY___LAYER;
using BUSINESS___LAYER;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;
QuestPDF.Settings.License = LicenseType.Community;

List<Class_Entity_Dashboard> Obj_List_Class_Entity_Dashboard = new Class_Business_Dashboard().Class_Business_Dashboard_Chart_02();

decimal Total = Obj_List_Class_Entity_Dashboard.Sum(Value_01 => Convert.ToDecimal(Value_01.Exit_Sum, new CultureInfo("es-PE")));

string Current_Day = DateTime.Now.Day.ToString();
string Current_Month = DateTime.Now.Month.ToString();
string Current_Year = DateTime.Now.Year.ToString();

int Obj_List_Class_Entity_Dashboard_Size = Obj_List_Class_Entity_Dashboard.Count;

string ID_Usuario_String = "53";
string Nombre_Apellido_Usuario_String = "Juan Carlos Aronés Peña";
string E_Mail_Usuario_String = "U22208295@utp.edu.pe";

// ? Code in Your Mind Method
Document.Create(Document =>
{
    Document.Page(Page =>
    {
        Page.Margin(30);

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
}).ShowInCompanion();

/* var Obj_Process = new Process();
Obj_Process.StartInfo = new ProcessStartInfo(Path.Combine(Directory.GetCurrentDirectory(), File_Name))
{
    UseShellExecute = true
};
Obj_Process.Start(); */