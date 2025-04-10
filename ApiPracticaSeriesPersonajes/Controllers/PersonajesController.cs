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
        public async Task<ActionResult<List<Personaje>>>
            GetPersonajes()
        {
            return await this.repo.GetPersonajesAsync();
        }
        // BUSCAR PERSONAJE
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Personaje>>
            FindPersonaje(int id)
        {
            return await this.repo.FindPersonajeAsync(id);
        }
        // MOVER PERSONAJE DE UNA SERIE A OTRA
        [HttpPut("[action]/{idPersonaje}/{idSerieNuevo}")]
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
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Serie>>>
            GetSeries()
        {
            return await this.repo.GetSeriesAsync();
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Serie>>
            FindSerie(int id)
        {
            return await this.repo.FindSerieAsync(id);
        }
        #endregion

        #region Comentarios
        //[HttpGet("[action]")]
        //public async Task<ActionResult<List<string>>>
        //    GetSeries()
        //{
        //    return await this.repo.GetSeries();
        //}

        //[HttpGet("[action]/{serie}")]
        //public async Task<ActionResult<List<Personaje>>>
        //    GetPersonajesSerie(string serie)
        //{
        //    return await this.repo.GetPersonajesSeriesAsync(serie);
        //}

        //[HttpPost("[action]")]
        //public async Task<ActionResult> PostPersonaje
        //    (Personaje personaje)
        //{
        //    int lastId = await repo.GetUltimoId() + 1;

        //    personaje.IdPersonaje = lastId;

        //    await this.repo.InsertPersonajeAsync(personaje);
        //    return Ok();
        //}

        //[HttpPut("[action]")]
        //public async Task<ActionResult> PutPersonaje
        //    (Personaje personaje)
        //{
        //    await this.repo.UpdatePersonajeAsync(personaje);
        //    return Ok();
        //}
        #endregion

    }
}
