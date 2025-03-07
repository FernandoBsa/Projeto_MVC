using EmprestimoLivros.Models.DTO;
using EmprestimoLivros.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Controllers;

public class LoginController : Controller
{
    private readonly ILoginService _loginService;
    private readonly ISessaoService _sessaoService;

    public LoginController(ILoginService loginService, ISessaoService sessaoService)
    {
        _loginService = loginService;
        _sessaoService = sessaoService;
    }

    public IActionResult Login()
    {
        var usuario = _sessaoService.BuscarSessao();

        if (usuario != null)
            return RedirectToAction("Index", "Home");
        
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(UsuarioLoginDto usuarioDto)
    {
        if (ModelState.IsValid)
        {
            var usuario = await _loginService.Login(usuarioDto);

            if (usuario.Status)
            {
                TempData["MensagemSucesso"] = usuario.Mensagem;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["MensagemErro"] = usuario.Mensagem;
                return View(usuarioDto);
            }
        }

        return View(usuarioDto);
    }

    
    public IActionResult Registrar()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Registrar(UsuarioRegisterDto usuarioDto)
    {
        if (ModelState.IsValid)
        {
            var usuario = await _loginService.RegistrarUsuario(usuarioDto);

            if (usuario.Status)
            {
                TempData["MensagemSucesso"] = usuario.Mensagem;
            }
            else
            {
                TempData["MensagemErro"] = usuario.Mensagem;
                return View(usuarioDto);
            }

            return RedirectToAction("Index");
        }
        
        return View(usuarioDto);
    }
    
    public IActionResult Logout()
    {
        _sessaoService.RemoveSessao();
        return RedirectToAction("Login");
    }

    
}