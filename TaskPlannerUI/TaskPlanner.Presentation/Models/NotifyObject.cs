using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskPlanner.Presentation.Models
{
    public class NotifyObject : INotifyPropertyChanged
    {
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
