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

namespace LeitorThingspeak2
{
    [Activity(Label = "ChartActivity")]
    public class RefActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.References);
        }

        protected override async void OnStart()
        {
            base.OnStart();
            var channel = Resources.GetString(Resource.String.channel);
            var teste = await new RequestThingSpeakData(channel).DefaultAsync();
            Console.WriteLine(teste.ToString());
        }
    }
}