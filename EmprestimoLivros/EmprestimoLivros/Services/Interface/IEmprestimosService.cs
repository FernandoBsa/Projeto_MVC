using System.Data;
using EmprestimoLivros.Models.Entities;
using EmprestimoLivros.Models.Response;

namespace EmprestimoLivros.Services.Interface;

public interface IEmprestimosService
{
    Task<ResponseModel<List<EmprestimosModel>>> BuscarEmprestimos();
    Task<ResponseModel<EmprestimosModel>> BuscarEmprestimoPorId(Guid? id);
    Task<DataTable> BuscaDadosEmprestimoExcel();
    Task<ResponseModel<EmprestimosModel>> CadastrarEmprestimo(EmprestimosModel emprestimosModel);
    Task<ResponseModel<EmprestimosModel>> EditarEmprestimo(EmprestimosModel emprestimosModel);
    Task<ResponseModel<EmprestimosModel>> RemoverEmprestimo(EmprestimosModel emprestimosModel);
}