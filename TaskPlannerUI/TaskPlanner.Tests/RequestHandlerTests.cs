using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskPlanner.Messages;
using TaskPlanner.Replies;
using TaskPlanner.Requests;

namespace TaskPlanner.Tests
{
    [TestClass]
    public class RequestHandlerTests
    {
        [TestInitialize]
        public void InitTests()
        {
            const string tasksXml = "tasks.xml";
            if (File.Exists(tasksXml))
            {
                File.Delete(tasksXml);
            }
        }

        [TestMethod]
        public void AddTask()
        {
            var requestHandler = new RequestHandler();
            var reply = requestHandler.AddTask(new RequestAddTask ("Butter #Einkauf"));
            TaskInfo task = reply.TaskInfos.FirstOrDefault(t => t.Text == "Butter #Einkauf");
            Assert.IsNotNull(task);
            Assert.IsTrue(reply.TaskInfos.Any(t => t.Text == "Butter #Einkauf"));
        }

        [TestMethod]
        public void FilterTask()
        {
            var requestHandler = new RequestHandler();
            requestHandler.AddTask(new RequestAddTask ("Butter #Einkauf"));
            requestHandler.AddTask(new RequestAddTask ("Schlafen #Home #Wochenende"));
            requestHandler.AddTask(new RequestAddTask ("Bier #Einkauf"));
            var tasks = requestHandler.LoadTasks(new RequestLoadFiltered("#Einkauf"));

            Assert.AreEqual(2, tasks.TaskInfos.Length);
            Assert.IsTrue(tasks.TaskInfos.Any(taskInfo => taskInfo.Text == "Butter #Einkauf"));
            Assert.IsTrue(tasks.TaskInfos.Any(taskInfo => taskInfo.Text == "Bier #Einkauf"));
        }

        [TestMethod]
        public void LoadTags()
        {
            var requestHandler = new RequestHandler();
            requestHandler.AddTask(new RequestAddTask("Bier #Einkauf #Home"));
            requestHandler.AddTask(new RequestAddTask("Hausaufgaben #CCD #Spaß"));
            requestHandler.AddTask(new RequestAddTask("CleanCodeBuch #Einkauf #Schnell"));
            ReplyLoadTags replyLoadTags = requestHandler.LoadTags(new RequestLoadTags());
            Assert.AreEqual(5, replyLoadTags.TagInfos.Length);
            Dictionary<string, int> tags = replyLoadTags.TagInfos.ToDictionary(info => info.Tag, info => info.Count);
            Assert.AreEqual(2, tags["#Einkauf"]);
            Assert.AreEqual(1, tags["#Spaß"]);
        }

        [TestMethod]
        public void UpdateTask()
        {
            var requestHandler = new RequestHandler();
            requestHandler.AddTask(new RequestAddTask("Bier #Einkauf #Home"));
            requestHandler.AddTask(new RequestAddTask("Hausaufgaben #CCD #Spaß"));
            requestHandler.AddTask(new RequestAddTask("CleanCodeBuch #Einkauf #Schnell"));
            var tasks = requestHandler.LoadTasks(new RequestLoadFiltered("#Einkauf"));
            var taskInfo = tasks.TaskInfos.First();
            Assert.IsFalse(taskInfo.Done);
            requestHandler.UpdateTask(new RequestUpdateTask(taskInfo.Id,true));
            tasks = requestHandler.LoadTasks(new RequestLoadFiltered("#Einkauf"));
            var updatedtask = tasks.TaskInfos.First(info => info.Id == taskInfo.Id);
            Assert.IsTrue(updatedtask.Done);
        }
    }
}
