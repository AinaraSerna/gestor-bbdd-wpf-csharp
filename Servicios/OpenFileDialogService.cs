using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym24_7.Servicios
{
    class OpenFileDialogService
    {
        public static string AbrirDialogo()
        {
            string ruta = string.Empty;
            OpenFileDialog ofd = new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "Fichero de imagen (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };
            if (ofd.ShowDialog() == true)
            {
                ruta = Path.GetFullPath(ofd.FileName);
            }
            return ruta;
        }
    }
}
