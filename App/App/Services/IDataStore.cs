using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace App.Services
{
    public interface IDataStore<T>
    {
        Task<T> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<string> UploadFile(Stream stream);
    }
}
