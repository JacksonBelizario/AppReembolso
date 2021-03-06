﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using App.Models;
using System.IO;
using System.Net.Http.Headers;
using Plugin.Media.Abstractions;

namespace App.Services
{
    class SolicitacaoDataStore : IDataStore<Solicitacao>
    {
        readonly HttpClient client;
        IEnumerable<Solicitacao> items;

        public SolicitacaoDataStore()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.BackendUrl}/")
            };

            items = new List<Solicitacao>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        
        public async Task<IEnumerable<Solicitacao>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/solicitacao");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Solicitacao>>(json));
            }

            return items;
        }

        public async Task<Solicitacao> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/solicitacao/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Solicitacao>(json));
            }

            return null;
        }

        public async Task<Solicitacao> AddItemAsync(Solicitacao item)
        {
            if (item == null || !IsConnected)
                return null;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/solicitacao", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            var contents = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<Solicitacao>(contents));
            // return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Solicitacao item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/solicitacao/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            if (id == 0 && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/solicitacao/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<string> UploadFile(MediaFile media)
        {
            System.Diagnostics.Debug.WriteLine("UploadFile");
            if (media == null || !IsConnected)
                return "";

            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(media.GetStream());
            content.Add(streamContent, "\"files\"", $"\"{media.Path}\"");

            using (var response = await client.PostAsync($"api/solicitacao/upload", content))
            {
                return await response.Content.ReadAsStringAsync();
            }


        }
    }
}
