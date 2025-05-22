using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GUI
{
    public static class UserControl
    {
        private static readonly string validUsername = "admin";
        private static readonly string validPassword = "admin123";

        public static bool loginuser(string username, string password)
        {
            // Verificar si las credenciales son correctas
            return username == validUsername && password == validPassword;
        }
    }
}

