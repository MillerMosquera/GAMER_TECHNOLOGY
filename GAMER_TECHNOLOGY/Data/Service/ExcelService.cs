using Syncfusion.XlsIO;
using System.IO;
using System.Data;
using System.Collections.Generic;
using GAMER_TECHNOLOGY.Data.Model;
using Syncfusion.Drawing;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public class ExcelService : IExcelService
    {


        public MemoryStream CreateExcel(IEnumerable<Articulo> articulos)
        {
            //Create an instance of ExcelEngine.
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Create a workbook.
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                //Initialize DataTable and data get from SampleDataTable method.
                DataTable table = SampleDataTable(articulos);

                //Export data from DataTable to Excel worksheet.
                worksheet.ImportDataTable(table, true, 1, 1);

                //HEADER

                IStyle headerStyle = workbook.Styles.Add("HeaderStyle");
                headerStyle.BeginUpdate();
                headerStyle.Color = Color.FromArgb(255, 174, 33);
                headerStyle.Font.Bold = true;
                headerStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                headerStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                headerStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                headerStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                headerStyle.EndUpdate();

                worksheet.Rows[0].CellStyle = headerStyle;

                //BODY
                IStyle bodyStyle = workbook.Styles.Add("BodyStyle");
                bodyStyle.BeginUpdate();
                bodyStyle.Color = Color.FromArgb(239, 243, 247);
                bodyStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                bodyStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                bodyStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                bodyStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                bodyStyle.EndUpdate();

                worksheet.Range["A2:E39"].CellStyle = bodyStyle;

                worksheet.UsedRange.AutofitColumns();


                //Save the document as a stream and return the stream.
                using (MemoryStream stream = new MemoryStream())
                {
                    //Save the created Excel document to MemoryStream.
                    workbook.SaveAs(stream);
                    return stream;
                }
            }
            return null;
        }
        private DataTable SampleDataTable(IEnumerable<Articulo> articulos)
        {
            DataTable reports = new DataTable();

            reports.Columns.Add("Codigo");
            reports.Columns.Add("Nombre");
            reports.Columns.Add("Categoria");
            reports.Columns.Add("Descripcion");
            reports.Columns.Add("Marca");

            foreach (var item in articulos)
            {
                reports.Rows.Add(item.Codigo,item.Nombre,item.Categoria,item.Descripcion,item.Marca);
            }

            return reports;
        }
    }
}
