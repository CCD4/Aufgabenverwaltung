using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskPlanner.Domain;

namespace TaskPlanner.Tests
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void ParseTask()
        {
            Task task = Parser.ParseTask("Butter #Einkauf #Home");
            Assert.AreEqual(2, task.Tags.Length);
            Assert.AreEqual("Butter #Einkauf #Home", task.Text);
            Assert.AreEqual("#Einkauf", task.Tags[0]);
            Assert.AreEqual("#Home", task.Tags[1]);
        }

        [TestMethod]
        public void ParseQuery()
        {
            string[] tags = Parser.ParseQuery("#Einkauf #Home");
            Assert.AreEqual(2, tags.Length);
            Assert.AreEqual("#Einkauf", tags[0]);
            Assert.AreEqual("#Home", tags[1]);
        }
    }
}
