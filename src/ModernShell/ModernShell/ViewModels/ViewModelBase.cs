using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModernShell.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T propertyField, T newPropertyValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(propertyField, newPropertyValue))
            {
                propertyField = newPropertyValue;
                OnPropertyChanged(propertyName);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}