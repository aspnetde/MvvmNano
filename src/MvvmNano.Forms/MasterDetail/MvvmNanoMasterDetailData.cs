using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmNano.Forms.Annotations;

namespace MvvmNano.Forms.MasterDetail
{ 
    /// <summary>
    /// Contains all informations needed to present a detail in a master detail page.
    /// </summary>
    public class MvvmNanoMasterDetailData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// View model type associated with the presented detail page.
        /// </summary>
        public Type ViewModelType { get; set; } 

        /// <summary>
        /// Title of the detail page.
        /// </summary>
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