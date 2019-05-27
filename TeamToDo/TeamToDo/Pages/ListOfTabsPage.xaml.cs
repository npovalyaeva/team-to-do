using System;
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
            ToolbarItems.Add(new ToolbarItem("Logout", null, OnBackPressed));
        }

        private async void OnBackPressed()
        {
            await Navigation.PopToRootAsync(true);
        }
    }
}