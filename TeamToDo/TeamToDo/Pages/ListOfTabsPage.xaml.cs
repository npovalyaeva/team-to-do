﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamToDo.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfTabsPage : TabbedPage
    {
        public ListOfTabsPage()
        {
            InitializeComponent();
        }

        private async void Logout_Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopToRootAsync(true);
        }
    }
}