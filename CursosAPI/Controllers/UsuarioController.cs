using CursosAPI.Filters;
using CursosAPI.Models;
using CursosAPI.Models.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CursosAPI.Repository.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CursosAPI.Repository.Entities;
using CursosAPI.Repository.Interfaces;
using CursosAPI.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using CursosAPI.Services.Interfaces;

namespace CursosAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IAuthenticationService authenticationService;
        public UsuarioController(
            IUsuarioRepository _usuarioRepository, 
            IAuthenticationService _authenticationService)
        {
            usuarioRepository = _usuarioRepository;
            authenticationService = _authenticationService;
        }
        /// <summary>
        /// Autentica um usuário
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns></returns>
        [SwaggerResponse(statusCode:200,description:"Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatorios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("login")]
        [ValidacaoModelStateCustom]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            Usuario usuario = usuarioRepository.ObterUsuario(loginViewModelInput.login);
            if(usuario == null)
            {
                return BadRequest("Houve um erro ao tentar acessar");
            }
            //if(usuario.senha != loginViewModelInput.senha.GerarSenhaCriptografada())
            //{
            //    return BadRequest("Houve um erro ao tentar acessar a senha");
            //}
            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                id = usuario.id,
                login = loginViewModelInput.login,
                email = usuario.email
            };
            var token = authenticationService.GerarToken(usuarioViewModelOutput);
            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        /// <summary>
        /// Registra um novo um usuário
        /// </summary>
        /// <param name="registroViewModelInput"></param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao registrar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatorios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustom]
        public IActionResult Registrar(RegistroViewModelInput registroViewModelInput)
        {
            //var migracoesPendentes = contexto.Database.GetPendingMigrations();
            //if (migracoesPendentes.Count() > 0)
            //{
            //    contexto.Database.Migrate();
            //}
            var usuario = new Usuario();
            usuario.login = registroViewModelInput.login;
            usuario.senha = registroViewModelInput.senha;
            usuario.email = registroViewModelInput.email;
            usuarioRepository.Adicionar(usuario);
            usuarioRepository.Commit();

            return Created("", registroViewModelInput);
        }
    }
}
