using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TaskPlanner.Domain;

namespace TaskPlanner.Storage
{
    public class XmlProvider : IStorageProvider
    {
        private readonly string fileName;

        public XmlProvider() : this("tasks.xml")
        {
        }

        internal XmlProvider(string fileName)
        {
            this.fileName = fileName;
        }


        public List<Task> LoadTasks()
        {
            var ser = new XmlSerializer(typeof(List<Task>));
            List<Task> tasks;

            try
            {
                using (var fileStream = File.OpenRead(fileName))
                {
                    tasks = (List<Task>) ser.Deserialize(fileStream);
                }
            }
            catch (FileNotFoundException)
            {
                tasks = new List<Task>();
            }
            return tasks;
        }

        public void SaveTasks(List<Task> tasks)
        {
            var ser = new XmlSerializer(typeof(List<Task>));
            using (var fileStream = File.Create(fileName))
            {
                ser.Serialize(fileStream, tasks);
                fileStream.Flush(true);
            }
        }
    }
}