using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Quan_ly_project.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;

        // Thuộc tính giúp theo dõi trạng thái "bận"
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        // Sự kiện thông báo thay đổi thuộc tính
        public event PropertyChangedEventHandler PropertyChanged;

        // Phương thức giúp thay đổi giá trị của thuộc tính và thông báo thay đổi cho UI
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // Phương thức để thông báo sự thay đổi của thuộc tính
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}