namespace Peliculas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Peliculas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPelicula { get; set; }

        [Required(ErrorMessage = "Campo necesario")]
        [StringLength(50)]
        public string titulo { get; set; }

        [Required(ErrorMessage = "Campo necesario")]
        //[Column(TypeName = "date")]
        public int ano { get; set; }

        public int idCategoria { get; set; }

        //[Required(ErrorMessage = "Campo necesario")]
        [StringLength(250)]
        public string rutaImagen { get; set; }

        public virtual Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Campo necesario")]
        [StringLength(30)]
        public string Director { get; set; }
    }
}
