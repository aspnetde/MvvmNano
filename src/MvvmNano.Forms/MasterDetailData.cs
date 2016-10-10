using System;

namespace MvvmNano.Forms
{
    /// <summary>
    /// Contains data for the Detail menu of the <see cref="MvvmNanoMasterDetailPage{TViewModel}"/>
    /// Feel free to derive from this method to add your own properties.
    ///  </summary>
    public class MasterDetailData 
    {
        public Type ViewModel { get; private set; }
        public string Title { get; private set; }

        public MasterDetailData(Type viewModelType,string pageTitle)
        {
            Title = pageTitle;
            ViewModel = viewModelType;
        }
    }
}