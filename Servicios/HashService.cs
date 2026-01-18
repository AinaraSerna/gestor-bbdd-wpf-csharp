using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Gym24_7.Servicios
{
    class HashService
    {
        public static string GenerarHash(string correo, string contraseña)
        {
            string sSourceData = correo + "|" + contraseña;
            byte[] tmpSource = Encoding.ASCII.GetBytes(sSourceData);
            byte[] tmpHash = MD5.HashData(tmpSource);
            return ByteArrayToString(tmpHash);
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        public static bool CompararHashes(string hashObtenido, string hashCalculado)
        {
            bool iguales = false;
            if (hashCalculado.Length == hashObtenido.Length)
            {
                int i = 0;
                while ((i < hashObtenido.Length) && (hashCalculado[i] == hashObtenido[i]))
                {
                    i += 1;
                }
                if (i == hashCalculado.Length)
                {
                    iguales = true;
                }
            }
            return iguales;
        }
    }
}
