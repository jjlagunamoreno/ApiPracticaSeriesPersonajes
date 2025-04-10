using ApiPracticaSeriesPersonajes.Models;
using ApiPracticaSeriesPersonajes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPracticaSeriesPersonajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }

        #region Personajes
        // MOSTRAR PERSONAJES
        [HttpGet("[action]")]
        [Tags("Personajes")]
        public async Task<ActionResult<List<Personaje>>>
            GetPersonajes()
        {
            return await this.repo.GetPersonajesAsync();
        }
        // BUSCAR PERSONAJE
        [HttpGet("[action]/{id}")]
        [Tags("Personajes")]
        public async Task<ActionResult<Personaje>>
            FindPersonaje(int id)
        {
            return await this.repo.FindPersonajeAsync(id);
        }
        // MOVER PERSONAJE DE UNA SERIE A OTRA
        [HttpPut("[action]/{idPersonaje}/{idSerieNuevo}")]
        [Tags("Personajes")]
        public async Task<ActionResult> MovePersonajeToSerie(int idPersonaje, int idSerieNuevo)
        {
            var personaje = await this.repo.FindPersonajeAsync(idPersonaje);
            if (personaje == null)
            {
                return NotFound($"No existe el personaje con ID {idPersonaje}");
            }
            await this.repo.MovePersonajeToSerieAsync(idPersonaje, idSerieNuevo);
            return Ok($"Personaje: {idPersonaje} movido a la serie: {idSerieNuevo}");
        }

        // DELETE PERSONAJE
        [HttpDelete("[action]/{id}")]
        [Tags("Personajes")]
        public async Task<ActionResult> DeletePersonaje(int id)
        {
            //PODEMOS PERSONALIZAR LA RESPUESTA
            if (await this.repo.FindPersonajeAsync(id) == null)
            {
                //NO EXISTE EL DEPARTAMENTO PARA ELIMINARLO
                return NotFound();
            }
            else
            {
                await this.repo.DeletePersonajeAsync(id);
                return Ok();
            }
        }
        #endregion

        #region Series
        // TODAS LAS SERIES
        [HttpGet("[action]")]
        [Tags("Series")]
        public async Task<ActionResult<List<Serie>>>
            GetSeries()
        {
            return await this.repo.GetSeriesAsync();
        }
        // BUSCAR UNA SERIE POR ID
        [HttpGet("[action]/{id}")]
        [Tags("Series")]
        public async Task<ActionResult<Serie>>
            FindSerie(int id)
        {
            return await this.repo.FindSerieAsync(id);
        }
        // BUSCAR PERSONAJES POR SERIE
        [HttpGet("[action]/{idSerie}")]
        [Tags("Series")]
        public async Task<ActionResult<List<Personaje>>> GetPersonajesBySerie(int idSerie)
        {
            var personajes = await this.repo.GetPersonajesBySerieAsync(idSerie);
            if (personajes == null || personajes.Count == 0)
            {
                return NotFound($"No hay personajes en la serie: {idSerie}");
            }
            return personajes;
        }
        // BUSCAR PERSONAJES DE VARIAS SERIES
        [HttpPost("[action]")]
        [Tags("Series")]
        public async Task<ActionResult<List<Personaje>>> GetPersonajesBySeries([FromBody] List<int> idsSerie)
        {
            var personajes = await this.repo.GetPersonajesBySeriesAsync(idsSerie);
            if (personajes == null || personajes.Count == 0)
            {
                return NotFound("No se encontraron personajes para los Ids de serie proporcionados.");
            }
            return personajes;
        }

        #endregion

    }
}
