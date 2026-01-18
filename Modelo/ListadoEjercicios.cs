using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym24_7.Modelo
{
    public class ListadoEjercicios : ObservableObject
    {
        private int id;
        private string nombre;
        private string imagen;
        private string informacion;

        public int Id 
        { 
            get => id; 
            set => SetProperty(ref id, value);
        }
        public string Nombre
        {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }

        public string Imagen
        {
            get => imagen;
            set => SetProperty(ref imagen, value);
        }

        public string Informacion
        {
            get => informacion;
            set => SetProperty(ref informacion, value);
        }

        public ListadoEjercicios()
        {
            id = 0;
            nombre = imagen = informacion = string.Empty;
        }

        public ListadoEjercicios(int id, string nombre, string imagen, string informacion)
        {
            this.id = id;
            this.nombre = nombre;
            this.imagen = imagen;
            this.informacion = informacion;
        }
    }
}
