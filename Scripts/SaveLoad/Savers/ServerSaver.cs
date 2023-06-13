using Assets.SimpleWallet.Scripts.SaveLoad.Serializers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.SimpleWallet.Scripts.SaveLoad.Savers
{
    public class ServerSaver : ISaveLoader
    {
        private readonly string _url;
        private readonly ISerializer<string> _serializer;

        public ServerSaver(string url, ISerializer<string> serializer)
        {
            _url = url;
            _serializer = serializer;
        }

        public TType Load<TType>() =>
            ServerLoad<TType>().GetAwaiter().GetResult();

        public void Save<TType>(TType data) =>
            ServerSave(data).GetAwaiter().GetResult();

        private async Task<TType> ServerLoad<TType>()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(_url);

                    if (response.IsSuccessStatusCode)
                    {
                        string dataString = await response.Content.ReadAsStringAsync();
                        return _serializer.Deserialize<TType>(dataString);
                    }
                    else
                    {
                        Debug.LogWarning($"Ошибка при загрузке данных: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Ошибка при загрузке данных: {ex.Message}");
                }
            }

            return default;
        }

        private async Task ServerSave<TType>(TType data)
        {
            string serializedData = _serializer.Serialize(data);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync(_url, new StringContent(serializedData));

                    if (!response.IsSuccessStatusCode)
                    {
                        Debug.LogWarning($"Ошибка при сохранении данных: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Ошибка при сохранении данных: {ex.Message}");
                }
            }
        }
    }
}