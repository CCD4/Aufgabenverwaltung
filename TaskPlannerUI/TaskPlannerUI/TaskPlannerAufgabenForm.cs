using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskPlanner;

namespace TaskPlannerUI
{
    public partial class TaskPlannerAufgabenForm : Form
    {
        public TaskPlannerAufgabenForm()
        {
            InitializeComponent();
        }

        public event EventHandler<FilterEventArgs> FilterChanged;

        public void AufgabeAnzeigen(ReplyLoadFiltered reply)
        {
            textBoxFilter.Text = reply.Filter;
            aufgabenliste.Items.Clear();
            
            var Texte = AufgabeAnzeigeErzeugen(reply.AufgabeInfos);
            aufgabenliste.Items.AddRange(Texte.ToArray());
            tabControl.SelectedTab = TabPageAufgaben;
        }

        public List<string> AufgabeAnzeigeErzeugen(AufgabeInfo[] aufgabeInfos)
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

        public event Action<RequestLoadFiltered> AufgabenViewRequested;
        public event Action<RequestLoadTags> TagsViewRequested;

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            AufgabenViewRequested(new RequestLoadFiltered {Filter = textBoxFilter.Text});
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == TabPageTags)
            {
                TagsViewRequested(new RequestLoadTags());
            }
        }
        
        public void TagsAnzeigen(ReplayLoadTags reply)
        {
            listBoxTags.Items.Clear();
            listBoxTags.Items.AddRange(reply.Tags);    
        }

    
        private void listBoxTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tag selectetdTag = (Tag)listBoxTags.SelectedItem;
            AufgabenViewRequested(new RequestLoadFiltered { Filter = selectetdTag.Text });
        }
    }
}
