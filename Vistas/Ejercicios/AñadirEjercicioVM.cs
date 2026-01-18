using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym24_7.Modelo;
using Gym24_7.Servicios;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trivial.servicios;

namespace Gym24_7.Vistas.Ejercicios
{
    class AñadirEjercicioVM : ObservableObject
    {
        public RelayCommand GuardarEjercicioCommand { get; }
        public RelayCommand AbrirExaminarCommand { get; }

        private ListadoEjercicios ejercicioNuevo;
        public ListadoEjercicios EjercicioNuevo
        {
            get => ejercicioNuevo;
            set => SetProperty(ref ejercicioNuevo, value);
        }

        public AñadirEjercicioVM()
        {
            ejercicioNuevo = new ListadoEjercicios();
            GuardarEjercicioCommand = new RelayCommand(GuardarEjercicio);
            AbrirExaminarCommand = new RelayCommand(SetImagen);
        }

        private void SetImagen() => ejercicioNuevo.Imagen = OpenFileDialogService.AbrirDialogo();

        private void GuardarEjercicio()
        {
            ejercicioNuevo.Imagen = AzureService.GetImagenAzure(ejercicioNuevo.Imagen);
            RestResponse respuestaApi = ApiRestService.EjercicioPost(ejercicioNuevo);
            Respuesta? r = JsonConvert.DeserializeObject<Respuesta>(respuestaApi!.Content!);
            MessageBoxService.GenerarMessageBox(r!.Mensaje);
            ejercicioNuevo = new ListadoEjercicios();
        }
    }
}
