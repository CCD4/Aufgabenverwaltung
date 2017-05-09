using System;
using System.Linq;

namespace TaskPlanner.Domain
{
    public static class Parser
    {
        public static Task ParseTask(string taskText)
        {
            var task = new Task();
            task.Text = taskText;
            task.Tags = ExtractTags(taskText);
            return task;
        }

        public static string[] ExtractTags(string taskText)
        {
            var tags = taskText.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return tags.Where(t => t.StartsWith("#")).ToArray();
        }
    }
}