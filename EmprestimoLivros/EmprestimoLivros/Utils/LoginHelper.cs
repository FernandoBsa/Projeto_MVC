using System.Security.Cryptography;
using System.Text;
using EmprestimoLivros.Models.Entity;
using Newtonsoft.Json;

namespace EmprestimoLivros.Utils;

public static class LoginHelper
{
    private static readonly IHttpContextAccessor _httpContextAccessor;
    public static void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            senhaSalt = hmac.Key;
            senhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }
    }

    public static bool VerificaSenha(string senha, byte[] senhaHash, byte[] senhaSalt)
    {
        using (var hmac = new HMACSHA512(senhaSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return computedHash.SequenceEqual(senhaHash);
        }
    }
}