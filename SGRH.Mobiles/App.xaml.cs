using static System.Net.Mime.MediaTypeNames;

namespace SGRH.Mobiles
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
