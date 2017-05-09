using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskPlanner.Storage;

namespace TaskPlanner.Tests
{
    [TestClass]
    public class XmlProviderTest
    {
        [TestMethod]
        public void LoadTasks()
        {
            var sut = new XmlProvider("xmlprovider-tasks.xml");
            var tasks = sut.LoadTasks();

            Assert.AreEqual(2, tasks.Count);
            Assert.AreEqual(new Guid("4e875038-65a4-4263-bd77-c89b61eafda9"), tasks[0].Id);
            Assert.AreEqual("Butter #Einkaufen", tasks[0].Text);
            Assert.IsFalse(tasks[0].Done);
            Assert.AreEqual("#Einkaufen", tasks[0].Tags[0]);
            Assert.AreEqual(1, tasks[0].Tags.Length);

            Assert.IsTrue(tasks[1].Done);
        }
    }
}