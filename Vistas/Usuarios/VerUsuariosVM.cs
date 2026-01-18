using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gym24_7.Modelo;
using Gym24_7.Servicios;
using Newtonsoft.Json;
using RestSharp;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace Gym24_7.VerUsuarios
{
    class VerUsuariosVM : ObservableObject
    {
        public RelayCommand EditarUsuarioCommand { get; }
        public RelayCommand EliminarUsuarioCommand { get; }
        private NavegacionService navegacionService;

        private ObservableCollection<Usuario>? listaUsuarios;
        public ObservableCollection<Usuario>? ListaUsuarios
        {
            get => listaUsuarios;
            set => SetProperty(ref listaUsuarios, value);
        }
        private Usuario? usuarioSeleccionado;
        public Usuario? UsuarioSeleccionado
        {
            get => usuarioSeleccionado;
            set => SetProperty(ref usuarioSeleccionado, value);
        }


        public ObservableCollection<string> OpcionesComboBox { get; }
        private string opcionSeleccionada;
        public string OpcionSeleccionada
        {
            get => opcionSeleccionada;
            set
            {
                SetProperty(ref opcionSeleccionada, value);
                SetLista();
            }
        }

        private void SetLista()
        {
            ListaUsuarios = OpcionSeleccionada switch
            {
                "Todos" => ApiRestService.GetUsuarios(),
                "Administradores" => ApiRestService.GetUsuariosByRol("administrador"),
                "Clientes" => ApiRestService.GetUsuariosByRol("cliente"),
                _ => null
            };
        }

        public VerUsuariosVM()
        {
            OpcionesComboBox = ["Todos", "Administradores", "Clientes"];
            opcionSeleccionada = OpcionesComboBox.First();

            ListaUsuarios = ApiRestService.GetUsuarios();
            navegacionService = new NavegacionService();

            EditarUsuarioCommand = new RelayCommand(EditarUsuarioSeleccionado);
            EliminarUsuarioCommand = new RelayCommand(EliminarUsuarioSeleccionado);
        }

        private void EditarUsuarioSeleccionado()
            => navegacionService.AbrirEditarUsuario(usuarioSeleccionado!.Correo);

        private void EliminarUsuarioSeleccionado()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"¿Seguro que quiere borrar el usuario '{usuarioSeleccionado!.Nombre}'?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                RestResponse respuestaApi = ApiRestService.EliminarUsuario(usuarioSeleccionado.Id);
                Respuesta? r = JsonConvert.DeserializeObject<Respuesta>(respuestaApi.Content!);
                MessageBoxService.GenerarMessageBox(r!.Mensaje);
                ListaUsuarios = ApiRestService.GetUsuarios();
            }
        }
    }
}
