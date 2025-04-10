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
        public async Task<List<Serie>> GetSeriesAsync()
        {
            return await this.context.Series.ToListAsync();
        }

        public async Task<Serie>
            FindSerieAsync(int idSerie)
        {
            return await this.context.Series.FirstOrDefaultAsync(z => z.IdSerie == idSerie);
        }
        #endregion

        #region Comentarios
        //public async Task InsertPersonajeAsync(Personaje personaje)
        //{

        //    this.context.Personajes.Add(personaje);
        //    await this.context.SaveChangesAsync();
        //}

        //public async Task<List<string>> GetSeries()
        //{
        //    List<string> series = await this.context.Personajes.Select(s => s.Serie).Distinct().ToListAsync();

        //    return series;
        //}

        //public async Task<List<Personaje>> GetPersonajesSeriesAsync(string serie)
        //{
        //    List<Personaje> personajes = await this.context.Personajes.Where(x => x.Serie == serie).ToListAsync();

        //    return personajes;
        //}

        //public async Task UpdatePersonajeAsync(Personaje personaje)
        //{
        //    Personaje updatePersonaje = await this.FindPersonajeAsync(personaje.IdPersonaje);

        //    updatePersonaje.Nombre = personaje.Nombre;
        //    updatePersonaje.Imagen = personaje.Imagen;
        //    updatePersonaje.Serie = personaje.Serie;

        //    await this.context.SaveChangesAsync();
        //}

        //public async Task<int> GetUltimoId()
        //{
        //    var ultimoId = await this.context.Personajes
        //                                    .MaxAsync(p => (int?)p.IdPersonaje);

        //    return ultimoId ?? 1;
        //}
        #endregion

    }
}
