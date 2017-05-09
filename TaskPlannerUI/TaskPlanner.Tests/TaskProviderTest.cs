using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TaskPlanner.Domain;
using TaskPlanner.Storage;

namespace TaskPlanner.Tests
{
    [TestClass]
    public class TaskProviderTest
    {
        [TestMethod]
        public void LoadTags()
        {
            Mock<IStorageProvider> mockProvider = new Mock<IStorageProvider>();
            List<Task> tasks = new List<Task>();
            tasks.Add(new Task { Tags = new[] { "#A", "#B" } });
            tasks.Add(new Task { Tags = new[] { "#C", "#D" } });
            tasks.Add(new Task { Tags = new[] { "#C", "#A" } });

            mockProvider.Setup(provider => provider.LoadTasks()).Returns(tasks);
            var sut = new TaskProvider(mockProvider.Object);

            var tagUsage = sut.LoadTags();

            Assert.AreEqual(4, tagUsage.Count);
            Assert.AreEqual(2, tagUsage["#A"]);
            Assert.AreEqual(2, tagUsage["#C"]);
            Assert.AreEqual(1, tagUsage["#B"]);
            Assert.AreEqual(1, tagUsage["#D"]);
        }

        [TestMethod]
        public void UpdateTask()
        {
            Mock<IStorageProvider> mockProvider = new Mock<IStorageProvider>();
            List<Task> tasks = new List<Task>();
            var id = new Guid("0B58A88D-2308-4BAB-8547-64C755FCD1B1");
            tasks.Add(new Task { Id = id, Text = "A", Tags = new[] { "#X", "#Z" } });
            tasks.Add(new Task { Text = "B", Tags = new[] { "#X" } });
            tasks.Add(new Task { Text = "C", Tags = new[] { "#Y" } });

            mockProvider.Setup(provider => provider.LoadTasks()).Returns(tasks);
            var sut = new TaskProvider(mockProvider.Object);

            var expectedText = "B2 #Y";

            List<Task> savedTasks = null;
            mockProvider.Setup(provider => provider.SaveTasks(It.IsAny<List<Task>>())).Callback(
                delegate (List<Task> t)
                {
                    savedTasks = t;
                });
            sut.UpdateTask(id, false, expectedText);

            Assert.IsNotNull(savedTasks);
            Assert.AreEqual(3, savedTasks.Count);
            Assert.AreEqual(expectedText, savedTasks[0].Text);
            Assert.AreEqual(1, savedTasks[0].Tags.Length);
            Assert.AreEqual("#Y", savedTasks[0].Tags[0]);
        }

        [TestMethod]
        public void LoadFilterDone()
        {
            Mock<IStorageProvider> mockProvider = new Mock<IStorageProvider>();
            List<Task> tasks = new List<Task>
            {
                new Task {Text="A"},
                new Task {Text="B"},
                new Task {Text="C", Done=true}
            };
            mockProvider.Setup(provider => provider.LoadTasks()).Returns(tasks);
            var sut = new TaskProvider(mockProvider.Object);

            var loadedTasks = sut.LoadTasks(new string[0], false);
            Assert.AreEqual(2, loadedTasks.Length);
            Assert.AreEqual("A", loadedTasks[0].Text);
            Assert.AreEqual("B", loadedTasks[1].Text);

            loadedTasks = sut.LoadTasks(new string[0], true);
            Assert.AreEqual(3, loadedTasks.Length);
            Assert.AreEqual("A", loadedTasks[0].Text);
            Assert.AreEqual("B", loadedTasks[1].Text);
            Assert.AreEqual("C", loadedTasks[2].Text);
        }


    }
}