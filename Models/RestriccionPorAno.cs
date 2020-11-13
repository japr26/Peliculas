namespace Peliculas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RestriccionPorAno")]
    public partial class RestriccionPorAno
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int ano { get; set; }

        public int idCategoria { get; set; }

        public virtual Categoria Categoria { get; set; }

    }
}
