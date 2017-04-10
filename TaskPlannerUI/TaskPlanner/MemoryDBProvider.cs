using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner
{
    public class MemoryDBProvider
    {
        private List<Aufgabe> storage;

        public MemoryDBProvider()
        {
            storage = new List<Aufgabe>
            {
                new Aufgabe
                {
                    Text = "Mlich #Einkaufen",
                    Done = false,
                    Tags = new [] {"#Einkaufen"}
                },
                new Aufgabe
                {
                    Text = "Chips #Einkaufen",
                    Done = true,
                    Tags = new [] {"#Einkaufen"}
                },
                new Aufgabe
                {
                    Text = "Friseur #Home",
                    Done = false,
                    Tags = new [] {"#Home"}
                }
            };
        }


        public string[] TagsLaden()
        {
            return storage.SelectMany(a => a.Tags).Distinct().ToArray();
        }

        public Aufgabe[] AufgabenLaden(string[] tags)
        {
            if (tags.Length == 0)
                return storage.ToArray();
            return storage.Where(a => tags.All(t => a.Tags.Contains(t))).ToArray();
        }
    }

    public class Aufgabe
    {
        public string Text { get; set; }
        public bool Done { get; set; }
        public string[] Tags { get; set; }
    }
}
