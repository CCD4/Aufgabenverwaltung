using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            Assert.IsTrue(requestHandler.AddTask(new RequestAddTask ("Butter #Einkauf")));
            var reply = requestHandler.LoadTasks(new RequestLoadFiltered("", true));
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
            var tasks = requestHandler.LoadTasks(new RequestLoadFiltered("#Einkauf", true));

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
        public void UpdateDoneTask()
        {
            var requestHandler = new RequestHandler();
            requestHandler.AddTask(new RequestAddTask("Bier #Einkauf #Home"));
            requestHandler.AddTask(new RequestAddTask("Hausaufgaben #CCD #Spaß"));
            requestHandler.AddTask(new RequestAddTask("CleanCodeBuch #Einkauf #Schnell"));
            var tasks = requestHandler.LoadTasks(new RequestLoadFiltered("#Einkauf", true));
            var taskInfo = tasks.TaskInfos.First();
            Assert.IsFalse(taskInfo.Done);
            requestHandler.UpdateTask(new RequestUpdateTask(taskInfo.Id, true, taskInfo.Text));
            tasks = requestHandler.LoadTasks(new RequestLoadFiltered("#Einkauf", true));
            var updatedtask = tasks.TaskInfos.First(info => info.Id == taskInfo.Id);
            Assert.IsTrue(updatedtask.Done);
        }

        [TestMethod]
        public void UpdateTextTask()
        {
            var requestHandler = new RequestHandler();
            requestHandler.AddTask(new RequestAddTask("A #x"));
            requestHandler.AddTask(new RequestAddTask("B #x"));
            requestHandler.AddTask(new RequestAddTask("c #y"));

            var tasks = requestHandler.LoadTasks(new RequestLoadFiltered("", true));
            var taskBId = tasks.TaskInfos[1].Id;

            var expectedText = "B2 #y";
            requestHandler.UpdateTask(new RequestUpdateTask(taskBId, true, expectedText));

            tasks = requestHandler.LoadTasks(new RequestLoadFiltered("", true));
            Assert.AreEqual(3, tasks.TaskInfos.Length);
            var updatedtask = tasks.TaskInfos[1];
            Assert.AreEqual(expectedText, updatedtask.Text);
            var replyLoadTags = requestHandler.LoadTags(new RequestLoadTags());
            var tagInfo = replyLoadTags.TagInfos.First(tag => tag.Tag == "#y");
            Assert.AreEqual(2, tagInfo.Count);
            tagInfo = replyLoadTags.TagInfos.First(tag => tag.Tag == "#x");
            Assert.AreEqual(1, tagInfo.Count);
        }

        [TestMethod]
        public void FilterDoneTask()
        {
            var requestHandler = new RequestHandler();
            requestHandler.AddTask(new RequestAddTask("A"));
            requestHandler.AddTask(new RequestAddTask("B"));
            requestHandler.AddTask(new RequestAddTask("C"));
            var loadReply = requestHandler.LoadTasks(new RequestLoadFiltered("", true));
            Assert.AreEqual(3, loadReply.TaskInfos.Length);
            var doneTask = loadReply.TaskInfos[2];
            requestHandler.UpdateTask(new RequestUpdateTask(doneTask.Id, true, doneTask.Text));

            loadReply = requestHandler.LoadTasks(new RequestLoadFiltered("", false));
            Assert.AreEqual(2, loadReply.TaskInfos.Length);
            Assert.AreEqual("A", loadReply.TaskInfos[0].Text);
            Assert.AreEqual("B", loadReply.TaskInfos[1].Text);

            loadReply = requestHandler.LoadTasks(new RequestLoadFiltered("", true));
            Assert.AreEqual(3, loadReply.TaskInfos.Length);
            Assert.AreEqual("A", loadReply.TaskInfos[0].Text);
            Assert.AreEqual("B", loadReply.TaskInfos[1].Text);
            Assert.AreEqual("C", loadReply.TaskInfos[2].Text);
        }
    }
}
