using Microsoft.AspNetCore.Mvc;
using PortalColaborador.Data;

namespace PortalColaborador.Controllers;

[ApiController]
[Route("portalcolaborador/login")]

public class LoginController : ControllerBase
{
    private readonly AppDataContext _context;

    public LoginController(AppDataContext context)
    {
        _context = context;
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
            //Verificar atravez do CPF do funcionario se existe um funcionario
            var funcionarioExiste = _context.Funcionarios.SingleOrDefault(f => f.Cpf == usuario.Funcionario.Cpf);
            //Se existir salvar o usuario no banco de dados.
            if(funcionarioExiste != null){
                usuario.FuncionarioId = funcionarioExiste.FuncionarioId;
                usuario.Funcionario = funcionarioExiste;
                _context.Add(usuario);
                _context.SaveChanges();
                return Ok(usuario + "Cadastrado com Susesso.");
            }
            return NotFound("Nenhum funcionario encontrado");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
