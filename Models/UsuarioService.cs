using System.Collections.Generic;
using System.Linq;


namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public Usuario ListarPorId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {//Find ->retorna um objeto != ToList -> retorna varios objetos;
                return bc.Usuarios.Find(id);
            }
        }

        public List<Usuario> Listar()
        { //read
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
        }
        public void incluirUsuario(Usuario u)
        {//insert
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Add(u); //reconhecimento automatico da tabela p/ add por conta do formato do usuario
                bc.SaveChanges();//salva mudanças
            }
        }
        public void editarUsuario(Usuario EditU)
        {//update
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario achadoU = ListarPorId(EditU.Id);//usuario antigo achado c/ base no id enviado user novo(msm id)
                achadoU.Login = EditU.Login;
                achadoU.Nome = EditU.Nome;
                achadoU.Senha = EditU.Senha;
                achadoU.Tipo = EditU.Tipo;
                bc.SaveChanges();

            }
        }
        public void excluirUsuario(int id)
        {//drop
            using (BibliotecaContext bc = new BibliotecaContext())
            {

                bc.Usuarios.Remove(ListarPorId(id));
                bc.SaveChanges();
            }
        }
    }
}