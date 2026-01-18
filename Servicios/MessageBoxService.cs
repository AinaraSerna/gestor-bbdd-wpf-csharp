using Gym24_7.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gym24_7.Servicios
{
    class MessageBoxService
    {
        public static void GenerarMessageBox(string mensaje, MessageBoxImage imagen = MessageBoxImage.Information)
        {
            MessageBox.Show(mensaje, "Mensaje", MessageBoxButton.OK, imagen);
        }
    }
}
