using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using App.Models;
using App.Views;

namespace App.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Solicitacao> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Solicitações";
            Items = new ObservableCollection<Solicitacao>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Solicitacao>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Solicitacao;
                var itemDb = await DataStore.AddItemAsync(newItem);
                Items.Add(itemDb);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Solicitacao>(this, "DelItem", async (obj, item) =>
            {
                var oldItem = item as Solicitacao;
                Items.Remove(oldItem);
                await DataStore.DeleteItemAsync(oldItem.Id);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}