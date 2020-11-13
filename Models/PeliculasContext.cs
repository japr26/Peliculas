namespace Peliculas.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PeliculasContext : DbContext
    {
        public PeliculasContext()
            : base("name=PeliculasContext")
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Peliculas> Peliculas { get; set; }
        public virtual DbSet<RestriccionPorAno> RestriccionPorAno { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Peliculas)
                .WithRequired(e => e.Categoria)
                .HasForeignKey(e => e.idCategoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Peliculas>()
                .Property(e => e.titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Peliculas>()
                .Property(e => e.rutaImagen)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.RestriccionPorAno)
                .WithRequired(e => e.Categoria)
                .HasForeignKey(e => e.idCategoria)
                .WillCascadeOnDelete(false);
        }
    }
}
