using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

using App.Models;
using static App.Web.Models.Enums;
using Xamarin.Essentials;

namespace App.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Solicitacao Item { get; set; }
        public List<string> ListaCategorias => Enum.GetNames(typeof(Categorias)).ToList();

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Solicitacao
            {
                DataDaSolicitacao = DateTime.UtcNow,
                DataDaCompra = DateTime.UtcNow,
                Categoria = (Categorias)1,
                Descricao = "",
                Anexo = "",
                Valor = 0,
                Status = (Status)0,
            };

            BindingContext = this;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        public async void Save_Clicked(object sender, EventArgs e)
        {
            try
            {
                var localizacao = await Geolocation.GetLastKnownLocationAsync();
                if (localizacao != null)
                {
                    Item.Latitude = localizacao.Latitude;
                    Item.Longitude = localizacao.Longitude;
                }
                MessagingCenter.Send(this, "AddItem", Item);
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
    }
}