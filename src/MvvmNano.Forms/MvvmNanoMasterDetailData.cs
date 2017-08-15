using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmNano.Forms.Annotations;

namespace MvvmNano.Forms
{
    public class MvvmNanoMasterDetailData<TViewModel> : MvvmNanoMasterDetailData
        where TViewModel : MvvmNanoViewModel
    {
        public MvvmNanoMasterDetailData(string title) : base(title)
        {
            ViewModelType = typeof(TViewModel);
        }
    }

    public abstract class MvvmNanoMasterDetailData : INotifyPropertyChanged
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