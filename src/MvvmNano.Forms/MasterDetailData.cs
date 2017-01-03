using System;

namespace MvvmNano.Forms
{
    /// <summary>
    /// Contains data for the Detail menu of the <see cref="MvvmNanoMasterDetailPage{TViewModel}"/>
    /// Feel free to derive from this class to add your own properties.
    ///  </summary>
    public class MasterDetailData 
    {
        public Type ViewModelType { get; set; }
        public string Title { get; set; }

        public MasterDetailData(string title)
        {
            Title = title;
        }
    }
}
