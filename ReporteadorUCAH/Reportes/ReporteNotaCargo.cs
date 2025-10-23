using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ReporteadorUCAH.Modelos;

public class ReporteNotaCargo : IDocument
{
    private readonly NotaCargo _notaCargo;

    public ReporteNotaCargo(NotaCargo notaCargo)
    {
        _notaCargo = notaCargo;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.Letter);
            page.Margin(0.5f, Unit.Centimetre);

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Column(column =>
        {
            // Logo e información de la empresa - Centrado
            column.Item().PaddingVertical(-3).AlignCenter().Column(headerColumn =>
            {
                // Logo desde directorio de ejecución
                var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UCAH-LOGO-trans-2.png");

                if (File.Exists(imagePath))
                {
                    headerColumn.Item().AlignCenter().Width(180).Height(60).Image(imagePath, ImageScaling.FitArea);
                }
                else
                {
                    // Placeholder si no encuentra la imagen
                    headerColumn.Item().AlignCenter().Width(180).Height(80).Background(Colors.Grey.Lighten2)
                        .AlignCenter().AlignMiddle().Text("LOGO UCAH").FontColor(Colors.Grey.Medium);
                }

                // Nombre de la empresa - CENTRADO
                headerColumn.Item().AlignCenter().Text("UNION DE CREDITO AGRICOLA DE HUATABAMPO, SA DE CV")
                    .Bold().FontSize(10);

                // RFC - CENTRADO
                headerColumn.Item().AlignCenter().Text("UCA-540914-TL6")
                    .Bold().FontSize(10);

                // Dirección - CENTRADO
                headerColumn.Item().AlignCenter().Text("AGUSTIN DE ITURBIDE SIN NUMERO COL.CENTRO HUATABAMPO, SONORA C.P. 85900")
                    .Bold().FontSize(9);
            });

            column.Item().PaddingTop(10).PaddingBottom(5).Row(row =>
            {
                row.RelativeItem(1.8f); // 18% espacio izquierdo
                row.RelativeItem(6.4f).LineHorizontal(2).LineColor(Colors.Black); // 64% línea
                row.RelativeItem(1.8f); // 18% espacio derecho
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingVertical(20).Column(column =>
        {
            // Información del documento y FINIQUITO en el mismo renglón
            column.Item().Row(row =>
            {
                // FINIQUITO centrado + información del cliente (lado izquierdo)
                row.RelativeItem().Column(sectionColumn =>
                {
                    // FINIQUITO centrado
                    sectionColumn.Item().AlignCenter().PaddingTop(-0.5f, Unit.Centimetre).Text("FINIQUITO").Bold().FontSize(20);

                    // Table de información del cliente (a la izquierda)
                    sectionColumn.Item().PaddingVertical(10).PaddingLeft(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(80); // Ancho fijo para etiquetas
                            columns.RelativeColumn();    // El resto del ancho para valores
                        });

                        // Fila 1: Nombre
                        table.Cell().PaddingVertical(3).AlignRight().Text("Nombre:").Bold();
                        table.Cell().PaddingVertical(3).AlignLeft().Text(_notaCargo._Cliente?.Nombre ?? "N/A");

                        // Fila 2: RFC
                        table.Cell().PaddingVertical(3).AlignRight().Text("RFC:").Bold();
                        table.Cell().PaddingVertical(3).AlignLeft().Text(_notaCargo._Cliente?.Rfc ?? "N/A");

                        // Fila 3: Dirección
                        table.Cell().PaddingVertical(3).AlignRight().Text("Dirección:").Bold();
                        table.Cell().PaddingVertical(3).AlignLeft().Text(_notaCargo._Cliente?.Calle ?? "N/A");
                    });
                });

                // Información del documento (lado derecho)
                row.RelativeItem().AlignRight().PaddingRight(1, Unit.Centimetre).Column(docColumn =>
                {
                    docColumn.Item().PaddingTop(-0.5f, Unit.Centimetre).AlignRight().Text($"{_notaCargo.Fecha:dd/MM/yyyy}").FontSize(11).Underline();
                    docColumn.Item().AlignRight().Text($"Nota #{_notaCargo.Id}").FontSize(12);
                    //docColumn.Item().AlignRight().Text($"Cosecha:{DateTime.MinValue.ToShortDateString()}-{DateTime.MaxValue.ToShortDateString()}").FontSize(9);
                    docColumn.Item().AlignRight().Text($"Cosecha:{_notaCargo._Cosecha.FechaInicial.ToShortDateString()}-{_notaCargo._Cosecha.FechaFinal.ToShortDateString()}").FontSize(9);
                });
            });

            column.Item().PaddingTop(-10).PaddingBottom(5).Row(row =>
            {
                row.RelativeItem(0.5f); // 5% espacio izquierdo
                row.RelativeItem(90f).LineHorizontal(0.5f).LineColor(Colors.Black); // 9% línea
                row.RelativeItem(0.5f); // 5% espacio derecho
            });
            // Articulo
            column.Item().Section("Articulo").Column(sectionColumn =>
            {
                sectionColumn.Item().Text("ARTICULO").Bold().FontSize(14);

                sectionColumn.Item().PaddingVertical(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Border(1).BorderColor(Colors.Black).Background(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text("CULTIVO").Bold();
                        header.Cell().Border(1).BorderColor(Colors.Black).Background(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text("TONELADAS").Bold();
                        header.Cell().Border(1).BorderColor(Colors.Black).Background(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text("PRECIO").Bold();
                        header.Cell().Border(1).BorderColor(Colors.Black).Background(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text("IMPORTE").Bold();
                    });

                    // Datos
                    table.Cell().Border(1).BorderColor(Colors.Black).Padding(5).Text(_notaCargo._Cultivo?.CultivoTipo ?? "N/A");
                    table.Cell().Border(1).BorderColor(Colors.Black).Padding(5).AlignRight().Text(_notaCargo.Tons.ToString("F2"));
                    table.Cell().Border(1).BorderColor(Colors.Black).Padding(5).AlignRight().Text(_notaCargo.Precio.ToString("C2"));
                    table.Cell().Border(1).BorderColor(Colors.Black).Padding(5).AlignRight().Text(_notaCargo.Importe.ToString("C2"));
                });
            });
            // Deducciones
            if (_notaCargo.Deducciones?.Any() == true)
            {
                column.Item().Section("Deducciones").Column(sectionColumn =>
                {
                    sectionColumn.Item().Text("DEDUCCIONES APLICADAS").Bold().FontSize(14);

                    var deducciones = _notaCargo.Deducciones.ToList();
                    var columnas = 2; // Forzamos 2 columnas para 14 deducciones (7 y 7)

                    sectionColumn.Item().PaddingVertical(5).Row(row =>
                    {
                        var itemsPorColumna = (int)Math.Ceiling(deducciones.Count / (double)columnas);

                        for (int i = 0; i < columnas; i++)
                        {
                            if (i > 0) row.ConstantItem(15); // Espacio entre columnas

                            row.RelativeItem().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2.5f);
                                    columns.RelativeColumn(1.5f);
                                });

                                // MOSTRAR HEADER EN TODAS LAS COLUMNAS
                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("DEDUCCIÓN").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("IMPORTE").Bold();
                                });

                                var itemsColumna = deducciones.Skip(i * itemsPorColumna).Take(itemsPorColumna);

                                foreach (var deduccion in itemsColumna)
                                {
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(2).Text(deduccion._Deduccion.Nombre);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(2).AlignRight().Text(deduccion.Importe.ToString("N2"));
                                }
                            });
                        }
                    });
                });
            }

            // Resumen
            column.Item().Section("Resumen").Column(sectionColumn =>
            {
                sectionColumn.Item().PaddingVertical(10).Background(Colors.Grey.Lighten1).Padding(15).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(6); // Columna para etiquetas
                        columns.RelativeColumn(1.5f); // Columna para cantidades
                    });

                    var totalDeducciones = _notaCargo.Deducciones?.Sum(d => d.Importe) ?? 0;
                    var netoAPagar = _notaCargo.Importe - totalDeducciones;

                    // Fila 1: Importe Bruto
                    table.Cell().AlignRight().Text("Importe Bruto:");
                    table.Cell().AlignRight().Text(_notaCargo.Importe.ToString("C2"));

                    // Fila 2: Total Deducciones
                    table.Cell().AlignRight().Text("Total Deducciones:");
                    // Para texto rojo 
                    table.Cell().AlignRight().Text(text => {
                        text.Span("-"+totalDeducciones.ToString("C2")).FontColor(Colors.Red.Medium);
                    });

                    // Fila 3: Neto a Pagar
                    table.Cell().AlignRight().Text("Neto a Pagar:").Bold();
                    table.Cell().AlignRight().Text(netoAPagar.ToString("C2")).Bold().FontSize(16);
                });
            });
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.AlignCenter().Text(text =>
        {
            text.Span("UUID Factura: ").FontSize(8);
            if (string.IsNullOrEmpty(_notaCargo.FacturaUUID))
                text.Span("11111111-2222-3333-4444-555555555555").Bold().FontSize(8);
            else
                text.Span(_notaCargo.FacturaUUID).Bold().FontSize(8);

                text.EmptyLine();
            text.Span("Documento generado - ").FontSize(8);
            text.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).FontSize(8);
        });
    }
}