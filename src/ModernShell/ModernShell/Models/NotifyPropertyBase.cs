using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModernShell.Models
{
    public abstract class NotifyPropertyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            var result = false;

            if (field == null || !field.Equals(value))
            {
                field = value;
                result = true;
                OnPropertyChanged(propertyName);
            }

            return result;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}