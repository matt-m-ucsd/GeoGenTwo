using Prism.Mvvm;

namespace GeoGenTwo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Geo Gen (5 years later ver.)";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
