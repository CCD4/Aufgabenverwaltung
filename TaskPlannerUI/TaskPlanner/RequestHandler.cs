using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlanner.Domain;
using TaskPlanner.Messages;
using TaskPlanner.Replies;
using TaskPlanner.Requests;
using TaskPlanner.Storage;

namespace TaskPlanner
{
    public class RequestHandler
    {
        private readonly TaskProvider taskProvider;
        private string currentFilter = "";
        private bool currentDoneFilter;

        public RequestHandler()
        {
            taskProvider = new TaskProvider(new XmlProvider());
        }

        public ReplyLoadFiltered LoadTasks(RequestLoadFiltered request)
        {
            currentFilter = request.Filter;
            currentDoneFilter = request.IncludeDoneTasks;
            string[] tags = Parser.ExtractTags(currentFilter);
            var tasks = taskProvider.LoadTasks(tags, request.IncludeDoneTasks);
            var taskInfos = MapTasks(tasks);
            return new ReplyLoadFiltered
            {
                Filter = currentFilter,
                TaskInfos = taskInfos
            };
        }

        public bool AddTask(RequestAddTask request)
        {
            try
            {
                Task newTask = Parser.ParseTask(request.TaskText);
                taskProvider.AddTask(newTask);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public ReplyLoadFiltered UpdateTask(RequestUpdateTask request)
        {
            taskProvider.UpdateTask(request.Id, request.Done, request.TaskText);
            return LoadTasks(new RequestLoadFiltered(currentFilter, currentDoneFilter));
        }

        private TaskInfo[] MapTasks(Task[] tasks)
        {
            return tasks.Select(task => new TaskInfo
            {
                Id = task.Id,
                Text = task.Text,
                Done = task.Done
            }).ToArray();
        }

        public ReplyLoadTags LoadTags(RequestLoadTags request)
        {
            var tags = taskProvider.LoadTags();
            var tagInfos = MapTagCounts(tags);
            return new ReplyLoadTags { TagInfos = tagInfos.ToArray() };
        }

        private IEnumerable<TagInfo> MapTagCounts(Dictionary<string, int> tagCounts)
        {
            return tagCounts.Select(tc => new TagInfo {Tag = tc.Key, Count = tc.Value});
        }
    }

}