using ApiPracticaSeriesPersonajes.Data;
using ApiPracticaSeriesPersonajes.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiPracticaSeriesPersonajes.Repositories
{
    public class RepositoryPersonajes
    {
        private SeriesContext context;

        public RepositoryPersonajes(SeriesContext context)
        {
            this.context = context;
        }

        #region Personajes
        // MOSTRAR PERSONAJES
        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await this.context.Personajes.ToListAsync();
        }
        // BUSCAR PERSONAJE
        public async Task<Personaje>
            FindPersonajeAsync(int idPersonaje)
        {
            return await this.context.Personajes.FirstOrDefaultAsync(z => z.IdPersonaje == idPersonaje);
        }
        // MOVER PERSONAJE DE UNA SERIE A OTRA
        public async Task MovePersonajeToSerieAsync(int idPersonaje, int idSerieNuevo)
        {
            Personaje personaje = await this.FindPersonajeAsync(idPersonaje);
            if (personaje != null)
            {
                personaje.IdSerie = idSerieNuevo;
                await this.context.SaveChangesAsync();
            }
        }

        // DELETE PERSONAJE
        public async Task DeletePersonajeAsync(int id)
        {
            Personaje personaje = await this.FindPersonajeAsync(id);
            this.context.Personajes.Remove(personaje);
            await this.context.SaveChangesAsync();
        }
        #endregion

        #region Series
        // TODAS LAS SERIES
        public async Task<List<Serie>> GetSeriesAsync()
        {
            return await this.context.Series.ToListAsync();
        }
        // BUSCAR UNA SERIE POR ID
        public async Task<Serie>
            FindSerieAsync(int idSerie)
        {
            return await this.context.Series.FirstOrDefaultAsync(z => z.IdSerie == idSerie);
        }
        // BUSCAR PERSONAJES POR SERIE
        public async Task<List<Personaje>> GetPersonajesBySerieAsync(int idSerie)
        {
            return await this.context.Personajes.Where(p => p.IdSerie == idSerie).ToListAsync();
        }
        // BUSCAR PERSONAJES DE VARIAS SERIES
        public async Task<List<Personaje>> GetPersonajesBySeriesAsync(List<int> idsSerie)
        {
            return await this.context.Personajes.Where(p => idsSerie.Contains(p.IdSerie)).ToListAsync();
        }
        #endregion

    }
}
