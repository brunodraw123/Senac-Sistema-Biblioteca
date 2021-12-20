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
    public class UsuarioController : Controller
    {
        //Lista
        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            UsuarioService Lista = new UsuarioService();

            return View(Lista.Listar());
        }//Insercao
        public IActionResult RegistrarUsuario()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }
        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario u)
        {
            UsuarioService us = new UsuarioService();
            u.Senha = Criptografo.TextoCriptografado(u.Senha);
            us.incluirUsuario(u);
            return RedirectToAction("ListaDeUsuarios");
        }
        //Edicao
        public IActionResult EditarUsuario(int id)
        {
            Autenticacao.CheckLogin(this);//veririca se tem usuario logado
            Autenticacao.verificaSeUsuarioEAdmin(this);//verifica se esse usuario é o adm. Caso nao, ele vai p/ login
            Usuario u = new UsuarioService().ListarPorId(id);
            return View(u);//Joga para a view esse usario encontrado por id;
        }
        [HttpPost]
        public IActionResult EditarUsuario(Usuario usarioEdit)
        {
            Autenticacao.CheckLogin(this);//veririca se tem usuario logado
            Autenticacao.verificaSeUsuarioEAdmin(this);
            UsuarioService us = new UsuarioService();
            us.editarUsuario(usarioEdit);
            return RedirectToAction("ListaDeUsuarios");
        }

        //Exclusao
        public IActionResult ExcluirUsuario(int id)
        {
            Autenticacao.CheckLogin(this);//veririca se tem usuario logado
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View(new UsuarioService().ListarPorId(id));
        }
        //Funcoes_Extras: 
        [HttpPost]
        public IActionResult ExcluirUsuario(string Decisao, int Id)
        {
            if (Decisao.ToLower() == "excluir")
            {
                ViewData["Mensagem"] = "Exclusão do usuário " + new UsuarioService().ListarPorId(Id).Nome + " realiza com sucesso";
                new UsuarioService().excluirUsuario(Id);
                return RedirectToAction("ListaDeUsuarios");
            }
            else
            {
                ViewData["Mensagem"] = "Exclusao cancelada";
                return RedirectToAction("ListaDeUsuarios");
            }
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return View("../Home/Login");
        }
        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            //Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }
    }
}
