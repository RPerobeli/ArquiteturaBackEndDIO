using CursosAPI.Models.Curso;
using CursosAPI.Repository.Entities;
using CursosAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CursosAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository cursoRepository;
        public CursoController(ICursoRepository _cursoRepository)
        {
            cursoRepository = _cursoRepository;
        }
        /// <summary>
        /// Permite cadastrar um novo curso para o usuario autenticado
        /// </summary>
        /// <returns>Retorna status 201 e dados do curso do usuario</returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um curso", Type = typeof(CursoViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Nao autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            Curso curso = new Curso();
            curso.nome = cursoViewModelInput.nome;
            curso.descricao = cursoViewModelInput.descricao;
            var idUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            curso.idUsuario = idUsuario;
            cursoRepository.Adicionar(curso);
            cursoRepository.Commit();
            return Created("", cursoViewModelInput);
        }
        /// <summary>
        /// Permite obter todos os cursos ativos do usuario
        /// </summary>
        /// <returns>Retorna status 200 e dados dos cursos do usuario</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os cursos", Type = typeof(CursoViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Nao autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var idUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var cursos = cursoRepository.ObterPorUsuario(idUsuario).Select(s => new CursoViewModelOutput
            {
                nome = s.nome,
                descricao = s.descricao,
                login = s.Usuario.login
            });
            
            
            return Ok(cursos);
        }
    }
}
