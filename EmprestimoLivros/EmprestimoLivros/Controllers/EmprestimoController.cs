using System.Data;
using ClosedXML.Excel;
using EmprestimoLivros.Data.Context;
using EmprestimoLivros.Models;
using EmprestimoLivros.Models.Entity;
using EmprestimoLivros.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Controllers;

public class EmprestimoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ISessaoService _sessaoService;
    
    public EmprestimoController(ApplicationDbContext context, ISessaoService sessaoService)
    {
        _context = context;
        _sessaoService = sessaoService;
    }
    public IActionResult Index()
    {
        var usuario = _sessaoService.BuscarSessao();

        if (usuario == null)
            return RedirectToAction("Login", "Login");
        
        IEnumerable<EmprestimosModel> emprestimos = _context.Emprestimos;
        return View(emprestimos);
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
    public IActionResult Cadastrar(EmprestimosModel emprestimo)
    {
        if (ModelState.IsValid)
        {
            emprestimo.DataUltimaAtualizacao = DateTime.UtcNow;
            
            _context.Emprestimos.Add(emprestimo);
            _context.SaveChanges();

            TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!"; 

            return RedirectToAction("Index");
        }

        return View("Cadastrar");
    }
    
    
    [HttpGet]
    public IActionResult Editar(Guid id)
    {
        var usuario = _sessaoService.BuscarSessao();

        if (usuario == null)
            return RedirectToAction("Login", "Login");
        
        if (id == null || id == Guid.Empty)
            return NotFound();
        
        EmprestimosModel emprestimo = _context.Emprestimos.FirstOrDefault(e => e.Id == id);
        
        if (emprestimo == null)
            return NotFound();
        
        return View(emprestimo);
    }

    [HttpPost]
    public IActionResult Editar(EmprestimosModel emprestimo)
    {
        if (ModelState.IsValid)
        {
            var emprestimoDb = _context.Emprestimos.Find(emprestimo.Id);
            emprestimoDb.Fornecedor = emprestimo.Fornecedor;
            emprestimoDb.Recebedor = emprestimo.Recebedor;
            emprestimoDb.Livro = emprestimo.Livro;
            
            _context.Emprestimos.Update(emprestimoDb);
            _context.SaveChanges();
            
            TempData["MensagemSucesso"] = "Edição realizado com sucesso!"; 

            return RedirectToAction("Index");
        }

        return View(emprestimo);
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var usuario = _sessaoService.BuscarSessao();

        if (usuario == null)
            return RedirectToAction("Login", "Login");
        
        if (id == null | id == Guid.Empty)
            return NotFound();
        
        EmprestimosModel emprestimo = _context.Emprestimos.FirstOrDefault(x => x.Id == id);

        if (emprestimo == null)
            return NotFound();

        return View(emprestimo);
    }

    [HttpPost]
    public IActionResult Excluir(EmprestimosModel emprestimo)
    {
        if (emprestimo == null)
            return NotFound();
        
        _context.Emprestimos.Remove(emprestimo);
        _context.SaveChanges();
        
        TempData["MensagemSucesso"] = "Remoção realizada com sucesso!"; 

        return RedirectToAction("Index");
    }

    public IActionResult Exportar()
    {
        var dados = GetDados();

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

    private DataTable GetDados()
    {
        DataTable dt = new DataTable();

        dt.TableName = "Dados Emprestimos";
        dt.Columns.Add("Recebedor", typeof(string));
        dt.Columns.Add("Fornecedor", typeof(string));
        dt.Columns.Add("Livro", typeof(string));
        dt.Columns.Add("Data Emprestimo", typeof(DateTime));
        
        var dados = _context.Emprestimos.ToList();

        if (dados.Count > 0)
        {
            foreach (var emprestimo in dados)
            {
                dt.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.Livro,
                    emprestimo.DataUltimaAtualizacao);
            }
        }
        return dt;
    }
}