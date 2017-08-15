using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmNano;
using MvvmNano.Forms;
using Xamarin.Forms;

namespace Demo.Pages
{
    public class TabbedPage : MvvmNanoTabbedPage<MvvmNanoViewModel>
    {
        public TabbedPage()
        {
            Children.Add(new TabOnePage());
            Children.Add(new TabTwoPage());
            CurrentPage = Children[0];
            Title = "Tabbed Page";
        }
    }
}