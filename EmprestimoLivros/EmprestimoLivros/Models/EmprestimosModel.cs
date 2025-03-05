namespace EmprestimoLivros.Models;

public class EmprestimosModel
{
    public Guid id { get; set; }
    public string Recebedor { get; set; }
    public string Fornecedor { get; set; }
    public string Livro { get; set; }
    public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now;
}