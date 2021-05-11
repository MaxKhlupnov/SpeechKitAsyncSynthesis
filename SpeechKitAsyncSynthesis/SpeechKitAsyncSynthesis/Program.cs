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
                using (Stream synthesResults = await Tts())
                {
                    using (Stream streamToFile = File.Open("speech.ogg", FileMode.Create))
                    {
                        await synthesResults.CopyToAsync(streamToFile);
                    }                        
                    Console.WriteLine("Compleated");
                }
            }).GetAwaiter().GetResult();

        }

        static async Task<Stream> Tts()
        {
            const string iamToken = "t1.9euelZrKlsbIjJOako2Pxp7Gk42Siu3rnpWaksyWzMmRlpTPmcaPncvKzJDl8_cfORV7-e8fY2NU_N3z919nEnv57x9jY1T8.kOf3asSvKTYE5VR5POvar_y1jc-38pgmhMHwFBErkYvoRdMh5MoF5cdUyeu-EkLJkge7WCFVWRh1uC-uCaA9Dw"; // Укажите IAM-токен.
            const string folderId = "b1gvp43cei68d5sfhsu7"; // Укажите ID каталога.

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + iamToken);
                var values = new Dictionary<string, string>
            {
            { "text", @"Молодая рыжая собака – помесь такса с дворняжкой – очень похожая мордой на лисицу, бегала взад и вперед по тротуару и беспокойно оглядывалась по сторонам. Изредка она останавливалась и, плача, приподнимая то одну озябшую лапу, то другую, старалась дать себе отчет: как это могло случиться, что она заблудилась?

Она отлично помнила, как она провела день и как в конце концов попала на этот незнакомый тротуар.

День начался с того, что ее хозяин, столяр Лука Александрыч, надел шапку, взял под мышку какую-то деревянную штуку, завернутую в красный платок, и крикнул:

– Каштанка, пойдем!

Услыхав свое имя, помесь такса с дворняжкой вышла из-под верстака, где она спала на стружках, сладко потянулась и побежала за хозяином. Заказчики Луки Александрыча жили ужасно далеко, так что, прежде чем дойти до каждого из них, столяр должен был по нескольку раз заходить в трактир и подкрепляться.Каштанка помнила, что по дороге она вела себя крайне неприлично.От радости, что ее взяли гулять, она прыгала, бросалась с лаем на вагоны конножелезки, забегала во дворы и гонялась за собаками.Столяр то и дело терял ее из виду, останавливался и сердито кричал на нее.Раз даже он с выражением алчности на лице забрал в кулак ее лисье ухо, потрепал и проговорил с расстановкой:

– Чтоб… ты… из… дох… ла, холера!

Побывав у заказчиков, Лука Александрыч зашел на минутку к сестре, у которой пил и закусывал; от сестры пошел он к знакомому переплетчику, от переплетчика в трактир, из трактира к куму и т.д.Одним " },
            { "lang", "ru-ru" },
            { "folderId", folderId },
          //  { "format", "lpcm" },
          //  { "sampleRateHertz", "48000" }
            };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://tts.api.cloud.yandex.net/speech/v1/tts:synthesize", content);
                return await response.Content.ReadAsStreamAsync();
                
         }
    }
}