using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var File_Name = "Test_Report.pdf";

// ? Code in Your Mind Method
Document.Create(document =>
{
    document.Page(page =>
    {
        page.Margin(30);

        page.Header().ShowOnce().Row(row =>
        {
            row.ConstantItem(140).Height(60).Placeholder();

            row.RelativeItem().Column(col =>
            {
                col.Item().AlignCenter().Text("Sistema de Inventario SAC").Bold().FontSize(14);
                col.Item().AlignCenter().Text("Av. Ruiseñores, Los Milanos 161 - Santa Anita").FontSize(9);
                col.Item().AlignCenter().Text("959 748 008 / 998 723 316").FontSize(9);
                col.Item().AlignCenter().Text("jucarpe0806@gmail.com").FontSize(9);
            });

            row.RelativeItem().Column(col =>
            {
                col.Item().Border(1).BorderColor("#257272").AlignCenter().Text("RUC - 21312312312");
                col.Item().Background("#257272").Border(1).BorderColor("#257272").AlignCenter().Text("QuestPDF Companion").FontColor("#FFFFFF");
                col.Item().Border(1).BorderColor("#257272").AlignCenter().Text("N° 00001");
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
                    txt.Span("Juan Carlos Aronés Peña").FontSize(10);
                });

                col_02.Item().Text(txt =>
                {
                    txt.Span("Correo Electrónico: ").SemiBold().FontSize(10);
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
}).ShowInCompanion();

/* var Obj_Process = new Process();
Obj_Process.StartInfo = new ProcessStartInfo(Path.Combine(Directory.GetCurrentDirectory(), File_Name))
{
    UseShellExecute = true
};
Obj_Process.Start(); */