using GAMER_TECHNOLOGY.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface IArticuloService
    {
        Task Delete(int id);
        Task<IEnumerable<Articulo>> GetAll();
        Task<IEnumerable<Articulo>> GetId(int id);
        Task InsertArt(Articulo articulo);
        Task UpdateArticulo(Articulo articulo);
    }
}