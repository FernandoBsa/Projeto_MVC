using EmprestimoLivros.Models.Entity;
using EmprestimoLivros.Services.Interface;
using Newtonsoft.Json;

namespace EmprestimoLivros.Services.Service;

public class SessaoService : ISessaoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SessaoService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public UsuarioModel BuscarSessao()
    {
        var sessaoUsuario = _httpContextAccessor.HttpContext.Session.GetString("sessaoUsuario");

        if (string.IsNullOrEmpty(sessaoUsuario))
            return null;
        
        return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
    }

    public void CriarSessao(UsuarioModel usuarioModel)
    {
        var usuarioJson = JsonConvert.SerializeObject(usuarioModel);
        _httpContextAccessor.HttpContext.Session.SetString("sessaoUsuario", usuarioJson);
    }

    public void RemoveSessao()
    {
        _httpContextAccessor.HttpContext.Session.Remove("sessaoUsuario");
    }
}