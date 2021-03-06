﻿using System;
using Android.App;
using Android.OS;
using Android.Views;
using LeitorThingspeak2.Utils;
using LeitorThingspeak2.Utils.Charts;
using Microcharts.Droid;

/// <summary>
/// Fragment que cria um gráfico com a bibliotada Microcharts
/// </summary>

namespace LeitorThingspeak2
{
    public class MicrochartsFragment : Fragment
    {
        private View view;
        private ChartView chartView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
        }

        // Inflando fragment
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            base.OnCreateView(inflater, container, savedInstanceState); 

            view = inflater.Inflate(Resource.Layout.MicrochartTab, container, false);
            chartView = view.FindViewById<ChartView>(Resource.Id.chartView);

            return view;
        }

        public async override void OnStart()
        {
            base.OnStart();

            // Requisição de dados
            var channel = Resources.GetString(Resource.String.channel);
            var field = Resources.GetString(Resource.String.field);
            var results = Convert.ToInt32(Resources.GetString(Resource.String.num_results));

            var response = await new RequestThingSpeakData(channel, field).CustomAsync(results);

            // Cria o gráfico com a biblioteca Microcharts
            if (response != null) new LinearMicrocharts(chartView, field).Create(response);
        }

    }
}