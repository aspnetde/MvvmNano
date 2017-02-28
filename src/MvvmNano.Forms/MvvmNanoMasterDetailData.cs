using System;

namespace MvvmNano.Forms
{
    /// <summary>
    /// Contains data for the Detail menu of the <see cref="MvvmNanoMasterDetailPage{TViewModel}"/>
    /// Feel free to derive from this class to add your own properties.
    ///  </summary>
    public class MvvmNanoMasterDetailData 
    {
        public Type ViewModelType { get; set; }
        public string Title { get; set; }

        public MvvmNanoMasterDetailData()
        {

        }

        public MvvmNanoMasterDetailData(string title)
        {
            Title = title;
        } 
    }
}
