﻿using Android.App;
using Android.Widget;
using Android.OS;
using LeitorThingspeak2.Activities;

/// <summary>
/// Tela do Menu Principal
/// </summary>

namespace LeitorThingspeak2
{
    [Activity(Label = "LeitorThingspeak2", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var btn_char = FindViewById<Button>(Resource.Id.btn_char);
            btn_char.Click += Btn_char_Click;

            var btn_matVis = FindViewById<Button>(Resource.Id.btn_matVis);
            btn_matVis.Click += Btn_matVis_Click;

            var btn_ref = FindViewById<Button>(Resource.Id.btn_ref);
            btn_ref.Click += Btn_ref_Click;

            var btn_han = FindViewById<Button>(Resource.Id.btn_handler);
            btn_han.Click += Btn_han_Click;

            var btn_hanChar = FindViewById<Button>(Resource.Id.btn_handlerChar);
            btn_hanChar.Click += Btn_hanChar_Click;
            
        }

        // Eventos de clique do botão

        private void Btn_hanChar_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(AsyncChartActivity));
        }

        private void Btn_han_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(HandlerExampleActivity));
        }

        private void Btn_matVis_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(MatlabVisualizationActivity));
        }

        private void Btn_char_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(ChartActivity));
        }

        private void Btn_ref_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(RefActivity));
        }
    }
}

