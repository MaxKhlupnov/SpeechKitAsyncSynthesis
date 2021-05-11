using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace TTS
{
    class Program
    {
        static void Main()
        {
            Task.Run(async () =>
            {
                byte [] synthesResults = await Tts();
                await File.WriteAllBytesAsync("speech.raw", synthesResults);                
            });            
        }

        static async Task<byte[]> Tts()
        {
            const string iamToken = "CggaATEVAgA..."; // Укажите IAM-токен.
            const string folderId = "b1gvmob95yysaplct532"; // Укажите ID каталога.

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + iamToken);
                var values = new Dictionary<string, string>
            {
            { "text", "Hello World" },
            { "lang", "en-US" },
            { "folderId", folderId },
            { "format", "lpcm" },
            { "sampleRateHertz", "48000" }
            };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://tts.api.cloud.yandex.net/speech/v1/tts:synthesize", content);
                return await response.Content.ReadAsByteArrayAsync();
                
         }
    }
}