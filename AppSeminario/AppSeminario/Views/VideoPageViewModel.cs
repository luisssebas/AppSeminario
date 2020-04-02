using Acr.UserDialogs;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSeminario.Views
{
    [AddINotifyPropertyChangedInterfaceAttribute]
    public class VideoPageViewModel : INavigatedAware
    {
        private readonly IUserDialogs _userDialogs;

        public string Video { get; set; }

        public VideoPageViewModel(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Video"))
                Video = parameters["Video"] as string;
        }
    }
}
