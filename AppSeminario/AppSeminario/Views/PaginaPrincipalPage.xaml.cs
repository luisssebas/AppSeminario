using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSeminario.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaPrincipalPage : ContentPage
    {
        public PaginaPrincipalPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}