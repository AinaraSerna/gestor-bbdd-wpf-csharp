using Gym24_7.Modelo;
using Gym24_7.Sesion;
using Gym24_7.VentanasEdicion;
using Gym24_7.VerEjercicios;
using Gym24_7.VerUsuarios;
using Gym24_7.Vistas.Ejercicios;
using Gym24_7.Vistas.Inicio;
using Gym24_7.Vistas.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Gym24_7.Servicios
{
    class NavegacionService
    {
        private InicioSesion? inicioSesion;
        private EditarUsuario? editarUsuario;
        private EditarEjercicio? editarEjercicio;

        public static UserControl GetInicio() => new Inicio();
        public static UserControl GetUsuarios() => new Usuarios();
        public static UserControl GetEjercicios() => new Ejercicios();
        public static UserControl GetFormUsuario() => new AñadirUsuario();
        public static UserControl GetFormEjercicio() => new AñadirEjercicio();

        public void AbrirLogin()
        {
            inicioSesion = new InicioSesion();
            inicioSesion.ShowDialog();
        }

        public void AbrirEditarUsuario(string correo) 
        {
            editarUsuario = new EditarUsuario(correo);
            editarUsuario.ShowDialog();
        }

        internal void AbrirEditarEjercicio(int id)
        {
            editarEjercicio = new EditarEjercicio(id);
            editarEjercicio.ShowDialog();
        }
    }
}
