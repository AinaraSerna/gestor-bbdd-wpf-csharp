using Gym24_7.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gym24_7.VentanasEdicion
{
    /// <summary>
    /// Lógica de interacción para EditarEjercicio.xaml
    /// </summary>
    public partial class EditarEjercicio : Window
    {
        private readonly EditarEjercicioVM vm;
        public EditarEjercicio(int id)
        {
            InitializeComponent();
            vm = new EditarEjercicioVM(id);
            DataContext = vm;
        }
    }
}
