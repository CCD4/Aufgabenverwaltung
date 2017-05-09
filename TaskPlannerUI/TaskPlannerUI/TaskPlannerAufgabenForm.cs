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
        private TaskInfo[] taskInfos;
        private TagInfo[] tagInfos;
        private TaskInfo taskInfo;

        public TaskPlannerMainForm()
        {
            InitializeComponent();
        }

        public void ShowTasks(ReplyLoadFiltered reply)
        {
            FilterAnzeigen(reply.Filter);
            var texte = AufgabeAnzeigeErzeugen(reply.TaskInfos);
            AufgabenAnzeigen(texte);
            taskInfo = null;
            textBoxAufgabeneditor.Text = "";
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
            taskInfos = aufgabeInfos;
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
        public event Action<RequestUpdateTask> UpdateTaskRequested;
       

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            FilterTasks();
        }

        private void FilterTasks()
        {
            TaskViewRequested(new RequestLoadFiltered(textBoxFilter.Text, checkBoxIncludeDone.Checked));
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
            TaskViewRequested(new RequestLoadFiltered(selectetdTag.Tag, true));
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            OnAddTaskRequested();
        }

        private void textBoxAufgabeneditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                OnAddTaskRequested();
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                taskInfo = null;
                aufgabenliste.SelectedIndex = -1;
            }
        }

        private void OnAddTaskRequested()
        {
            if (string.IsNullOrEmpty(textBoxAufgabeneditor.Text))
                return;
            if (taskInfo == null)
            {
                AddTaskRequested(new RequestAddTask(textBoxAufgabeneditor.Text));
            }
            else
            {
                UpdateTaskRequested(new RequestUpdateTask(taskInfo.Id, taskInfo.Done, textBoxAufgabeneditor.Text));
            }
            textBoxAufgabeneditor.Clear();
        }

        private void aufgabenliste_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var requestUpdateTask = new RequestUpdateTask(taskInfos[aufgabenliste.SelectedIndex].Id, !taskInfos[aufgabenliste.SelectedIndex].Done, taskInfos[aufgabenliste.SelectedIndex].Text);
            UpdateTaskRequested(requestUpdateTask);
        }

        public void ShowError()
        {
            MessageBox.Show("Fehler");
        }

        private void aufgabenliste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(aufgabenliste.SelectedIndex == -1)
                textBoxAufgabeneditor.Clear();
            else
            {
                taskInfo = taskInfos[aufgabenliste.SelectedIndex];
                textBoxAufgabeneditor.Text = taskInfo.Text;
            }
        }

        private void checkBoxIncludeDone_CheckedChanged(object sender, EventArgs e)
        {
            FilterTasks();
        }
    }
}
