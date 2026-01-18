using Gym24_7.Vistas.Ejercicios;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gym24_7.VerEjercicios
{
    /// <summary>
    /// Lógica de interacción para Ejercicios.xaml
    /// </summary>
    public partial class Ejercicios : UserControl
    {
        private VerEjerciciosVM vm;
        public Ejercicios()
        {
            InitializeComponent();
            vm = new VerEjerciciosVM();
            DataContext = vm;
        }
    }
}
