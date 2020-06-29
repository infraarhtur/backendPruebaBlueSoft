using Datos.EF;
using Microsoft.EntityFrameworkCore;
using Modelos.Basicos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.AutorNegocio
{
  public  class AutorNegocio
    {

        private readonly LibrosContexto _contexto;
        public AutorNegocio(LibrosContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<List<Autor>> obtenerAutores()
        {
            try
            {
                return await _contexto.AutorItems.ToListAsync();
            }
            catch (Exception e)
            {

                throw new Exception("BL Autor" + e.Message);
            }
        }

        public Autor obtenerAutorPorId(int id)
        {

            try
            {
                var AutorItem = _contexto.AutorItems.Find(id);

                if (AutorItem == null)
                {
                    return null;
                }
                return AutorItem;
            }
            catch (Exception e)
            {

                throw new Exception("BL-Autor:" + e.Message);
            }
        }

        public int CrearAutor(Autor item)
        {
            try
            {
                _contexto.AutorItems.Add(item);
                _contexto.SaveChanges();
                int id = item.Id;
                return id;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EditarAutor(int id, Autor item)
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

        public bool EliminarAutor(int id)
        {
            try
            {

                var AutorItem = _contexto.AutorItems.Find(id);


                if (AutorItem == null)
                {
                    return false;
                }

                _contexto.AutorItems.Remove(AutorItem);
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

