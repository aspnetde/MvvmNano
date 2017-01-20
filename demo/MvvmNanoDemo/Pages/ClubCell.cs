using MvvmNanoDemo.Model;
using Xamarin.Forms;

namespace MvvmNanoDemo.Pages
{
    public class ClubCell : TextCell
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var club = BindingContext as Club;
            if (club == null)
                return;

            Text = club.Name;
            Detail = club.Country;
        }
    }
}

