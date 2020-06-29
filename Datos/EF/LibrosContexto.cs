using Microsoft.EntityFrameworkCore;
using Modelos.Basicos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.EF
{
    public class LibrosContexto : DbContext
    {
        public LibrosContexto(DbContextOptions<LibrosContexto> options) : base(options)
        {

        }
        //crear DBSet
        public DbSet<Libro> LibroItems { get; set; }
        public DbSet<Categoria> CategoriaItems { get; set; }
        public DbSet<Autor> AutorItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("Categoria");

                //entity.HasNoKey();

                //entity.HasIndex(e => new { e.Efecty, e.Categoria })
                //    .HasName("IX_Homologacion_Tipo_Documento")
                //    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .IsRequired()
                    .IsUnicode(true);

                entity.Property(e => e.Nombre)
                    .HasColumnName("Nombre")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);


                entity.Property(e => e.Descripcion)
            .HasColumnName("Descripcion")
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode(false);

            });


            modelBuilder.Entity<Autor>(entity =>
            {
                entity.ToTable("Autor");


                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .IsRequired()
                    .IsUnicode(true);

                entity.Property(e => e.Nombre)
                    .HasColumnName("Nombre")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido)
                  .HasColumnName("Apellido")
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento)
                   .HasColumnName("FechaNacimiento")
                   .IsRequired()
                   .IsUnicode(false);


            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.ToTable("Libro");


                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .IsRequired()
                    .IsUnicode(true);

                entity.Property(e => e.Nombre)
                    .HasColumnName("Nombre")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);



                entity.Property(e => e.Isbn)
            .HasColumnName("Isbn")
            .IsRequired()
            .HasMaxLength(100)
            .IsUnicode(true);



            });


        }



    }
}
