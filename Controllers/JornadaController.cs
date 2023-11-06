using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalColaborador.Data;
using PortalColaborador.Models;

namespace PortalColaborador.Controllers;

[ApiController]
[Route("portalcolaborador/jornada")]
public class JornadaController : ControllerBase
{
    private readonly AppDataContext _context;

    public JornadaController(AppDataContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Jornada> jornadas = _context.jornadas.Include(x => x.Funcionario).ToList();
            return Ok(jornadas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Jornada jornada)
    {
        try
        {
            Funcionario? funcionario = _context.Funcionarios.Find(jornada.FuncionarioId);
            if (funcionario == null || funcionario.Status != 0)
            {
                return BadRequest("Funcionario não encontrado ou inativo");
            }
            jornada.Funcionario = funcionario;
            _context.Add(jornada);
            _context.SaveChanges();
            return Created("", jornada);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}