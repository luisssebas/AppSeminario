using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppSeminario.Views;
using Prism.Unity;
using Prism.Ioc;
using Prism;
using Acr.UserDialogs;

namespace AppSeminario
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/PaginaPrincipalPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            
            containerRegistry.RegisterForNavigation<PaginaPrincipalPage, PaginaPrincipalPageViewModel>();
            containerRegistry.RegisterForNavigation<FotoPage, FotoPageViewModel>();
            containerRegistry.RegisterForNavigation<VideoPage, VideoPageViewModel>();
        }
    }
}
