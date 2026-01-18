using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gym24_7.Modelo;
using Gym24_7.Servicios;
using Gym24_7.VerUsuarios;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gym24_7.VentanasEdicion
{
    class EditarUsuarioVM : ObservableObject
    {

        public List<string> Roles { get; }
        public RelayCommand EditarUsuarioCommand { get; }
        private Usuario usuarioEditar;
        public Usuario UsuarioEditar
        {
            get => usuarioEditar;
            set => SetProperty(ref usuarioEditar, value);
        }

        private readonly string dniOriginal;
        private readonly string correoOriginal;
        private readonly long? telefonoOriginal;
        private readonly string contraseñaOriginal;

        public EditarUsuarioVM(string correo)
        {
            Roles = ["Administrador", "Cliente"];
            usuarioEditar = ApiRestService.GetUsuarioByCorreo(correo)!;
            dniOriginal = usuarioEditar.Dni;
            correoOriginal = usuarioEditar.Correo;
            telefonoOriginal = usuarioEditar.Telefono;
            contraseñaOriginal = usuarioEditar.Contraseña;

            EditarUsuarioCommand = new RelayCommand(ActualizarUsuario);
        }

        private void ActualizarUsuario()
        {
            RestResponse? respuestaApi = UsuarioPut();
            if (respuestaApi != null)
            {
                Respuesta? r = JsonConvert.DeserializeObject<Respuesta>(respuestaApi.Content!);
                MessageBoxService.GenerarMessageBox(r!.Mensaje);
                usuarioEditar = new Usuario();
            }
        }

        private RestResponse? UsuarioPut()
        {
            RestClient cliente = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new("usuarios", Method.Put);

            ObservableCollection<Usuario>? usuarios = ApiRestService.GetUsuarios();
            RestResponse? response = null;

            if (usuarios == null || usuarios.Count == 0)
            {
                response = ApiRestService.UpdateUsuario(cliente, request, usuarioEditar, contraseñaOriginal, correoOriginal);
            }
            else
            {
                if (ValidacionService.ComprobarCorreoDniTelefonoUpdate(usuarios, usuarioEditar, correoOriginal, dniOriginal, telefonoOriginal))
                {
                    response = ApiRestService.UpdateUsuario(cliente, request, usuarioEditar, contraseñaOriginal, correoOriginal);
                }
            }
            return response;
        }
    }
}
