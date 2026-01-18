using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym24_7.Modelo
{
    public class Usuario : ObservableObject
    {
        private int id;
        private string rol;
        private string nombre;
        private string dni;
        private string correo;
        private long? telefono;
        private string contraseña;

        public int Id 
        { 
            get => id; 
            set => SetProperty(ref id, value);
        }
        public string Rol
        {
            get => rol;
            set => SetProperty(ref rol, value);
        }
        public string Nombre
        {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }

        public string Dni
        {
            get => dni;
            set => SetProperty(ref dni, value);
        }

        public string Correo
        {
            get => correo;
            set => SetProperty(ref correo, value);
        }

        public long? Telefono
        {
            get => telefono;
            set => SetProperty(ref telefono, value);
        }

        public string Contraseña
        {
            get => contraseña;
            set => SetProperty(ref contraseña, value);
        }

        public Usuario()
        {
            id = 0;
            nombre = dni = correo = contraseña = rol = string.Empty;
            telefono = null;
        }

        public Usuario(int id, string rol, string nombre, string dni, string correo, long telefono, string contraseña)
        {
            this.id = id;
            this.rol = rol;
            this.nombre = nombre;
            this.dni = dni;
            this.correo = correo;
            this.telefono = telefono;
            this.contraseña = contraseña;
        }

        public Usuario(string rol, string nombre, string dni, string correo, long telefono, string contraseña)
        {
            this.rol = rol;
            this.nombre = nombre;
            this.dni = dni;
            this.correo = correo;
            this.telefono = telefono;
            this.contraseña = contraseña;
        }
    }
}
