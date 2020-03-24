using System;
using System.ComponentModel;
using Xamarin.Forms;

using App.Models;
using static App.Web.Models.Enums;
using Xamarin.Essentials;
using Plugin.Media;
using Plugin.Media.Abstractions;
using App.ViewModels;

namespace App.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        readonly NewItemViewModel viewModel;

        public NewItemPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new NewItemViewModel();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        public async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                await viewModel.GetGeolocation();
                MessagingCenter.Send(this, "AddItem", viewModel.Item);
                await Navigation.PopModalAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Recurso não suportado no dispositivo
                await DisplayAlert("Erro: ", fnsEx.Message, "Ok");
            }
            catch (PermissionException pEx)
            {
                // Tratando erro de permissão
                await DisplayAlert("Erro: ", pEx.Message, "Ok");
            }
            catch (Exception ex)
            {
                // Não foi possivel obter a localização
                await DisplayAlert("Erro : ", ex.Message, "Ok");
            }
        }

        private async void TirarFoto(object sender, EventArgs e)
        {
            
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");
                return;
            }

            var foto = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Solicitacoes"
                });

            if (foto == null)
                return;

            viewModel.SetFile(foto);

            MinhaImagem.Source = ImageSource.FromStream(() =>
            {

                var stream = foto.GetStream();
                // foto.Dispose();
                return stream;

            });
        }
    }
}