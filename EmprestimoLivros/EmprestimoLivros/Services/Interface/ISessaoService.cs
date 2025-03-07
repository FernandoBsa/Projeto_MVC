using EmprestimoLivros.Models.Entity;

namespace EmprestimoLivros.Services.Interface;

public interface ISessaoService
{
    UsuarioModel BuscarSessao();
    void CriarSessao(UsuarioModel usuarioModel);
    void RemoveSessao();
}