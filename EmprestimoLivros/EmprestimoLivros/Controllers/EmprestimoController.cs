using ClosedXML.Excel;
using EmprestimoLivros.Models.Entities;
using EmprestimoLivros.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Controllers;

public class EmprestimoController : Controller
{
    private readonly ISessaoService _sessaoService;
    private readonly IEmprestimosService _emprestimosService;
    
    public EmprestimoController(ISessaoService sessaoService, IEmprestimosService emprestimosService)
    {
        _sessaoService = sessaoService;
        _emprestimosService = emprestimosService;
    }
    public async Task<IActionResult> Index()
    {
        var usuario = _sessaoService.BuscarSessao();

        if (usuario == null)
            return RedirectToAction("Login", "Login");
        
        var emprestimos = await _emprestimosService.BuscarEmprestimos();
        
        return View(emprestimos.Dados);
    }
    
    [HttpGet]
    public IActionResult Cadastrar()
    {
        var usuario = _sessaoService.BuscarSessao();

        if (usuario == null)
            return RedirectToAction("Login", "Login");
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(EmprestimosModel emprestimo)
    {
        if (ModelState.IsValid)
        {
            var emprestimoResult = await _emprestimosService.CadastrarEmprestimo(emprestimo);

            if (emprestimoResult.Status)
            {
                TempData["MensagemSucesso"] = emprestimoResult.Mensagem; 
            }
            else
            {
                TempData["MensagemErro"] = emprestimoResult.Mensagem;
                return View(emprestimo);
            }

            return RedirectToAction("Index");
        }

        return View("Cadastrar");
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Editar(Guid? id)
    {
        var usuario = _sessaoService.BuscarSessao();

        if (usuario == null)
            return RedirectToAction("Login", "Login");

        var emprestimo = await _emprestimosService.BuscarEmprestimoPorId(id);
        
        return View(emprestimo.Dados);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(EmprestimosModel emprestimo)
    {
        if (ModelState.IsValid)
        {
            var emprestimoResult = await _emprestimosService.EditarEmprestimo(emprestimo);
            
            if (emprestimoResult.Status)
            {
                TempData["MensagemSucesso"] = emprestimoResult.Mensagem; 
            }
            else
            {
                TempData["MensagemErro"] = emprestimoResult.Mensagem;
                return View(emprestimo);
            }

            return RedirectToAction("Index");
        }
        
        TempData["MensagemErro"] = "Ocorreu algum erro no momento da edição!";
        
        return View(emprestimo);
    }

    [HttpGet]
    public async Task<IActionResult> Excluir(Guid? id)
    {
        var usuario = _sessaoService.BuscarSessao();

        if (usuario == null)
            return RedirectToAction("Login", "Login");

        var emprestimo = await _emprestimosService.BuscarEmprestimoPorId(id);

        return View(emprestimo.Dados);
    }

    [HttpPost]
    public async Task<IActionResult> Excluir(EmprestimosModel emprestimo)
    {
        if (emprestimo == null)
        {
            TempData["MensagemSucesso"] = "Emprestimo não localizado!";
            return View(emprestimo);
        }
        
        var emprestimoResult = await _emprestimosService.RemoverEmprestimo(emprestimo);

        if (emprestimoResult.Status)
        {
            TempData["MensagemSucesso"] = emprestimoResult.Mensagem; 
        }
        else
        {
            TempData["MensagemErro"] = emprestimoResult.Mensagem;
            return View(emprestimo);
        }
        
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Exportar()
    {
        var dados = await _emprestimosService.BuscaDadosEmprestimoExcel();

        using (XLWorkbook workbook = new XLWorkbook())
        {
            workbook.AddWorksheet(dados, "Dados Emprestimo");
            
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.SaveAs(ms);
                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Emprestimos.xlsx");
            }
        }
    }
}