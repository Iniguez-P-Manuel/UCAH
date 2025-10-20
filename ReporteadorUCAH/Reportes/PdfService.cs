using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using ReporteadorUCAH.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Infrastructure;
using QuestPDF.Companion;
using System.Diagnostics;
namespace ReporteadorUCAH.Reportes
{




    public class PdfService
    {
        public PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        // 1. GENERAR Y GUARDAR PDF
        public void GenerarYGuardarPdf(NotaCargo notaCargo, string outputPath)
        {
            try
            {
                var document = new ReporteNotaCargo(notaCargo);
                document.GeneratePdf(outputPath);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar PDF: {ex.Message}");
            }
        }

        // 2. GENERAR PDF COMO BYTES (para ASP.NET Core)
        public byte[] GenerarPdfBytes(NotaCargo notaCargo)
        {
            try
            {
                var document = new ReporteNotaCargo(notaCargo);
                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar PDF: {ex.Message}");
            }
        }

        // 3. VISTA PREVIA - ABRIR CON VISOR PREDETERMINADO
        public void MostrarVistaPrevia(NotaCargo notaCargo)
        {
            try
            {
                // Generar PDF temporal
                var tempPdfPath = Path.Combine(
                    Path.GetTempPath(),
                    $"NotaCargo_Preview_{notaCargo.Id}_{DateTime.Now:yyyyMMddHHmmss}.pdf"
                );

                GenerarYGuardarPdf(notaCargo, tempPdfPath);

                // Abrir con visor predeterminado
                AbrirConVisorPredeterminado(tempPdfPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al mostrar vista previa: {ex.Message}");
            }
        }

        // 4. GENERAR Y ABRIR INMEDIATAMENTE
        public void GenerarYAbrir(NotaCargo notaCargo, string outputPath = null)
        {
            try
            {
                var finalPath = outputPath ?? Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"NotaCargo_{notaCargo.Id}_{DateTime.Now:yyyyMMddHHmmss}.pdf"
                );

                GenerarYGuardarPdf(notaCargo, finalPath);
                AbrirConVisorPredeterminado(finalPath);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar y abrir: {ex.Message}");
            }
        }

        private void AbrirConVisorPredeterminado(string pdfPath)
        {
            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = pdfPath,
                    UseShellExecute = true // Esto abre con el programa predeterminado del sistema
                };

                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo abrir el PDF: {ex.Message}");
            }
        }
    }
}