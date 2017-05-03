using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TaskPlanner.Domain;

namespace TaskPlanner.Storage
{
    public class XmlProvider
    {
        private const string FileName = "tasks.xml";

        public Dictionary<string, int> LoadTags()
        {
            var tasks = LoadFromFile();
            return CountTags(tasks);
        }

        private static Dictionary<string, int> CountTags(List<Task> tasks)
        {
            var allTags = tasks.SelectMany(a => a.Tags);
            var groupedTags = allTags.GroupBy(tag => tag);
            return groupedTags.ToDictionary(grp => grp.Key, grp => grp.Count());
        }

        public Task[] LoadTasks(string[] tags)
        {
            IEnumerable<Task> tasks = LoadFromFile();
            tasks = Filter(tasks, tags);
            return tasks.ToArray();
        }

        private List<Task> LoadFromFile()
        {
            var ser = new XmlSerializer(typeof(List<Task>));
            List<Task> tasks;

            try
            {
                using (var fileStream = File.OpenRead(FileName))
                {
                    tasks = (List<Task>)ser.Deserialize(fileStream);
                }
            }
            catch (FileNotFoundException)
            {
                tasks = new List<Task>();
            }
            return tasks;
        }

        private Task[] Filter(IEnumerable<Task> tasks, string[] tags)
        {
            if (tags.Length == 0)
                return tasks.ToArray();
            return tasks.Where(a => tags.All(t => a.Tags.Contains(t))).ToArray();
        }

        public void AddTask(Task taskToAdd)
        {
            var storage = LoadFromFile();
            storage.Add(taskToAdd);
            SaveTasks(storage);
        }

        private void SaveTasks(List<Task> tasks)
        {
            var ser = new XmlSerializer(typeof(List<Task>));
            using (var fileStream = File.Create(FileName))
            {
                ser.Serialize(fileStream, tasks);
                fileStream.Flush(true);
            }
        }

        public void UpdateTask(Guid taskId, bool done)
        {
            var storage = LoadFromFile();
            UpdateTask(storage, taskId, done);
            SaveTasks(storage);
        }

        private static void UpdateTask(List<Task> storage, Guid taskId, bool done)
        {
            var task = storage.First(t => t.Id == taskId);
            task.Done = done;
        }
    }
}
