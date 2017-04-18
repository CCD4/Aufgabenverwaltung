using System;
using System.Windows.Forms;
using TaskPlanner;

namespace TaskPlannerUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var taskPlannerMainForm = new TaskPlannerMainForm();
            RequestHandler requestHandler = new RequestHandler();

            taskPlannerMainForm.TaskViewRequested += request =>
            {
                var reply = requestHandler.LoadTasks(request);
                taskPlannerMainForm.ShowTasks(reply);
            };

            taskPlannerMainForm.TagsViewRequested += request =>
            {
                var reply = requestHandler.LoadTags(request);
                taskPlannerMainForm.ShowTags(reply);
            };

            var initialReply = requestHandler.LoadTasks(new RequestLoadFiltered {Filter = string.Empty});
            taskPlannerMainForm.ShowTasks(initialReply);

            Application.Run(taskPlannerMainForm);
        }
    }
}
