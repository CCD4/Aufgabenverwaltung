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
            string[] tags = Parser.ParseQuery(currentFilter);
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
            Task newTask = Parser.ParseTask(request.TaskText);
            dbProvider.AddTask(newTask);
            return LoadTasks(new RequestLoadFiltered(currentFilter));
        }

        private TaskInfo[] MapTasks(Task[] tasks)
        {
            return tasks.Select(task => new TaskInfo
            {
                Text = task.Text,
                Done = task.Done
            }).ToArray();
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