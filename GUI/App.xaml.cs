using BLL.Interfaces;
using BLL.Services;
using BLL.Utilities.Validators;
using DAL.Database;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            //set startup window chỗ này thay cho StartUpUrl
            var window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .Build();

            //services.AddDbContext<TodoListAppDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));  

            // Đăng ký DbContext
            services.AddDbContext<TodoListAppDbContext>();

            // Đăng ký repository
            services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
            services.AddScoped<ISubTaskRepository, SubTaskRepository>();
            services.AddScoped<ILabelRepository, LabelRepository>();
            services.AddScoped<IDailyTaskRepository, DailyTaskRepository>();

            // Đăng ký services
            services.AddScoped<ITodoTaskService, TodoTaskService>();
            services.AddScoped<ISubTaskService, SubTaskService>();
            services.AddScoped<ILabelService, LabelService>();
            services.AddScoped<IDailyTaskService, DailyTaskService>();

            // Đăng ký Window
            services.AddTransient<MainWindow>();
            services.AddTransient<SubtaskWindow>();
            services.AddTransient<DailyTaskDetailWindow>();

            // Đăng ký validator
            services.AddScoped<ValidateTodoTask>();
            services.AddScoped<ValidateSubTask>();
            services.AddScoped<ValidateLabel>();
            services.AddScoped<ValidateTask>();


        }
    }

}
