using System;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public class Criptografo
    {
        public static string TextoCriptografado(string textoSemFormatacao)
        {
            MD5 MD5Hasher = MD5.Create();
            byte[] By = Encoding.Default.GetBytes(textoSemFormatacao);//passa o texto passa "array de bytes";
            byte[] bytesCriptografado = MD5Hasher.ComputeHash(By);//passa o array de bytes para a criptografia de fato (ainda em byte);

            StringBuilder SB = new StringBuilder();
            foreach (byte b in bytesCriptografado)//conversao de byte p/ texto;
            {
                string DebugB = b.ToString("x2");
                SB.Append(DebugB);  

            }
            return SB.ToString();
        }
    }
}