using Microsoft.AspNetCore.Mvc;
using PortalColaborador.Data;
using PortalColaborador.Models;


namespace PortalColaborador.Controllers;

[ApiController]
[Route("portalcolaborador/funcionario")]
public class FuncionarioController : ControllerBase
{
    private readonly AppDataContext _context;

    public FuncionarioController(AppDataContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Funcionario> funcionarios = _context.Funcionarios.ToList();
            return Ok(funcionarios);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Funcionario funcionario)
    {
        try
        {
            _context.Add(funcionario);
            _context.SaveChanges();
            return Created("", funcionario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("atualizar/{id}")]
    public IActionResult Atualizar([FromRoute] int id, 
        [FromBody] Funcionario funcionario)
    {
        try
        {
            Funcionario? funcionarioCadastrado = 
            _context.Funcionarios.FirstOrDefault(x => x.FuncionarioId == id);
            
            if (funcionarioCadastrado == null)
            {
                return NotFound();
            }
            funcionarioCadastrado.Nome = funcionario.Nome;
            funcionarioCadastrado.Cpf = funcionario.Cpf;
            funcionarioCadastrado.Status = funcionario.Status;
            _context.Funcionarios.Update(funcionarioCadastrado);
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
            Funcionario? funcionarioCadastrado = _context.Funcionarios.Find(id);
            if (funcionarioCadastrado == null)
            {
                return NotFound();
            }
            _context.Funcionarios.Remove(funcionarioCadastrado);
            _context.SaveChanges();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}