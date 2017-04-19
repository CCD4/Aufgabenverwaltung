using System;
using System.Linq;

namespace TaskPlanner
{
    public class RequestHandler
    {
        private readonly MemoryDBProvider dbProvider;
        private string currentFilter = "";

        public RequestHandler()
        {
            dbProvider = new MemoryDBProvider();
        }

        public ReplyLoadFiltered LoadTasks(RequestLoadFiltered request)
        {
            currentFilter = request.Filter;
            string[] tags = ParseQuery(currentFilter);
            var tasks = dbProvider.LoadTasks(tags);
            var taskInfos = Map(tasks);
            return new ReplyLoadFiltered
            {
                Filter = currentFilter,
                TaskInfos = taskInfos
            };
        }

        public ReplyLoadFiltered AddTask(RequestAddTask request)
        {
            Task newTask = ParseTask(request.TaskText);
            dbProvider.AddTask(newTask);
            return LoadTasks(new RequestLoadFiltered { Filter = currentFilter });
        }

        public Task ParseTask(string taskText)
        {
            var task = new Task();
            task.Text = taskText;
            var tagText = ExtractTags(taskText);
            task.Tags = ParseQuery(tagText);
            return task;
        }

        private string ExtractTags(string taskText)
        {
            return taskText.SkipWhile(c => c != '#').Aggregate("", (s, c) => s + c);
        }

        private TaskInfo[] Map(Task[] tasks)
        {
            return tasks.Select(task => new TaskInfo
            {
                Text = task.Text,
                Done = task.Done
            }).ToArray();
        }

        private string[] ParseQuery(string filter)
        {
            var tags = filter.Split(new[] { '#', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return tags.Select(t => '#' + t).ToArray();
        }

        public ReplayLoadTags LoadTags(RequestLoadTags request)
        {
            var tags = dbProvider.LoadTags();
            return new ReplayLoadTags { Tags = tags.ToArray() };
        }
    }

}