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

        private Handler handler;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.HandlerExample);

            txt_titHandler = FindViewById<TextView>(Resource.Id.txt_titHandler);
            txt_readValue = FindViewById<TextView>(Resource.Id.txt_readValue);
            txt_readTime = FindViewById<TextView>(Resource.Id.txt_readTime);

            handler = new Handler();
            handler.PostDelayed(RequestValueAsync, 5000);
        }

        protected override void OnPause()
        {
            base.OnPause();
            handler.RemoveCallbacks(RequestValueAsync);
        }


        // TODO: Mover métodos abaixo

        private async void RequestValueAsync()
        {
            Toast.MakeText(this ,"!", ToastLength.Short).Show();

            var channel = Resources.GetString(Resource.String.channel);
            var field = Resources.GetString(Resource.String.field);
            var results = Convert.ToInt32(Resources.GetString(Resource.String.num_results));

            var response = await new RequestThingSpeakData(channel, field).LastOneAscync();

            if (response != null)
            {
                txt_titHandler.Text = "Canal " + channel + " : Campo " + field;

                var text = PropValueSearcher.ByName(response.Feeds.FirstOrDefault(), "Field" + field).ToString();
                if (field == "1") text += "°C";
                else if (field == "2") text += "°F";

                txt_readValue.Text = text;
                txt_readTime.Text = "Atualizado em: " + DateTime.Now;
            }

            handler.PostDelayed(RequestValueAsync, 5000);

        }
        
    }
}