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
                    headerColumn.Item().AlignCenter().PaddingBottom(-10).Width(180).Height(80).Image(imagePath, ImageScaling.FitArea);
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
            // Información del documento - CENTRADO
            column.Item().AlignRight().PaddingRight(2, Unit.Centimetre).Column(docColumn =>
            {
                docColumn.Item().AlignRight().Text($"{_notaCargo.Fecha:dd/MM/yyyy}").FontSize(11);
                docColumn.Item().AlignRight().Text($"Nota #{_notaCargo.Id}").FontSize(11);
            });

            // FINIQUITO - CORREGIDO
            column.Item().Section("InformacionCliente").Column(sectionColumn =>
            {
                sectionColumn.Item().Text("FINIQUITO").Bold().FontSize(14);

                // Reemplazar Grid obsoleto por Table
                sectionColumn.Item().PaddingVertical(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(); // Columna para etiquetas
                        columns.RelativeColumn(); // Columna para valores
                    });

                    // Fila 1: Nombre
                    table.Cell().PaddingVertical(3).AlignRight().Text("Nombre:");
                    table.Cell().PaddingVertical(3).AlignLeft().Text(_notaCargo._Cliente?.Nombre ?? "N/A");

                    // Fila 2: RFC
                    table.Cell().PaddingVertical(3).AlignRight().Text("RFC:");
                    table.Cell().PaddingVertical(3).AlignLeft().Text(_notaCargo._Cliente?.Rfc ?? "N/A");

                    // Fila 3: Dirección
                    table.Cell().PaddingVertical(3).AlignRight().Text("Dirección:");
                    table.Cell().PaddingVertical(3).AlignLeft().Text(_notaCargo._Cliente?.Calle ?? "N/A");
                });
            });

            // Información del Producto - CORREGIDO
            column.Item().Section("InformacionProducto").Column(sectionColumn =>
            {
                sectionColumn.Item().Text("INFORMACIÓN DEL PRODUCTO").Bold().FontSize(14);

                // Reemplazar Grid obsoleto por Table
                sectionColumn.Item().PaddingVertical(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(); // Cultivo
                        columns.RelativeColumn(); // Variedad
                        columns.RelativeColumn(); // Cosecha
                    });

                    table.Cell().PaddingVertical(3).Text($"Cultivo: {_notaCargo._Cultivo?.Nombre ?? "N/A"}");
                    table.Cell().PaddingVertical(3).Text($"Variedad: {_notaCargo._Cultivo?.CultivoTipo ?? "N/A"}");
                    table.Cell().PaddingVertical(3).Text($"Cosecha: {_notaCargo._Cosecha?.FechaInicial:dd/MM/yyyy} - {_notaCargo._Cosecha?.FechaFinal:dd/MM/yyyy}");
                });
            });

            // Detalles de la Transacción - CORREGIDO
            column.Item().Section("DetallesTransaccion").Column(sectionColumn =>
            {
                sectionColumn.Item().Text("DETALLES DE LA TRANSACCIÓN").Bold().FontSize(14);

                // Reemplazar Grid obsoleto por Table
                sectionColumn.Item().PaddingVertical(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(); // Toneladas
                        columns.RelativeColumn(); // Precio por Ton
                        columns.RelativeColumn(); // Importe
                        columns.RelativeColumn(); // Factura
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Padding(5).Text("Toneladas").Bold();
                        header.Cell().Padding(5).Text("Precio por Ton").Bold();
                        header.Cell().Padding(5).Text("Importe").Bold();
                        header.Cell().Padding(5).Text("Factura").Bold();
                    });

                    // Datos
                    table.Cell().Padding(5).Text(_notaCargo.Tons.ToString("F2"));
                    table.Cell().Padding(5).Text(_notaCargo.Precio.ToString("C2"));
                    table.Cell().Padding(5).Text(_notaCargo.Importe.ToString("C2"));
                    table.Cell().Padding(5).Text(_notaCargo.FacturaFolio ?? "N/A");
                });
            });

            // Deducciones - CORREGIDO
            if (_notaCargo.Deducciones?.Any() == true)
            {
                column.Item().Section("Deducciones").Column(sectionColumn =>
                {
                    sectionColumn.Item().Text("DEDUCCIONES APLICADAS").Bold().FontSize(14);
                    sectionColumn.Item().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Deducción").Bold();
                            header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Importe").Bold();
                        });

                        foreach (var deduccion in _notaCargo.Deducciones)
                        {
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(5).Text(deduccion._Deduccion.Nombre);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(5).Text(deduccion.Importe.ToString("N2"));
                        }
                    });
                });
            }

            // Resumen - CORREGIDO
            column.Item().Section("Resumen").Column(sectionColumn =>
            {
                sectionColumn.Item().Text("RESUMEN").Bold().FontSize(14);
                sectionColumn.Item().PaddingVertical(10).Background(Colors.Grey.Lighten3).Padding(15).Column(col =>
                {
                    var totalDeducciones = _notaCargo.Deducciones?.Sum(d => d.Importe) ?? 0;
                    var netoAPagar = _notaCargo.Importe - totalDeducciones;

                    col.Item().AlignRight().Text($"Importe Bruto: {_notaCargo.Importe:C2}");
                    col.Item().AlignRight().Text($"Total Deducciones: {totalDeducciones:C2}");
                    col.Item().AlignRight().Text($"Neto a Pagar: {netoAPagar:C2}").Bold().FontSize(16);
                });
            });
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.AlignCenter().Text(text =>
        {
            text.Span("UUID Factura: ").FontSize(8);
            text.Span(_notaCargo.FacturaUUID).Bold().FontSize(8);
            text.EmptyLine();
            text.Span("Documento generado automáticamente - ").FontSize(8);
            text.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).FontSize(8);
        });
    }
}