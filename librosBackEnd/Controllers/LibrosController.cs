using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Datos.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Basicos;
using Negocio.LibroNegocio;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace librosBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {

        private readonly LibrosContexto _contexto;
        public LibrosController(LibrosContexto contexto)
        {
            _contexto = contexto;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> obtenerLibros()
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {
                LibroNegocio LibroNegocio = new LibroNegocio(_contexto);
                return await LibroNegocio.obtenerLibros();
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Libro> obtenerLibroPorId(int id)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);

            try
            {

                LibroNegocio LibroNegocio = new LibroNegocio(_contexto);
                var Libro = LibroNegocio.obtenerLibroPorId(id);
                if (Libro == null)
                {
                    return NotFound();
                }

                return Libro;
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public ActionResult<Libro> crearLibro(Libro item)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);

            try
            {
                LibroNegocio LibroNegocio = new LibroNegocio(_contexto);
                int idItem = LibroNegocio.CrearLibro(item);
                return CreatedAtAction(nameof(obtenerLibroPorId), new { id = idItem }, item);
            }
            catch (Exception ex)
            {
                logger.Error(MethodBase.GetCurrentMethod().Name, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        [HttpPut("{id}")]
        public ActionResult<Libro> actualizarLibro(int id, Libro item)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {
                LibroNegocio LibroNegocio = new LibroNegocio(_contexto);
                bool esActualizado = LibroNegocio.EditarLibro(id, item);

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
        public ActionResult<Libro> EliminarLibro(int id)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info(MethodBase.GetCurrentMethod().Name);
            try
            {
                LibroNegocio LibroNegocio = new LibroNegocio(_contexto);
                bool esEliminado = LibroNegocio.EliminarLibro(id);

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
