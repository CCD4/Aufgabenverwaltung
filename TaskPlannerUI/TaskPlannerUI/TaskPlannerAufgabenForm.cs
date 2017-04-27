using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TaskPlanner;
using TaskPlanner.Messages;
using TaskPlanner.Replies;
using TaskPlanner.Requests;

namespace TaskPlannerUI
{
    public partial class TaskPlannerMainForm : Form
    {
        private TagInfo[] tagInfos;

        public TaskPlannerMainForm()
        {
            InitializeComponent();
        }

        public void ShowTasks(ReplyLoadFiltered reply)
        {
            FilterAnzeigen(reply.Filter);
            var texte = AufgabeAnzeigeErzeugen(reply.TaskInfos);
            AufgabenAnzeigen(texte);
        }

        private void FilterAnzeigen(string filter)
        {
            textBoxFilter.Text = filter;
        }

        private void AufgabenAnzeigen(List<string> texte)
        {
            aufgabenliste.Items.Clear();

            aufgabenliste.Items.AddRange(texte.ToArray());
            tabControl.SelectedTab = TabPageAufgaben;
        }

        public List<string> AufgabeAnzeigeErzeugen(TaskInfo[] aufgabeInfos)
        {
            var texte = new List<string>();
            foreach (var aufgabeInfo in aufgabeInfos)
            {
                string text;
                if (aufgabeInfo.Done)
                {
                    text = "[x] " + aufgabeInfo.Text;
                }
                else
                {
                    text = "[ ] " + aufgabeInfo.Text;
                }
                texte.Add(text);
            }
            return texte;

        }

        public event Action<RequestLoadFiltered> TaskViewRequested;
        public event Action<RequestLoadTags> TagsViewRequested;
        public event Action<RequestAddTask> AddTaskRequested;

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            TaskViewRequested(new RequestLoadFiltered(textBoxFilter.Text));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == TabPageTags)
            {
                TagsViewRequested(new RequestLoadTags());
            }
        }

        public void ShowTags(ReplyLoadTags reply)
        {
            listBoxTags.Items.Clear();
            tagInfos = reply.TagInfos;
            var tagInfoStrings = reply.TagInfos.Select(ti => $"{ti.Tag} ({ti.Count})").ToArray();
            listBoxTags.Items.AddRange(tagInfoStrings);
        }

        private void listBoxTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectetdTag = tagInfos[listBoxTags.SelectedIndex];
            TaskViewRequested(new RequestLoadFiltered(selectetdTag.Tag));
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            AddTaskRequested(new RequestAddTask(textBoxAufgabeneditor.Text));
        }
    }
}
