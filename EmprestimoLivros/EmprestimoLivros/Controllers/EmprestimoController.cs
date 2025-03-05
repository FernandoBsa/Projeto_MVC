using EmprestimoLivros.Data.Context;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimoLivros.Controllers;

public class EmprestimoController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public EmprestimoController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        IEnumerable<EmprestimosModel> emprestimos = _context.Emprestimos;
        return View(emprestimos);
    }
    
    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(EmprestimosModel emprestimo)
    {
        if (ModelState.IsValid)
        {
            _context.Emprestimos.Add(emprestimo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View("Cadastrar");
    }
    
    
    [HttpGet]
    public IActionResult Editar(Guid id)
    {
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
            _context.Emprestimos.Update(emprestimo);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(emprestimo);
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
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

        return RedirectToAction("Index");
    }
}