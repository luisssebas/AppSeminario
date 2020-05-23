using AppSeminario.Models;
using AppSeminario.Models.Sqlite;
using AppSeminario.Servicio;
using Prism.Commands;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppSeminario.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class PersonaPageViewModel : INavigatedAware
    {
        INavigationService _navigationService;
        private ServicioSqlite _servicio;

        public DelegateCommand AgregarPersonaCmd { get; set; }

        public ObservableCollection<PersonaMS> Personas { get; set; }

        public PersonaMS Persona { get; set; }


        public PersonaPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _servicio = new ServicioSqlite();

            Persona = new PersonaMS();
            Personas = new ObservableCollection<PersonaMS>();

            AgregarPersonaCmd = new DelegateCommand(AgregarPersonaEjecutar);
        }

        private async void AgregarPersonaEjecutar()
        {
            await _navigationService.NavigateAsync("PersonaDataPage");
        }

        private async void ObtenerDatos()
        {
            var mensaje = await _servicio.ObtenerPersonas();
            Personas.Clear();

            foreach (var item in mensaje)
            {
                PersonaMS dato = new PersonaMS()
                {
                    Carrera = item.Carrera,
                    Ciudad = item.Ciudad,
                    Identificacion = item.Identificacion,
                    Nombre = item.Nombre,
                    PersonaId = item.PersonaId,
                    Telefono = item.Telefono
                };

                Personas.Add(dato);
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ObtenerDatos();
        }

        
    }
}
