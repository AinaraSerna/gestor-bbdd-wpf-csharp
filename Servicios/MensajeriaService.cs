using CommunityToolkit.Mvvm.Messaging.Messages;
using Gym24_7.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym24_7.Servicios
{
    class EditarUsuarioMessage : ValueChangedMessage<Usuario>
    {
        public EditarUsuarioMessage(Usuario usuarioEditar) : base(usuarioEditar) { }
    }
}
