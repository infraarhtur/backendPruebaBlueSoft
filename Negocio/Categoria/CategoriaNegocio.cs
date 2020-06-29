using Datos.EF;
using Microsoft.EntityFrameworkCore;
using Modelos.Basicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.CategoriaNegocio
{
   public class CategoriaNegocio
    {
        private readonly LibrosContexto _contexto;
        public CategoriaNegocio(LibrosContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task< List<Categoria> >obtenerCategorias()
        {
            try
            {
                return await _contexto.CategoriaItems.ToListAsync();
            }
            catch (Exception e)
            {

                throw new Exception( "BL Categoria"+ e.Message) ;
            }
        }

        public Categoria obtenerCategoriaPorId(int id)
        {

            try
            {
                var categoriaItem = _contexto.CategoriaItems.Find(id);

                if (categoriaItem == null)
                {
                    return null;
                }
                return categoriaItem;
            }
            catch (Exception e)
            {

                throw new Exception("BL-categoria:"+ e.Message);
            }
        }

        public int CrearCategoria(Categoria item)
        {
            try
            {
                _contexto.CategoriaItems.Add(item);
                _contexto.SaveChanges();
                int id = item.Id;
                return id;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool EditarCategoria(int id ,Categoria item)
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


        public bool EliminarCategoria(int id)
        {
            try
            {

                var categoriaItem =  _contexto.CategoriaItems.Find(id);


                if (categoriaItem == null)
                {
                    return false;
                }

                _contexto.CategoriaItems.Remove(categoriaItem);
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
