using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym24_7.Modelo;
using Gym24_7.Servicios;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Gym24_7
{
    class MainWindowVM : ObservableObject
    {
        public RelayCommand VerInicioCommand { get; }
        public RelayCommand VerUsuariosCommand { get; }
        public RelayCommand VerEjerciciosCommand { get; }
        public RelayCommand AñadirUsuarioCommand { get; }
        public RelayCommand AñadirEjercicioCommand { get; }
        public RelayCommand AbrirAyudaCommand { get; }

        private Usuario usuarioLogin;
        public Usuario UsuarioLogin
        {
            get => usuarioLogin;
            set => SetProperty(ref usuarioLogin, value);
        }

        private UserControl opcionActual;
        public UserControl OpcionActual
        {
            get => opcionActual;
            set => SetProperty(ref opcionActual, value);
        }
        private readonly NavegacionService navegacionService;

        public MainWindowVM()
        {
            navegacionService = new NavegacionService();
            navegacionService.AbrirLogin();

            opcionActual = NavegacionService.GetInicio();
            VerInicioCommand = new RelayCommand(CargarInicio);
            VerUsuariosCommand = new RelayCommand(CargarUsuarios);
            VerEjerciciosCommand = new RelayCommand(CargarEjercicios);
            AñadirUsuarioCommand = new RelayCommand(CargarFormUsuario);
            AñadirEjercicioCommand = new RelayCommand(CargarFormEjercicio);
            AbrirAyudaCommand = new RelayCommand(CargarAyuda);
        }

        private void CargarAyuda()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = @"Gym24-7 Escritorio - Manual de usuario.chm",
                UseShellExecute = true
            });
        }

        private void CargarFormEjercicio() => OpcionActual = NavegacionService.GetFormEjercicio();

        private void CargarFormUsuario() => OpcionActual = NavegacionService.GetFormUsuario();

        private void CargarInicio() => OpcionActual = NavegacionService.GetInicio();

        private void CargarEjercicios() => OpcionActual = NavegacionService.GetEjercicios();

        private void CargarUsuarios() => OpcionActual = NavegacionService.GetUsuarios();
    }
}