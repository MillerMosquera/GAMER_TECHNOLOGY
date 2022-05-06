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

        public async Task GenerarFactura(Pago pago,Checkout checkout, IEnumerable<DetalleFactura> detalle)
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
                document.Add(new Paragraph("Nombre del Cliente:" +checkout.nombre  + " " + checkout.apellido));
                document.Add(new Paragraph("Email del Cliente:" + checkout.email));
                document.Add(new Paragraph(" "));
                document.Add(new Paragraph(" DETALLE DE LA FACTURA "));

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
                    table.AddCell(item.Codigo.ToString());
                    table.AddCell(item.Nombre);
                    table.AddCell(item.Valor.ToString());
                    table.AddCell(item.Cantidad.ToString());
                    document.Add(table);
                }

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
