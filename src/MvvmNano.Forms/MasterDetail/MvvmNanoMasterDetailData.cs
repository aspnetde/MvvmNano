using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmNano.Forms.Annotations;

namespace MvvmNano.Forms.MasterDetail
{ 
    public class MvvmNanoMasterDetailData : INotifyPropertyChanged
    {
        public Type ViewModelType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; set; } 

        public MvvmNanoMasterDetailData(string title)
        {
            Title = title;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}