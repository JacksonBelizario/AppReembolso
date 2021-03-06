﻿using System;
using System.ComponentModel;
using Xamarin.Forms;
using App.Models;
using App.ViewModels;

namespace App.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        readonly ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Solicitacao();

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        async void DelItem_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Remover?", "Deseja excluir a solicitação?", "Sim", "Não");
            if (answer)
            {
                MessagingCenter.Send(this, "DelItem", viewModel.Item);
                await Navigation.PopAsync();
            }
        }
    }
}