using System;
using System.Windows;

namespace TaskPlanner.Presentation.Models
{
    public class MainViewModel : NotifyObject
    {
        private string taskText = "";

        public MainViewModel()
        {
            AddTaskCommand = new DelegateCommand(_ => AddTaskRequested(new RequestAddTask { TaskText = TaskText}));
        }

        public event Action<RequestAddTask> AddTaskRequested;

        public DelegateCommand AddTaskCommand { get; }

        public string TaskText
        {
            get
            {
                return taskText;
            }
            set
            {
                if(value != taskText)
                {
                    taskText = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ShowTasks(ReplyLoadFiltered reply)
        {
            
        }
    }
}
