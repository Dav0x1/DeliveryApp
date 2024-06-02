
using DeliveryApp.Models;
using DeliveryApp.Services;
using DeliveryApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DeliveryApp
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext,services) =>{
                    services.AddDbContext<AppDbContext>(ServiceLifetime.Singleton);

                    services.AddSingleton<DataService>();

                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainWindowViewModel>()
                    });
                }
            ).Build();
        }
         
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            var _dbContext = _host.Services.GetRequiredService<AppDbContext>();
            _dbContext.Database.Migrate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }

}
