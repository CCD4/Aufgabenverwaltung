using System.Windows;
using TaskPlanner.Presentation.Models;

namespace TaskPlanner.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var window = new MainWindow();
            var requestHandler = new RequestHandler();
            var model = new MainViewModel();
            model.AddTaskRequested += request =>
            {
                var reply = requestHandler.AddTask(request);
                model.ShowTasks(reply);
            };
            window.DataContext = model;
            window.Show();
        }
    }
}
