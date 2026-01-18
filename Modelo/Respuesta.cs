using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym24_7.Modelo
{
    class Respuesta(string mensaje) : ObservableObject
    {
        private string mensaje = mensaje;
        public string Mensaje
        {
            get => mensaje;
            set => SetProperty(ref mensaje, value);
        }
    }
}
