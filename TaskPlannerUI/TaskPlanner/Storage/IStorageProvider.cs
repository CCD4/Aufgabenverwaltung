using System.Collections.Generic;
using TaskPlanner.Domain;

namespace TaskPlanner.Storage
{
    public interface IStorageProvider
    {
        List<Task> LoadTasks();
        void SaveTasks(List<Task> tasks);
    }
}