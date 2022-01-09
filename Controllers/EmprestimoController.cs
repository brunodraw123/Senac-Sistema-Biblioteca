using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace Biblioteca.Controllers
{

    public class EmprestimoController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);//enviando dados da sessao desse controle para fazer validação.
            LivroService livroService = new LivroService();
            EmprestimoService emprestimoService = new EmprestimoService();

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            //cadModel.Livros = livroService.ListarTodos();
            cadModel.Livros = livroService.ListarDisponiveis();
            return View(cadModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CadEmprestimoViewModel viewModel)
        {
            EmprestimoService emprestimoService = new EmprestimoService();

            if (viewModel.Emprestimo.Id == 0)
            {
                emprestimoService.Inserir(viewModel.Emprestimo);
            }
            else
            {
                emprestimoService.Atualizar(viewModel.Emprestimo);
            }
            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, string itensPorPagina, int numDaPagina, int paginaAtual)
        {
            Autenticacao.CheckLogin(this);//enviando dados da sessao desse controle para fazer validação.

            FiltrosEmprestimos objFiltro = null;
            if (!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosEmprestimos();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }
            ViewData["livrosPorPagina"] = (string.IsNullOrEmpty(itensPorPagina) ? 10 : Int32.Parse(itensPorPagina));//object que passa valor int;
            ViewData["paginaAtual"] = (paginaAtual != 0 ? paginaAtual : 1);
            EmprestimoService emprestimoService = new EmprestimoService();
            return View(emprestimoService.ListarTodos(objFiltro));
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);//enviando dados da sessao desse controle para fazer validação.

            LivroService livroService = new LivroService();
            EmprestimoService em = new EmprestimoService();
            Emprestimo e = em.ObterPorId(id);

            CadEmprestimoViewModel cadModel = new CadEmprestimoViewModel();
            cadModel.Livros = livroService.ListarDisponiveis();
            cadModel.Emprestimo = e;

            return View(cadModel);
        }
    }
}