using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSeminario.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class PaginaPrincipalPageViewModel : INavigatedAware
    {
        INavigationService _navigationService;

        public DelegateCommand NavegarFotoCmd { get; set; }
        public DelegateCommand NavegarWebCmd { get; set; }
        public DelegateCommand NavegarMapaCmd { get; set; }
        public DelegateCommand NavegarSqliteCmd { get; set; }

        public PaginaPrincipalPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavegarFotoCmd = new DelegateCommand(FotoEjecutar);
            NavegarWebCmd = new DelegateCommand(WebEjecutar);
            NavegarMapaCmd = new DelegateCommand(MapaEjecutar);
            NavegarSqliteCmd = new DelegateCommand(SqliteEjecutar);
        }

        private async void SqliteEjecutar()
        {
            await _navigationService.NavigateAsync("PersonaPage");
        }

        private async void FotoEjecutar()
        {
            await _navigationService.NavigateAsync("FotoPage");
        }

        private async void WebEjecutar()
        {
            await _navigationService.NavigateAsync("WebPage");
        }

        private async void MapaEjecutar()
        {
            await _navigationService.NavigateAsync("MapaPage");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
