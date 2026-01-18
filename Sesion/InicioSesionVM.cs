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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gym24_7.Sesion
{
    class InicioSesionVM : ObservableObject
    {
        public RelayCommand IniciarSesionCommand { get; }

        private bool loginCorrecto;
        public bool LoginCorrecto
        {
            get => loginCorrecto;
            set => SetProperty(ref loginCorrecto, value);
        }

        private string correoLogin;
        public string CorreoLogin
        {
            get => correoLogin;
            set => SetProperty(ref correoLogin, value);
        }
        private string contraseñaLogin;
        public string ContraseñaLogin
        {
            get => contraseñaLogin;
            set => SetProperty(ref contraseñaLogin, value);
        }
        private string mensajeError;
        public string MensajeError
        {
            get => mensajeError;
            set => SetProperty(ref mensajeError, value);
        }

        public InicioSesionVM()
        {
            correoLogin = contraseñaLogin = mensajeError = string.Empty;
            IniciarSesionCommand = new RelayCommand(ComprobarDatos);
            loginCorrecto = false;
        }

        private bool ComprobarCorreo()
        {
            RegexOptions regexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;
            string patronCorreo = @"^[\w.%-]+@[\w.-]+\.[a-zA-Z]{2,6}$";
            return Regex.IsMatch(correoLogin, patronCorreo, regexOptions);
        }

        private void ComprobarDatos()
        {
            if (CorreoLogin.Equals(string.Empty) || ContraseñaLogin.Equals(string.Empty))
            {
                MensajeError = "Rellene el formulario para iniciar sesión";
            }
            else if (!ComprobarCorreo())
            {
                MensajeError = "El campo 'correo electrónico' debe contener un correo válido.";
            }
            else
            {
                Usuario? usuario = ApiRestService.GetUsuarioByCorreo(correoLogin);
                string hashCalculado = HashService.GenerarHash(correoLogin, contraseñaLogin);
                MensajeError = usuario switch
                {
                    null => "No existe ningún usuario con correo '" + CorreoLogin + "'", // Si no ha encontrado ningún usuario con el correo introducido
                       _ => !HashService.CompararHashes(usuario.Contraseña, hashCalculado)
                                 ? "Las credenciales no son correctas" // Si los hashes de la BD y el calculado son distintos
                                 : !usuario.Rol.Equals("Administrador")
                                              ? "Solo los usuarios administradores pueden iniciar sesión" // Si el usuario es de tipo 'Cliente'
                                              : string.Empty, // Si la validación es correcta
                };
                if (MensajeError.Equals(string.Empty))
                {
                    LoginCorrecto = true;
                }
            }
        }
    }
}
