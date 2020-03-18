using System;

using App.Models;

namespace App.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Solicitacao Item { get; set; }
        public ItemDetailViewModel(Solicitacao item = null)
        {
            Title = item?.DataDaSolicitacao.ToString();
            Item = item;
        }
    }
}
