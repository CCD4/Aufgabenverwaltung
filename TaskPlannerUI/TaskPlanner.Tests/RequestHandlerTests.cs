using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskPlanner.Tests
{
    [TestClass]
    public class RequestHandlerTests
    {
        [TestMethod]
        public void AddTask()
        {
            var requestHandler = new RequestHandler();
            var reply = requestHandler.AddTask(new RequestAddTask {TaskText = "Butter #Einkauf"});
            TaskInfo task = reply.TaskInfos.FirstOrDefault(t => t.Text == "Butter #Einkauf");
            Assert.IsNotNull(task);
            Assert.IsTrue(reply.TaskInfos.Any(t => t.Text == "Butter #Einkauf"));
        }

        [TestMethod]
        public void ParseTask()
        {
            var requestHandler = new RequestHandler();
            Task task = requestHandler.ParseTask("Butter #Einkauf #Home");
            Assert.AreEqual(2, task.Tags.Length);
            Assert.AreEqual("Butter #Einkauf #Home", task.Text);
            Assert.AreEqual("#Einkauf", task.Tags[0]);        
            Assert.AreEqual("#Home", task.Tags[1]);        
        }
    }
}
