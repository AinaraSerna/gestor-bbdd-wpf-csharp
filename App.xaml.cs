using System.Configuration;
using System.Data;
using System.Windows;

namespace Gym24_7
{
    public partial class App : Application
    {
        public App() => Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Gym24_7.Properties.Settings.Default.LicenciaSyncfusion);
    }

}
