using CursosAPI.Repository.Entities;
using CursosAPI.Repository.Infrastructure;
using CursosAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosAPI.Repository.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext contexto;
        public CursoRepository(CursoDbContext _contexto)
        {
            contexto = _contexto;
        }
        public void Adicionar(Curso curso)
        {
            contexto.Curso.Add(curso);
        }

        public void Commit()
        {
            contexto.SaveChanges();
        }

        public IList<Curso> ObterPorUsuario(int idUsuario)
        {
            return contexto.Curso.Include(i => i.Usuario).Where(w => w.idUsuario == idUsuario).ToList();
        }
    }
}
