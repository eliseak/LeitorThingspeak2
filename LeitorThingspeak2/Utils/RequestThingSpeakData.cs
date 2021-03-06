﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LeitorThingspeak2.Model;
using Newtonsoft.Json;

namespace LeitorThingspeak2.Utils
{
    //TODO: Adicionar leitura por campo (field)
    
    public class RequestThingSpeakData
    {
        private static string URL = "https://api.thingspeak.com/channels/";
        private static int maxResults = 8000; // Valor máximo suportado pelo ThingSpeak
        private string channel;
        private string field;
        private string api_key; // Chave para canais privados

        public RequestThingSpeakData(string channel, string field)
        {
            this.channel = channel;
            this.field = field;
        }
        public RequestThingSpeakData (string channel, string field, string key)
        {
            this.channel = channel;
            this.field = field;
            this.api_key = key;
        }

        /// <summary>
        /// Requisita os dados de um canal do ThingSpeak
        /// </summary>
        /// <param name="request">Objeto de requisição</param>
        /// <returns>Resposta da requisição</returns>
        private async Task<ThingSpeakResponse> SendRequest (HttpWebRequest request)
        {
            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    try { 
                    var jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    // Console.Out.WriteLine("Response: {0}", jsonDoc.ToString()); // Teste

                    return JsonConvert.DeserializeObject<ThingSpeakResponse>(jsonDoc.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[Erro] " + e);
                        return null;
                    }
                }
            }
        }

        // Retorna as 100 últimas leituras recebida no canal
        public async Task<ThingSpeakResponse> DefaultAsync()
        {
            return await CustomAsync(100);
        }
        
        // Retorna a última leitura do canal recebida no canal
        public async Task<ThingSpeakResponse> LastOneAscync()
        {
            return await CustomAsync(1);
        }

        /// <summary>
        /// Faz requisição de dados de leitura no ThingSpeak com o número de resultados informado
        /// </summary>
        /// <param name="results">Número de leituras que devem ser retornados, máx.: 8000</param>
        /// <returns>Dados das últimas [results] leituras de um canal no ThingSpeak </returns>
        public async Task<ThingSpeakResponse> CustomAsync(int results)
        {
            // Validação
            if (results < 1 || results > maxResults)
                throw new Exception("O valor deve ser entre 1 e " + maxResults.ToString());

            // String da URL
            string req_url = URL + channel + "/field/" + field + "?";

            if (!String.IsNullOrWhiteSpace(api_key)) req_url += "api_key=" + api_key + "&";

            req_url += "results=" + results.ToString();

            // Requisição HTTP GET
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(req_url));
            request.ContentType = "application/json";
            request.Method = "GET";

            return await SendRequest(request);
            
        }
        
    }
}