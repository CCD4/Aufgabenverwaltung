using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlanner.Domain;

namespace TaskPlanner.Storage
{
    public class TaskProvider
    {
        private readonly IStorageProvider storageProvider;

        public TaskProvider(IStorageProvider storageProvider)
        {
            this.storageProvider = storageProvider;
        }

        public Dictionary<string, int> LoadTags()
        {
            var tasks = storageProvider.LoadTasks();
            return TagUsageCount(tasks);
        }

        private static Dictionary<string, int> TagUsageCount(List<Task> tasks)
        {
            var allTags = tasks.SelectMany(a => a.Tags);
            var groupedTags = allTags.GroupBy(tag => tag);
            return groupedTags.ToDictionary(grp => grp.Key, grp => grp.Count());
        }

        public Task[] LoadTasks(string[] tags, bool includeDoneTasks)
        {
            IEnumerable<Task> tasks = storageProvider.LoadTasks();
            tasks = Filter(tasks, tags);
            tasks = FilterDone(tasks, includeDoneTasks);
            return tasks.ToArray();
        }

        private IEnumerable<Task> FilterDone(IEnumerable<Task> tasks, bool includeDoneTasks)
        {
            if (includeDoneTasks)
                return tasks;
            return tasks.Where(t => !t.Done);
        }

        private Task[] Filter(IEnumerable<Task> tasks, string[] tags)
        {
            if (tags.Length == 0)
                return tasks.ToArray();
            return tasks.Where(a => tags.All(t => a.Tags.Contains(t))).ToArray();
        }

        public void AddTask(Task taskToAdd)
        {
            var storage = storageProvider.LoadTasks();
            storage.Add(taskToAdd);
            storageProvider.SaveTasks(storage);
        }

        public void UpdateTask(Guid taskId, bool done, string text)
        {
            var storage = storageProvider.LoadTasks();
            var tags = Parser.ExtractTags(text);
            UpdateTask(storage, taskId, done, text, tags);
            storageProvider.SaveTasks(storage);
        }

        private static void UpdateTask(List<Task> storage, Guid taskId, bool done, string text, string[] tags)
        {
            var task = storage.First(t => t.Id == taskId);
            task.Done = done;
            task.Text = text;
            task.Tags = tags;
        }
    }
}