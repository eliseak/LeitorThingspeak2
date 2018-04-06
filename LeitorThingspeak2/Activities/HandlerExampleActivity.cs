using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LeitorThingspeak2.Utils;

namespace LeitorThingspeak2.Activities
{
    [Activity(Label = "HandlerExampleActivity")]
    public class HandlerExampleActivity : Activity
    {
        private TextView txt_titHandler;
        private TextView txt_readValue;
        private TextView txt_readTime;
        private TextView txt_descr;
        private int time = 5000;

        private Handler handler; // Objeto que chama o método para atualizar os dados exibidos

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.HandlerExample);

            txt_titHandler = FindViewById<TextView>(Resource.Id.txt_titHandler);
            txt_readValue = FindViewById<TextView>(Resource.Id.txt_readValue);
            txt_readTime = FindViewById<TextView>(Resource.Id.txt_readTime);
            txt_descr = FindViewById<TextView>(Resource.Id.txt_descr);

            txt_descr.Text = "Dados atualizados automaticamente a cada " + time + " ms";
            
            handler = new Handler();
            handler.PostDelayed(RequestValueAsync, time);
        }

        protected override void OnPause()
        {
            base.OnPause();

            // Encerra handler
            try
            {
                handler.RemoveCallbacks(RequestValueAsync);
            }
            catch (Exception e)
            {
                Toast.MakeText(this, e.Message, ToastLength.Short).Show();
            }
        }
        
        // Requisita os dados do ThingSpeak e exibe na tela
        private async void RequestValueAsync()
        {
            Toast.MakeText(this ,"!", ToastLength.Short).Show();

            // Requisição dos dados
            var channel = Resources.GetString(Resource.String.channel);
            var field = Resources.GetString(Resource.String.field);
            var results = Convert.ToInt32(Resources.GetString(Resource.String.num_results));

            var response = await new RequestThingSpeakData(channel, field).LastOneAscync();

            // Exibição dos dados
            if (response != null)
            {
                txt_titHandler.Text = "Canal " + channel + " : Campo " + field;

                var text = response.Feeds.FirstOrDefault().GetValueFromField(field).ToString();
                if (field == "1") text += "°C";
                else if (field == "2") text += "°F";

                txt_readValue.Text = text;
                txt_readTime.Text = "Atualizado em: " + DateTime.Now;
            }

            // Ação será repetida novamente em 5s (tempo de "time")
            handler.PostDelayed(RequestValueAsync, time);

        }
        
    }
}