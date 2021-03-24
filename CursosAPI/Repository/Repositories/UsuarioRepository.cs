using CursosAPI.Models.Usuario;
using CursosAPI.Repository.Entities;
using CursosAPI.Repository.Infrastructure;
using CursosAPI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosAPI.Repository.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CursoDbContext contexto;
        public UsuarioRepository(CursoDbContext _contexto)
        {
            contexto = _contexto;
        }
        public void Adicionar(Usuario usuario)
        {
            contexto.Usuario.Add(usuario);

        }
        public void Commit()
        {
            contexto.SaveChanges();
        }
        public Usuario ObterUsuario(string login)
        {
            return contexto.Usuario.FirstOrDefault(u => u.login == login);            
        }
    }
}
