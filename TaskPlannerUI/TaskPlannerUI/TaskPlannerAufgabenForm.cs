using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TaskPlanner;

namespace TaskPlannerUI
{
    public partial class TaskPlannerMainForm : Form
    {
        public TaskPlannerMainForm()
        {
            InitializeComponent();
        }

        public void ShowTasks(ReplyLoadFiltered reply)
        {
            textBoxFilter.Text = reply.Filter;
            aufgabenliste.Items.Clear();
            
            var Texte = AufgabeAnzeigeErzeugen(reply.TaskInfos);
            aufgabenliste.Items.AddRange(Texte.ToArray());
            tabControl.SelectedTab = TabPageAufgaben;
        }

        public List<string> AufgabeAnzeigeErzeugen(TaskInfo[] aufgabeInfos)
        {
            List<string> Texte = new List<string>();
            foreach (var aufgabeInfo in aufgabeInfos)
            {
                string text;
                if(aufgabeInfo.Done)
                {
                    text = "[x] " + aufgabeInfo.Text;
                }
                else
                {
                    text = "[ ] " + aufgabeInfo.Text;
                }
                Texte.Add(text);
            }
            return Texte;

        }

        public event Action<RequestLoadFiltered> TaskViewRequested;
        public event Action<RequestLoadTags> TagsViewRequested;

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            TaskViewRequested(new RequestLoadFiltered {Filter = textBoxFilter.Text});
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == TabPageTags)
            {
                TagsViewRequested(new RequestLoadTags());
            }
        }
        
        public void ShowTags(ReplayLoadTags reply)
        {
            listBoxTags.Items.Clear();
            listBoxTags.Items.AddRange(reply.Tags);    
        }
   
        private void listBoxTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectetdTag = (string)listBoxTags.SelectedItem;
            TaskViewRequested(new RequestLoadFiltered { Filter = selectetdTag });
        }
    }
}
