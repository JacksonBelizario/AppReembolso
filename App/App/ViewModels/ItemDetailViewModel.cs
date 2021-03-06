﻿using System;
using System.Threading.Tasks;
using App.Models;

namespace App.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Solicitacao Item { get; set; }
        public ItemDetailViewModel(Solicitacao item = null)
        {
            Title = item?.Id + " | " + item?.Categoria.ToString();
            Item = item;
        }
    }
}
