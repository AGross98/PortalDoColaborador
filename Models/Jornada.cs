namespace PortalColaborador.Models;

public class Jornada
{
    public Jornada() => Ponto = DateTime.Now;
    public int JornadaId { get; set; }
    public DateTime Ponto { get; set; }
    public Funcionario? Funcionario { get; set; }
    public int FuncionarioId { get; set; }
    
}