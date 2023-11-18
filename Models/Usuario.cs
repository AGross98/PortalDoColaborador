using PortalColaborador.Models;

public class Usuario
{
    public int UsuarioId { get; set; }
    public string? user { get; set; }
    public string? senha { get; set; }
    public Funcionario? Funcionario { get; set; }
    public int  FuncionarioId { get; set; }

}