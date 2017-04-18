using System.Collections.Generic;
using System.Linq;

namespace TaskPlanner
{
    public class MemoryDBProvider
    {
        private List<Task> storage;

        public MemoryDBProvider()
        {
            storage = new List<Task>
            {
                new Task
                {
                    Text = "Mlich #Einkaufen",
                    Done = false,
                    Tags = new [] {"#Einkaufen"}
                },
                new Task
                {
                    Text = "Chips #Einkaufen",
                    Done = true,
                    Tags = new [] {"#Einkaufen"}
                },
                new Task
                {
                    Text = "Friseur #Home",
                    Done = false,
                    Tags = new [] {"#Home"}
                }
            };
        }


        public string[] LoadTags()
        {
            return storage.SelectMany(a => a.Tags).Distinct().ToArray();
        }

        public Task[] LoadTasks(string[] tags)
        {
            if (tags.Length == 0)
                return storage.ToArray();
            return storage.Where(a => tags.All(t => a.Tags.Contains(t))).ToArray();
        }
    }

    public class Task
    {
        public string Text { get; set; }
        public bool Done { get; set; }
        public string[] Tags { get; set; }
    }
}
