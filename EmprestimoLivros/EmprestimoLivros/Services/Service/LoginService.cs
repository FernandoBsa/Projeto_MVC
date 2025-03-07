using EmprestimoLivros.Data.Context;
using EmprestimoLivros.Models.DTO;
using EmprestimoLivros.Models.Entity;
using EmprestimoLivros.Models.Response;
using EmprestimoLivros.Services.Interface;
using EmprestimoLivros.Utils;


namespace EmprestimoLivros.Services.Service;

public class LoginService : ILoginService
{
    private readonly ApplicationDbContext _context;
    private readonly ISessaoService _sessaoService;
    
    public LoginService(ApplicationDbContext context, ISessaoService sessaoService)
    {
        _context = context;
        _sessaoService = sessaoService;
    }
    
    public async Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto usuarioDto)
    {
        ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();
        try
        {
            if (VerificarSeEmailExiste(usuarioDto))
            {
                response.Mensagem = "Email ja cadastrado!";
                response.Status = false;
                return response;
            }
            
            LoginHelper.CriarSenhaHash(usuarioDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            var usuarioModel = new UsuarioModel()
            {
                Nome = usuarioDto.Nome,
                Sobrenome = usuarioDto.Sobrenome,
                Email = usuarioDto.Email,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt
            };

            _context.Add(usuarioModel);
            await _context.SaveChangesAsync();
            
            response.Mensagem = "Usuario cadastrado com sucesso!";
            return response;

        }
        catch (Exception e)
        {
            response.Mensagem = e.Message;
            response.Status = false;

            return response;
        }
    }

    public async Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioDto)
    {
        ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();
        
        try
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == usuarioDto.Email);

            if (usuario == null)
            {
                response.Mensagem = "Credenciais Invalidas!";
                response.Status = false;
                return response;
            }

            if (!LoginHelper.VerificaSenha(usuarioDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
            {
                response.Mensagem = "Credenciais Invalidas!";
                response.Status = false;
                return response;
            }
    
            _sessaoService.CriarSessao(usuario);
            
            response.Mensagem = "Usuario logado com sucesso!";

            return response;

        }
        catch (Exception e)
        {
            response.Mensagem = e.Message;
            response.Status = false;

            return response;
        }
    }

    private bool VerificarSeEmailExiste(UsuarioRegisterDto usuarioDto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == usuarioDto.Email);

        if (usuario == null)
        {
            return false;
        }
        
        return true;
    }
    
}