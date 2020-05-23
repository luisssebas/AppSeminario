using AppSeminario.Models;
using AppSeminario.Models.Sqlite;
using AppSeminario.Servicio;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSeminario.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class PersonaDataPageViewModel : INavigatedAware
    {
        INavigationService _navigationService;
        private ServicioSqlite _servicio;

        public DelegateCommand AgregarPersonaCmd { get; set; }

        public PersonaMS Persona { get; set; }

        public PersonaDataPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _servicio = new ServicioSqlite();

            Persona = new PersonaMS();

            AgregarPersonaCmd = new DelegateCommand(AgregarPersonaEjecutar);
        }

        private async void AgregarPersonaEjecutar()
        {
            var mensaje = new Persona()
            {
                Carrera = Persona.Carrera,
                Ciudad = Persona.Ciudad,
                Identificacion = Persona.Identificacion,
                Nombre = Persona.Nombre,
                PersonaId = Persona.PersonaId,
                Telefono = Persona.Telefono
            };

            await _servicio.AgregarPersona(mensaje);

            await _navigationService.NavigateAsync("PersonaPage");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
