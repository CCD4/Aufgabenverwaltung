using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPlanner
{
    public static class Parser
    {
        public static  Task ParseTask(string taskText)
        {
            var task = new Task();
            task.Text = taskText;
            var tagText = ExtractTags(taskText);
            task.Tags = ParseQuery(tagText);
            return task;
        }

        private static string ExtractTags(string taskText)
        {
            return taskText.SkipWhile(c => c != '#').Aggregate("", (s, c) => s + c);
        }

        public static string[] ParseQuery(string filter)
        {
            var tags = filter.Split(new[] { '#', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return tags.Select(t => '#' + t).ToArray();
        }
    }
}
