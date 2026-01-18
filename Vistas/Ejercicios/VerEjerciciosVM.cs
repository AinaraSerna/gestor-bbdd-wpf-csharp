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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trivial.servicios;

namespace Gym24_7.Vistas.Ejercicios
{
    class VerEjerciciosVM : ObservableObject
    {
        public RelayCommand EditarEjercicioCommand { get; }
        public RelayCommand EliminarEjercicioCommand { get; }
        private NavegacionService navegacionService;

        private ObservableCollection<ListadoEjercicios>? listaEjercicios;
        public ObservableCollection<ListadoEjercicios>? ListaEjercicios
        {
            get => listaEjercicios;
            set => SetProperty(ref listaEjercicios, value);
        }
        private ListadoEjercicios? ejercicioSeleccionado;
        public ListadoEjercicios? EjercicioSeleccionado
        {
            get => ejercicioSeleccionado;
            set => SetProperty(ref ejercicioSeleccionado, value);
        }
        public VerEjerciciosVM()
        {
            navegacionService = new NavegacionService();
            ListaEjercicios = ApiRestService.GetEjercicios();

            EditarEjercicioCommand = new RelayCommand(EditarEjercicioSeleccionado);
            EliminarEjercicioCommand = new RelayCommand(EliminarEjercicioSeleccionado);
        }

        private void EliminarEjercicioSeleccionado()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"¿Seguro que quiere borrar el ejercicio '{ejercicioSeleccionado!.Nombre}'?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                AzureService.DeleteImagenAzure(ejercicioSeleccionado!.Imagen);
                RestResponse respuestaApi = ApiRestService.EliminarEjercicio(ejercicioSeleccionado.Id);
                Respuesta? r = JsonConvert.DeserializeObject<Respuesta>(respuestaApi.Content!);
                MessageBoxService.GenerarMessageBox(r!.Mensaje);
                ListaEjercicios = ApiRestService.GetEjercicios();
            }
        }

        private void EditarEjercicioSeleccionado()
            => navegacionService.AbrirEditarEjercicio(ejercicioSeleccionado!.Id);
    }
}
