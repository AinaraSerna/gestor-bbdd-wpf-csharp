using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym24_7.Modelo;
using Gym24_7.Servicios;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Gym24_7.Vistas.Usuarios
{
    class AñadirUsuarioVM : ObservableObject
    {
        public List<string> Roles { get; }
        private Usuario usuarioNuevo;
        public Usuario UsuarioNuevo
        {
            get => usuarioNuevo;
            set => SetProperty(ref usuarioNuevo, value);
        }
        public RelayCommand GuardarUsuarioCommand { get; }

        public AñadirUsuarioVM()
        {
            usuarioNuevo = new Usuario();
            Roles = ["Administrador", "Cliente"];
            GuardarUsuarioCommand = new RelayCommand(GuardarUsuario);
        }

        private void GuardarUsuario()
        {
            RestResponse? respuestaApi = UsuarioPost();
            if (respuestaApi != null)
            {
                Respuesta? r = JsonConvert.DeserializeObject<Respuesta>(respuestaApi!.Content!);
                MessageBoxService.GenerarMessageBox(r!.Mensaje);
                usuarioNuevo = new Usuario();
            }
        }

        private RestResponse? UsuarioPost()
        {
            RestClient cliente = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new("usuarios", Method.Post);

            ObservableCollection<Usuario>? usuarios = ApiRestService.GetUsuarios();
            RestResponse? response = null;

            if (usuarios == null || usuarios.Count == 0)
            {
                response = ApiRestService.InsertUsuario(cliente, request, usuarioNuevo);
            }
            else
            {
                if (ValidacionService.ComprobarCorreoDniTelefonoInsert(usuarios, usuarioNuevo))
                {
                    response = ApiRestService.InsertUsuario(cliente, request, usuarioNuevo);
                }
            }

            return response;
        }
    }
}
