using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Gym24_7.Sesion
{
    public partial class InicioSesion : Window
    {
        private readonly InicioSesionVM vm;


        public InicioSesion()
        {
            InitializeComponent();
            vm = new InicioSesionVM();
            DataContext = vm;
            //Closing += CerrarVentana_Click;
        }

        private void InicioSesionButton_Click(object sender, RoutedEventArgs e)
        {
            if (vm.LoginCorrecto)
            {
                DialogResult = true;
            }
        }

        private void CerrarVentana_Click(object? sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
