using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.Models.Entities;

public class EmprestimosModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Digite o nome do Recebedor")]
    public string Recebedor { get; set; }
    [Required(ErrorMessage = "Digite o nome do Fornecedor")]
    public string Fornecedor { get; set; }
    [Required(ErrorMessage = "Digite o nome do Livro")]
    public string Livro { get; set; }

    public DateTime DataUltimaAtualizacao { get; set; } = DateTime.UtcNow;
}