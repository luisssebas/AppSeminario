using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppSeminario.Views;
using Prism.Unity;
using Prism.Ioc;
using Prism;
using Acr.UserDialogs;
using AppSeminario.Interface;
using AppSeminario.Models.Sqlite;

namespace AppSeminario
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        protected override async void OnInitialized()
        {
            InitializeComponent();
            CreateDB();

            await NavigationService.NavigateAsync("NavigationPage/PaginaPrincipalPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);

            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<PaginaPrincipalPage, PaginaPrincipalPageViewModel>();
            containerRegistry.RegisterForNavigation<FotoPage, FotoPageViewModel>();
            containerRegistry.RegisterForNavigation<VideoPage, VideoPageViewModel>();
            containerRegistry.RegisterForNavigation<WebPage, WebPageViewModel>();
            containerRegistry.RegisterForNavigation<PersonaPage, PersonaPageViewModel>();
            containerRegistry.RegisterForNavigation<PersonaDataPage, PersonaDataPageViewModel>();
            containerRegistry.RegisterForNavigation<MapaPage, MapaPageViewModel>();
        }
        private void CreateDB()
        {
            var db = DependencyService.Get<ISQLite>().GetConnection();
            db.CreateTable<Persona>();
        }
    }
}
