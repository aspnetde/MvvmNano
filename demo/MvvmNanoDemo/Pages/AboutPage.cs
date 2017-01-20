﻿using MvvmNano.Forms;
using MvvmNanoDemo.ViewModels;
using Xamarin.Forms;

namespace MvvmNanoDemo.Pages
{
    public class AboutPage : MvvmNanoContentPage<AboutViewModel>
    {
        public AboutPage()
        {
            Title = "About this App";
            Padding = 10;

            Content = new StackLayout
            {
                Children = 
                {
                    new Label
                    {
                        Text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                    }
                }
            };
        }
    }
}

