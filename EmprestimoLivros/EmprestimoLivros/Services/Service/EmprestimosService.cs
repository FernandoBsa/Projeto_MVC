using System.Data;
using EmprestimoLivros.Data.Context;
using EmprestimoLivros.Models.Entities;
using EmprestimoLivros.Models.Response;
using EmprestimoLivros.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLivros.Services.Service;

public class EmprestimosService : IEmprestimosService
{
    private readonly ApplicationDbContext _context;

    public EmprestimosService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<List<EmprestimosModel>>> BuscarEmprestimos()
    {
        ResponseModel<List<EmprestimosModel>> response = new ResponseModel<List<EmprestimosModel>>();
        
        try
        {
            var emprestimos = await _context.Emprestimos.ToListAsync();

            response.Dados = emprestimos;
            response.Mensagem = "Dados coletados com sucesso!";

            return response;
        }
        catch (Exception e)
        {
            response.Mensagem = e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<EmprestimosModel>> BuscarEmprestimoPorId(Guid? id)
    {
        ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();
        
        try
        {
            if (id == null)
            {
                response.Mensagem = "Emprestimo nao localizado.";
                response.Status = false;
                return response;
            }
            
            var emprestimo = await _context.Emprestimos.FirstOrDefaultAsync(x => x.Id == id);

            if (emprestimo == null)
            {
                response.Mensagem = "Emprestimo nao localizado.";
                response.Status = false;
                return response;
            }
            
            response.Dados = emprestimo;
            response.Mensagem = "Emprestimo encontrado com sucesso!";

            return response;
        }
        catch (Exception e)
        {
            response.Mensagem = e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<DataTable> BuscaDadosEmprestimoExcel()
    {
        DataTable dt = new DataTable();

        dt.TableName = "Dados Emprestimos";
        dt.Columns.Add("Recebedor", typeof(string));
        dt.Columns.Add("Fornecedor", typeof(string));
        dt.Columns.Add("Livro", typeof(string));
        dt.Columns.Add("Data Emprestimo", typeof(DateTime));

        var dados = await BuscarEmprestimos();

        if (dados.Dados.Count > 0)
        {
            foreach (var emprestimo in dados.Dados)
            {
                dt.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.Livro,
                    emprestimo.DataUltimaAtualizacao);
            }
        }
        
        return dt;
    }

    public async Task<ResponseModel<EmprestimosModel>> CadastrarEmprestimo(EmprestimosModel emprestimosModel)
    {
        ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();

        try
        {
            _context.Add(emprestimosModel);
            await _context.SaveChangesAsync();
            
            response.Mensagem = "Emprestimo cadastrado com sucesso!";
            return response;
        }
        catch (Exception e)
        {
            response.Mensagem = e.Message;
            response.Status = false;

            return response;
        }
    }

    public async Task<ResponseModel<EmprestimosModel>> EditarEmprestimo(EmprestimosModel emprestimosModel)
    {
        ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();
        try
        {
            var emprestimo = await BuscarEmprestimoPorId(emprestimosModel.Id);
            
            if (!emprestimo.Status)
            {
                return emprestimo;
            }
            
            emprestimo.Dados.Livro = emprestimosModel.Livro;
            emprestimo.Dados.Fornecedor = emprestimosModel.Fornecedor;
            emprestimo.Dados.Recebedor = emprestimosModel.Recebedor;

            _context.Update(emprestimo.Dados);
            await _context.SaveChangesAsync();

            response.Mensagem = "Edição realizada com sucesso!";
            
            return response;
        }
        catch (Exception e)
        {
            response.Mensagem = e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<EmprestimosModel>> RemoverEmprestimo(EmprestimosModel emprestimosModel)
    {
        ResponseModel<EmprestimosModel> response = new ResponseModel<EmprestimosModel>();

        try
        {
            _context.Remove(emprestimosModel);
            await _context.SaveChangesAsync();
            
            response.Mensagem = "Emprestimo removido com sucesso!";

            return response;
        }
        catch (Exception e)
        {
            response.Mensagem = e.Message;
            response.Status = false;

            return response;
        }
    }
}