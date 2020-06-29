using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Datos.EF;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Basicos;
using Negocio.AutorNegocio;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace librosBackEnd.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly LibrosContexto _contexto;
      
        public AutoresController(LibrosContexto contexto)
        {
            _contexto = contexto;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

         
        }

       

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> obtenerAutors()
        {


            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);

            try
            {
                AutorNegocio AutorNegocio = new AutorNegocio(_contexto);
                                
               
                return await AutorNegocio.obtenerAutores();
            }
            catch (Exception ex)
            {
                logger.Error(MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Autor> obtenerAutorPorId(int id)
        {

            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {

                AutorNegocio AutorNegocio = new AutorNegocio(_contexto);
                var Autor = AutorNegocio.obtenerAutorPorId(id);
                if (Autor == null)
                {
                    logger.Info("no se encontro el  registro");
                    return NotFound();
                }

                return Autor;
            }
            catch (Exception ex)
            {
                logger.Error(MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public ActionResult<Autor> crearAutor(Autor item)
        {

            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {
                AutorNegocio AutorNegocio = new AutorNegocio(_contexto);
                int idItem = AutorNegocio.CrearAutor(item);
                return CreatedAtAction(nameof(obtenerAutorPorId), new { id = idItem }, item);
            }
            catch (Exception ex)
            {

                logger.Error(MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpPut("{id}")]

        public ActionResult<Autor> actualizarAutor(int id, Autor item)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {
                AutorNegocio AutorNegocio = new AutorNegocio(_contexto);
                bool esActualizado = AutorNegocio.EditarAutor(id, item);

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
        public ActionResult<Autor> EliminarAutor(int id)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {
                AutorNegocio AutorNegocio = new AutorNegocio(_contexto);
                bool esEliminado = AutorNegocio.EliminarAutor(id);

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
