using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using iText.Layout;
using iText.Layout.Element;
using System.IO;
using iText.Kernel.Pdf;
using GAMER_TECHNOLOGY.Data.Model;
using GAMER_TECHNOLOGY.Data.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Colors;
using iText.Layout.Properties;
using iText.Kernel.Geom;

namespace GAMER_TECHNOLOGY.Data.PDF
{
    public class FacturaPDF : PageModel, IFacturaPDF
    {
        private readonly IWebHostEnvironment _env;

        public FacturaPDF(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task GenerarFactura(IEnumerable<Checkout> checkout, IEnumerable<Detalle_venta> detalle)
        {
            string destination = "wwwroot/FilePdf/Factura.pdf";
            FileInfo file = new FileInfo(destination);
            file.Delete();
            var fileStream = file.Create();
            fileStream.Close();
            PdfDocument pdfdoc = new PdfDocument(new PdfWriter(file));
            pdfdoc.SetTagged();

            //Escribir el documento
            using (Document document = new Document(pdfdoc))
            {
                foreach(var info in checkout)
                {
                    document.Add(new Paragraph("Nombre del Cliente:" + info.nombre  + " " + info.apellido));
                    document.Add(new Paragraph("Email del Cliente:" + info.email_user));
                    document.Add(new Paragraph("Direccion:" + info.direccion));
                    document.Add(new Paragraph("Ciudad:" + info.ciudad));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph(" DETALLE DE LA FACTURA "));

                }

                float[] columnWidths = new float[] { 70f, 200f, 70f, 70f };
                Table table = new Table(columnWidths);
                Cell cell = new Cell(1, 1)
                  .SetBackgroundColor(ColorConstants.GRAY)
                  .SetTextAlignment(TextAlignment.CENTER)
                  .Add(new Paragraph("Código:"));
                table.AddCell(cell);
                cell = new Cell(1, 1)
                   .SetBackgroundColor(ColorConstants.GRAY)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Nombre Producto:"));
                table.AddCell(cell);
                cell = new Cell(1, 1)
                   .SetBackgroundColor(ColorConstants.GRAY)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Valor:"));
                table.AddCell(cell);
                cell = new Cell(1, 1)
                   .SetBackgroundColor(ColorConstants.GRAY)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .Add(new Paragraph("Cantidad:"));
                table.AddCell(cell);
                document.Add(table);

                foreach (var item in detalle)
                {
                    table = new Table(columnWidths);
                    table.AddCell(item.id_venta.ToString());
                    table.AddCell(item.nombre);
                    table.AddCell(item.valor.ToString());
                    table.AddCell(item.cantidad.ToString());
                    document.Add(table);
                }
                float[] columnWidths2 = new float[] { 70f, 200f, 70f, 70f };
                Table table2 = new Table(columnWidths2);
                Cell cell2 = new Cell(1, 3)
                 .SetBackgroundColor(ColorConstants.GRAY)
                 .SetTextAlignment(TextAlignment.CENTER)
                 .Add(new Paragraph("Valor Total:"));
                table2.AddCell(cell2);
                var total_venta = detalle.Select(x => x.valor * x.cantidad).Sum();
                table2.AddCell(total_venta.ToString("0,# $"));
                document.Add(table2);

                document.Close();
            }
            descargarPDF();
        }

        public FileResult descargarPDF()
        {
            var filePath = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot/FilePdf", "Factura.pdf");
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "wwwroot/FilePdf", "Factura.pdf");
        }
    }
}
