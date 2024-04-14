using System.Security.Cryptography;
using System.Text;

namespace ApiAthanasia.Helpers
{
    public class HelperCryptography
    {
        public static byte[] EncryptPassword(string password, string salt)
        {
            string contenido = password + salt;
            SHA512 sha = SHA512.Create();
            byte[] salida = Encoding.UTF8.GetBytes(contenido);
            for (int i = 1; i <= 165; i++)
            {
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            return salida;
        }
    }
}
