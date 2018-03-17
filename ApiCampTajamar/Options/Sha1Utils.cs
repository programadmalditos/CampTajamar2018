using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCampTajamar.Options
{
    public class Sha1Utils
    {
        public static string GetSha1(string data)

        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ApplicationException("La cadena no puede ser nula");
            var buffer = Encoding.UTF8.GetBytes(data);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var hash = sha1.ComputeHash(buffer);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
