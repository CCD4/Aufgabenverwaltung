using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var taskPlannerAufgabenForm = new TaskPlannerAufgabenForm();
            RequestHandler requestHandler = new RequestHandler();

            taskPlannerAufgabenForm.AufgabenViewRequested += request =>
            {
                var reply = requestHandler.AufgabeLaden(request);
                taskPlannerAufgabenForm.AufgabeAnzeigen(reply);

            };

            taskPlannerAufgabenForm.TagsViewRequested += request =>
            {
                var reply = requestHandler.TagsLaden(request);
                taskPlannerAufgabenForm.TagsAnzeigen(reply);
            };

            var initialReply = requestHandler.AufgabeLaden(new RequestLoadFiltered() {Filter = String.Empty});
            taskPlannerAufgabenForm.AufgabeAnzeigen(initialReply);

            Application.Run(taskPlannerAufgabenForm);
        }
    }
}
