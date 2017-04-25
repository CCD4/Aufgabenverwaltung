using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskPlanner
{
    public class RequestHandler
    {
        private readonly XMLProvider dbProvider;
        private string currentFilter = "";

        public RequestHandler()
        {
            dbProvider = new XMLProvider();
        }

        public ReplyLoadFiltered LoadTasks(RequestLoadFiltered request)
        {
            currentFilter = request.Filter;
            string[] tags = ParseQuery(currentFilter);
            var tasks = dbProvider.LoadTasks(tags);
            var taskInfos = MapTasks(tasks);
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
            return LoadTasks(new RequestLoadFiltered(currentFilter));
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

        private TaskInfo[] MapTasks(Task[] tasks)
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

        public ReplyLoadTags LoadTags(RequestLoadTags request)
        {
            var tags = dbProvider.LoadTags();
            var tagInfos = MapTagCounts(tags);
            return new ReplyLoadTags { TagInfos = tagInfos.ToArray() };
        }

        private IEnumerable<TagInfo> MapTagCounts(Dictionary<string, int> tagCounts)
        {
            return tagCounts.Select(tc => new TagInfo {Tag = tc.Key, Count = tc.Value});
        }
    }

}