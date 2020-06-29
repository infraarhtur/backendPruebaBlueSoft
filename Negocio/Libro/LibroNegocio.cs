using Datos.EF;
using Microsoft.EntityFrameworkCore;
using Modelos.Basicos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.LibroNegocio
{
    public class LibroNegocio
    {

        private readonly LibrosContexto _contexto;
        public LibroNegocio(LibrosContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<List<Libro>> obtenerLibros()
        {
            try
            {
                return await _contexto.LibroItems.ToListAsync();
            }
            catch (Exception e)
            {

                throw new Exception("BL Libro" + e.Message);
            }
        }

        public Libro obtenerLibroPorId(int id)
        {

            try
            {
                var LibroItem = _contexto.LibroItems.Find(id);

                if (LibroItem == null)
                {
                    return null;
                }
                return LibroItem;
            }
            catch (Exception e)
            {

                throw new Exception("BL-Libro:" + e.Message);
            }
        }

        public int CrearLibro(Libro item)
        {
            try
            {
                _contexto.LibroItems.Add(item);
                _contexto.SaveChanges();
                int id = item.Id;
                return id;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EditarLibro(int id, Libro item)
        {
            try
            {
                if (id != item.Id)
                {
                    return false;
                }

                _contexto.Entry(item).State = EntityState.Modified;
                _contexto.SaveChanges();

                return true;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EliminarLibro(int id)
        {
            try
            {

                var LibroItem = _contexto.LibroItems.Find(id);


                if (LibroItem == null)
                {
                    return false;
                }

                _contexto.LibroItems.Remove(LibroItem);
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }

}

