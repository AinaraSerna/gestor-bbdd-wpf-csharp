using Gym24_7.Modelo;
using Gym24_7.VerUsuarios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gym24_7.Servicios
{
    class ValidacionService
    {
        public static bool ComprobarCorreoDniTelefonoInsert(ObservableCollection<Usuario> usuarios, Usuario usuarioNuevo)
        {
            MessageBoxImage imageError = MessageBoxImage.Error;
            bool cambio = true;
            foreach (Usuario us in usuarios)
            {
                if (us.Correo == usuarioNuevo.Correo)
                {
                    MessageBoxService.GenerarMessageBox("Ya existe un usuario con el correo '" + us.Correo + "'", imageError);
                    cambio = false;
                    break;
                }
                else if (us.Dni == usuarioNuevo.Dni)
                {
                    MessageBoxService.GenerarMessageBox("Ya existe un usuario con el DNI '" + us.Dni + "'", imageError);
                    cambio = false;
                    break;
                }
                else if (us.Telefono == usuarioNuevo.Telefono)
                {
                    MessageBoxService.GenerarMessageBox("Ya existe un usuario con el nº de teléfono '" + us.Telefono + "'", imageError);
                    cambio = false;
                    break;
                }
            }
            return cambio;
        }

        public static bool ComprobarCorreoDniTelefonoUpdate(ObservableCollection<Usuario> usuarios, Usuario usuarioEditar, string correoOriginal, string dniOriginal, long? telefonoOriginal) 
        {
            MessageBoxImage imageError = MessageBoxImage.Error;
            bool cambio = true;
            if (usuarioEditar.Correo != correoOriginal)
            {
                foreach (Usuario us in usuarios)
                {
                    if (us.Correo == usuarioEditar.Correo)
                    {
                        MessageBoxService.GenerarMessageBox("Ya existe un usuario con el correo '" + us.Correo + "'", imageError);
                        cambio = false;
                        break;
                    }
                }
            }
            if (cambio && usuarioEditar.Dni != dniOriginal)
            {
                foreach (Usuario us in usuarios)
                {
                    if (us.Dni == usuarioEditar.Dni)
                    {
                        MessageBoxService.GenerarMessageBox("Ya existe un usuario con el DNI '" + us.Dni + "'", imageError);
                        cambio = false;
                        break;
                    }
                }
            }
            if (cambio && usuarioEditar.Telefono != telefonoOriginal)
            {
                foreach (Usuario us in usuarios)
                {
                    if (us.Telefono == usuarioEditar.Telefono)
                    {
                        MessageBoxService.GenerarMessageBox("Ya existe un usuario con el nº de teléfono '" + us.Telefono + "'", imageError);
                        cambio = false;
                        break;
                    }
                }
            }

            return cambio;
        }
    }
}
