using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym24_7.Modelo;
using Gym24_7.Servicios;
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

namespace Gym24_7.VentanasEdicion
{
    class EditarEjercicioVM : ObservableObject
    {
        public RelayCommand EditarEjercicioCommand { get; }
        public RelayCommand AbrirExaminarCommand { get; }

        private ListadoEjercicios ejercicioEditar;
        public ListadoEjercicios EjercicioEditar
        {
            get => ejercicioEditar;
            set => SetProperty(ref ejercicioEditar, value);
        }
        private string ImagenPrimera { get; set; }

        public EditarEjercicioVM(int id)
        {
            ejercicioEditar = ApiRestService.GetEjercicioById(id)!.First();
            EditarEjercicioCommand = new RelayCommand(ActualizarEjercicio);
            AbrirExaminarCommand = new RelayCommand(SetImagen);
            ImagenPrimera = ejercicioEditar.Imagen;
        }

        private void SetImagen()
        {
            EjercicioEditar.Imagen = OpenFileDialogService.AbrirDialogo();
        }

        private void ActualizarEjercicio()
        {
            if (!ImagenPrimera.Equals(EjercicioEditar.Imagen))
            {
                AzureService.DeleteImagenAzure(ImagenPrimera);
                EjercicioEditar.Imagen = AzureService.GetImagenAzure(ejercicioEditar.Imagen);
            }

            RestResponse respuestaApi = ApiRestService.EjercicioPut(ejercicioEditar);
            Respuesta? r = JsonConvert.DeserializeObject<Respuesta>(respuestaApi!.Content!);
            MessageBoxService.GenerarMessageBox(r!.Mensaje);
        }
    }
}
