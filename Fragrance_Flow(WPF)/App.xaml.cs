using Fragrance_flow_DL_VERSION_.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Fragrance_flow_DL_VERSION_.Domain.Entities;

namespace Fragrance_Flow_WPF_
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            
            services.AddSingleton<UserSession>();

            services.AddTransient<Loginwindow>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                var loginWindow = ServiceProvider.GetRequiredService<Loginwindow>();
                
                loginWindow.Show();
            }
            catch(InvalidOperationException ioex)
            {
                MessageBox.Show($" Error : {ioex.Message}","Error" ,MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }

}
