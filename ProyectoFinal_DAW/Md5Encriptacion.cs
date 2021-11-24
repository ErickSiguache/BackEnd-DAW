using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_DAW
{
    public class Md5Encriptacion
    {
        public string GetMD5(string str)
        {
            ///Se llama el metodo de conversion md5
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                ///Se declara un arreglo de tipo byte para representar valores que van de 0 a 250
                ///Que luego se reemplanzan por medio de encoding.ascii.getbytes con el cual 
                ///codifica todos los caracteres de la cadena especificada en una secuencia de bytes.
                byte[] data = Encoding.ASCII.GetBytes(str);
                ///Se encarga de calcula el valor hash del valor codificado en secuencia de bytes
                byte[] hash = md5.ComputeHash(data);
                ///Se recorna con bitconverter a md5 pero dado que la conversion de bits es un arreglo
                ///Quedaria (AB-CD) y por ello se le especifica que reemplace los (-) a modo que queden
                ///ABCD y luego retorna la informacion
                ///AD-AS
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
