using System;
using System.Windows.Forms;
using TaskPlanner;
using TaskPlanner.Requests;

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

            taskPlannerMainForm.AddTaskRequested += request =>
            {
                var result = requestHandler.AddTask(request);
                if (result)
                {
                    var reply = requestHandler.LoadTasks(new RequestLoadFiltered("", true));
                    taskPlannerMainForm.ShowTasks(reply);
                }
                else
                    taskPlannerMainForm.ShowError();

            };
            
            taskPlannerMainForm.UpdateTaskRequested += request =>
            {
                var reply = requestHandler.UpdateTask(request);
                taskPlannerMainForm.ShowTasks(reply);
            };

            var initialReply = requestHandler.LoadTasks(new RequestLoadFiltered(string.Empty, false));
            taskPlannerMainForm.ShowTasks(initialReply);

            Application.Run(taskPlannerMainForm);
        }
    }
}
