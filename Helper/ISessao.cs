using PrimeiraCrudMVC.Controllers;
using PrimeiraCrudMVC.Models;

namespace PrimeiraCrudMVC.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        void RemoverSessaoDoUsuario();

        UsuarioModel BuscarSessaoDoUsuario();

           
    }
}
