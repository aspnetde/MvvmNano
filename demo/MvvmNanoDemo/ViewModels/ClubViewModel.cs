using MvvmNano;

namespace MvvmNanoDemo
{
    public class ClubViewModel : MvvmNanoViewModel<Club>
    {
        private string _name;
        public string Name 
        {
            get { return _name; }
            private set { _name = value; NotifyPropertyChanged(); }
        }

        private string _country;
        public string Country 
        {
            get { return _country; }
            private set { _country = value; NotifyPropertyChanged(); }
        }

        public override void Initialize(Club parameter)
        {
            Name = parameter.Name;
            Country = parameter.Country;
        }
    }
}

