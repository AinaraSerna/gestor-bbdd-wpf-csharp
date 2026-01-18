using Gym24_7.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gym24_7.Servicios
{
    class ApiRestService
    {
        // Sustituir 'UrlBase' por 'UrlBase' para probar en local
        public static ObservableCollection<Usuario>? GetUsuarios()
        {
            RestClient client = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new("usuarios", Method.Get);
            RestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Usuario>>(response.Content!);
        }

        public static ObservableCollection<Usuario>? GetUsuariosByRol(string rol)
        {
            RestClient client = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new($"/usuarios/rol/{rol}", Method.Get);
            RestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Usuario>>(response.Content!);
        }

        public static Usuario? GetUsuarioByCorreo(string correoLogin)
        {
            RestClient client = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new($"/usuarios/{correoLogin}", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            return JsonConvert.DeserializeObject<ObservableCollection<Usuario>>(response.Content!)?.First();
        }


        public static RestResponse InsertUsuario(RestClient cliente, RestRequest request, Usuario usuarioNuevo)
        {
            RestResponse? response;
            string valorContraseña = HashService.GenerarHash(usuarioNuevo.Correo, usuarioNuevo.Contraseña);
            usuarioNuevo.Contraseña = valorContraseña;

            string data = JsonConvert.SerializeObject(usuarioNuevo);
            data = data.Replace("Id", "id");
            data = data.Replace("Rol", "rol");
            data = data.Replace("Nombre", "nombre");
            data = data.Replace("Dni", "dni");
            data = data.Replace("Correo", "correo");
            data = data.Replace("Telefono", "telefono");
            data = data.Replace("Contraseña", "contraseña");
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            response = cliente.Execute(request);
            return response;
        }

        public static RestResponse UpdateUsuario(RestClient cliente, RestRequest request, Usuario usuarioEditar, string contraseñaOriginal, string correoOriginal)
        {
            // Si se cambia el campo 'Contraseña' o 'Correo', hay que calcular el nuevo hash
            if (usuarioEditar.Contraseña != contraseñaOriginal || usuarioEditar.Correo != correoOriginal)
            {
                string nuevoHash = HashService.GenerarHash(usuarioEditar.Correo, usuarioEditar.Contraseña);
                usuarioEditar.Contraseña = nuevoHash;
            }

            string data = JsonConvert.SerializeObject(usuarioEditar);
            data = data.Replace("Id", "id");
            data = data.Replace("Rol", "rol");
            data = data.Replace("Nombre", "nombre");
            data = data.Replace("Dni", "dni");
            data = data.Replace("Correo", "correo");
            data = data.Replace("Telefono", "telefono");
            data = data.Replace("Contraseña", "contraseña");
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            RestResponse response = cliente.Execute(request);
            return response;
        }


        public static RestResponse EliminarUsuario(int id)
        {
            RestClient client = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new($"/usuarios/{id}", Method.Delete);
            RestResponse response = client.Execute(request);
            return response;
        }

        public static ObservableCollection<ListadoEjercicios>? GetEjercicios()
        {
            RestClient client = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new("lista_ejercicios", Method.Get);
            RestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<ListadoEjercicios>>(response.Content!);
        }

        public static ObservableCollection<ListadoEjercicios>? GetEjercicioById(int id)
        {
            RestClient client = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new($"lista_ejercicios/{id}", Method.Get);
            RestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<ListadoEjercicios>>(response.Content!);
        }

        public static RestResponse EjercicioPost(ListadoEjercicios ejercicioNuevo)
        {
            RestClient cliente = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new("lista_ejercicios", Method.Post);
            string data = JsonConvert.SerializeObject(ejercicioNuevo);
            data = data.Replace("Id", "id");
            data = data.Replace("Nombre", "nombre");
            data = data.Replace("Imagen", "imagen");
            data = data.Replace("Informacion", "informacion");
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            RestResponse response = cliente.Execute(request);
            return response;
        }

        public static RestResponse EjercicioPut(ListadoEjercicios ejercicioEditar)
        {
            RestClient cliente = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new("lista_ejercicios", Method.Put);
            string data = JsonConvert.SerializeObject(ejercicioEditar);
            data = data.Replace("Id", "id");
            data = data.Replace("Nombre", "nombre");
            data = data.Replace("Imagen", "imagen");
            data = data.Replace("Informacion", "informacion");
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            RestResponse response = cliente.Execute(request);
            return response;
        }

        public static RestResponse EliminarEjercicio(int id)
        {
            RestClient client = new(Properties.Settings.Default.UrlBase);
            RestRequest request = new($"/lista_ejercicios/{id}", Method.Delete);
            RestResponse response = client.Execute(request);
            return response;
        }
    }
}
