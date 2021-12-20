using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Autenticacao.CheckLogin(this);//verifica se existe alguem logado (this é o probrio controller/ dados desse metodo/pagina sendo enviados para o metodo invocado)
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            //if(login != "admin" || senha != "123") inaceitavel senha e login dentro do proprio codigo;
            if (Autenticacao.verificaLoginSenha(login, senha, this))
            {
                return View("Index");
            }
            else
            {
                ViewData["Erro"] = "Senha inválida";
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
