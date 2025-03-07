using EmprestimoLivros.Models.DTO;
using EmprestimoLivros.Models.Entity;
using EmprestimoLivros.Models.Response;

namespace EmprestimoLivros.Services.Interface;

public interface ILoginService
{
    Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioDto);
    Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioDto);
}