using GAMER_TECHNOLOGY.Data.Model;
using System.Collections.Generic;
using System.IO;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface IExcelService
    {
        MemoryStream CreateExcel(IEnumerable<Articulo> articulos);
    }
}