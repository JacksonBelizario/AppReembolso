using App.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static App.Web.Models.Enums;
using System.Linq;

namespace App.ViewModels
{
    class NewItemViewModel : BaseViewModel
    {
        public Solicitacao Item { get; set; }
        public List<string> ListaCategorias => Enum.GetNames(typeof(Categorias)).ToList();
        public NewItemViewModel()
        {

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
        }
        public async void SetFile(MediaFile foto)
        {

            var url = await DataStore.UploadFile(foto);
            System.Diagnostics.Debug.WriteLine(url);
            Item.Anexo = url;
        }

        public async Task GetGeolocation()
        {
            var localizacao = await Geolocation.GetLastKnownLocationAsync();
            if (localizacao != null)
            {
                Item.Latitude = localizacao.Latitude;
                Item.Longitude = localizacao.Longitude;
            }
        }
    }
}
