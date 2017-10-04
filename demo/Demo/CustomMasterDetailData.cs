using MvvmNano.Forms.MasterDetail;
using Xamarin.Forms;

namespace Demo
{
    public class CustomMasterDetailData : MvvmNanoMasterDetailData
    {
        public Color TextColor { get; set; }

        public CustomMasterDetailData(string title, Color textColor) : base(title)
        {
            TextColor = textColor;
        }
    }
}