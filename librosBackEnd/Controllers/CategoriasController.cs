using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Datos.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos.Basicos;
using Negocio.CategoriaNegocio;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace librosBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
  
        private readonly LibrosContexto _contexto;
        public CategoriasController(LibrosContexto contexto)
        {
            _contexto = contexto;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> obtenerCategorias()
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio(_contexto);
                return await categoriaNegocio.obtenerCategorias();
            }
            catch (Exception ex)
            {
                logger.Error(MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Categoria> obtenerCategoriaPorId(int id)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);

            try
            {

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio(_contexto);
                var categoria = categoriaNegocio.obtenerCategoriaPorId(id);
                if (categoria == null)
                {
                    return NotFound();
                }

                return categoria;
            }
            catch (Exception ex)
            {
                logger.Error(MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public ActionResult<Categoria> crearCategoria(Categoria item)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio(_contexto);
                int idItem = categoriaNegocio.CrearCategoria(item);
                return CreatedAtAction(nameof(obtenerCategoriaPorId), new { id = idItem }, item);
            }
            catch (Exception ex)
            {
                logger.Error(MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpPut("{id}")]

        public ActionResult<Categoria> actualizarCategoria(int id, Categoria item)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio(_contexto);
                bool esActualizado = categoriaNegocio.EditarCategoria(id, item);

                if (!esActualizado)
                {
                    logger.Info("no se encontro el  registro");
                    return NotFound();
                }
                return Ok("se edito con exito");
            }
            catch (Exception ex)
            {
                logger.Error(MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> EliminarCategoria(int id)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio(_contexto);
                bool esEliminado = categoriaNegocio.EliminarCategoria(id);

                if (!esEliminado)
                {
                    logger.Info("no se encontro el  registro");
                    return NotFound("no se encontro el registro a eliminar");
                }

                return Ok("Se elimino con exito");
            }
            catch (Exception ex)
            {
                logger.Error(MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

    }
}
