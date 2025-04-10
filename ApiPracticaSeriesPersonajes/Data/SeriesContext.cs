
using ApiPracticaSeriesPersonajes.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiPracticaSeriesPersonajes.Data
{
    public class SeriesContext : DbContext
    {
        public SeriesContext(DbContextOptions<SeriesContext> options) : base(options) { }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Personaje> Personajes { get; set; }
    }
}
