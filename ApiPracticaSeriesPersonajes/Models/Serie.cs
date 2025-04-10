using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiPracticaSeriesPersonajes.Models
{
    [Table("SERIES")]
    public class Serie
    {
        [Key]
        [Column("IDSERIE")]
        public int IdSerie { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("ANYO")]
        public int Anyo { get; set; }
    }

}
