using Microsoft.AspNetCore.Mvc;
using PrimeiraCrudMVC.Filters;
using PrimeiraCrudMVC.Helper;
using PrimeiraCrudMVC.Models;
using PrimeiraCrudMVC.Repositorio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PrimeiraCrudMVC.Controllers
{
    [PaginaParaUsuarioLogado]

    public class ContatoController : Controller
    { 

        private readonly IContatoRepositorio _contatorepositorio;
        private readonly ISessao _sessao;
        public ContatoController(IContatoRepositorio contatorepositorio,
                                 ISessao sessao)
        {
            _contatorepositorio = contatorepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<ContatoModel> contatos = _contatorepositorio.BuscarTodos(usuarioLogado.Id);
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
           ContatoModel contato = _contatorepositorio.ListaPorId(id);
            return View(contato);
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel contato = _contatorepositorio.ListaPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatorepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = $"Não conseguimos apagar seu contato, tente novamente!";
                }

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos apagar seu contato, tente novamente! Detalhe do erro: {erro.Message} ";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato) {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;
                    contato = _contatorepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos cadastrar seu contato, tente novamente! Detalhe do erro: {erro.Message} ";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;
                    _contatorepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);

            }
            catch (Exception erro) {
                TempData["MensagemErro"] = $"Não conseguimos atualizar seu contato, tente novamente! Detalhe do erro: {erro.Message} ";

                return RedirectToAction("Index");
            }
           
        }
    }
}
