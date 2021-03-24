using CursosAPI.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosAPI.Repository.Infrastructure.Mapping
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("tb_Curso");
            builder.HasKey(p => p.id);
            builder.Property(p => p.id).ValueGeneratedOnAdd();
            builder.Property(p => p.nome);
            builder.Property(p => p.descricao);
            builder.HasOne(p => p.Usuario).WithMany().HasForeignKey(fk => fk.idUsuario);
            builder.Property(p => p.idUsuario);
        }
    }
}
