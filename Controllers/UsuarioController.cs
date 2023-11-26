using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalColaborador.Data;
using PortalColaborador.Models;
using System;
using System.Linq;

namespace PortalColaborador.Controllers
{
    [ApiController]
    [Route("portalcolaborador/login")]
    public class LoginController : ControllerBase
    {
        private readonly AppDataContext _context;

        public LoginController(AppDataContext context)
        {
            _context = context;
        }

[HttpPost]
[Route("autenticar")]
public IActionResult Autenticar([FromBody] Usuario usuario)
{
    try
    {
        var usuarioAutenticado = _context.usuarios
            .Include(u => u.Funcionario)
            .FirstOrDefault(u => u.user == usuario.user && u.senha == usuario.senha);

        if (usuarioAutenticado != null)
        {
            // Verifica se o usuário é um gerente
            if (usuarioAutenticado.Funcionario.cargo == 1)
            {
                // Autenticação bem-sucedida para gerente
                return Ok(usuarioAutenticado); // Certifique-se de incluir informações do funcionário aqui
            }
            else
            {
                // Autenticação bem-sucedida para usuário
                return Ok(usuarioAutenticado); // Certifique-se de incluir informações do funcionário aqui
            }
        }

        // Autenticação falhou
        return Unauthorized("Usuário ou senha incorretos");
    }
    catch (Exception e)
    {
        return BadRequest(new { error = e.Message });
    }
}


        [HttpGet]
    [Route("listar")]
    public IActionResult Listar(){
        try
        {
            List<Usuario> usuarios = _context.usuarios.ToList();
            return Ok(usuarios);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

[HttpPost]
[Route("cadastrar")]
public IActionResult Cadastrar([FromBody] Usuario usuario)
{
    try
    {
        // Verificar através do CPF do funcionário se existe um funcionário
        var funcionarioExiste = _context.Funcionarios.SingleOrDefault(f => f.Cpf == usuario.Funcionario.Cpf);
        
        if(funcionarioExiste != null)
        {
            usuario.FuncionarioId = funcionarioExiste.FuncionarioId;
            usuario.Funcionario = funcionarioExiste;
            _context.Add(usuario);
            _context.SaveChanges();

            // Retorna uma resposta simples indicando que o cadastro foi bem-sucedido
            return Ok(new { message = "Cadastro bem-sucedido" });
        }
        else
        {
            // Retorna uma resposta indicando que nenhum funcionário foi encontrado
            return NotFound(new { error = "Nenhum funcionário encontrado" });
        }
    }
    catch (Exception e)
    {
        // Retorna uma resposta indicando que ocorreu um erro durante o processamento
        return BadRequest(new { error = e.Message });
    }
}


    [HttpPut]
    [Route("atualizar/{id}")]
    public IActionResult Atualizar([FromRoute] int id, 
        [FromBody] Usuario usuario)
    {
        try
        {
            Usuario? usuarioCadastrado = _context.usuarios.FirstOrDefault(u => u.UsuarioId == id);
            
            if(usuarioCadastrado == null){
                return NotFound("Usuario não encontrado ou não informado");
            }

            usuarioCadastrado.user = usuario.user;
            usuarioCadastrado.senha = usuario.senha;
            _context.usuarios.Update(usuarioCadastrado);
            _context.SaveChanges();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("deletar/{id}")]
    public IActionResult Deletar([FromRoute] int id)
    {
        try
        {
            Usuario? usuarioCadastrado = _context.usuarios.Find(id);
            if(usuarioCadastrado == null){
                return NotFound("Usuario não encontrado ou não informado");
            }
            _context.usuarios.Remove(usuarioCadastrado!);
            _context.SaveChanges();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message + "Erro inesperado");
        }
    }
    }
}