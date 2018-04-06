using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LeitorThingspeak2.Utils;
using OxyPlot.Xamarin.Android;

/// <summary>
/// Atualiza automaticamente o gráfico Oxyplot por meio do objeto Handler.
/// </summary>

namespace LeitorThingspeak2.Fragments
{
    public class AsyncOxyplotFragment : Fragment
    {
        private Context context; 
        private PlotView plotView;
        private Handler handler;
        private int time = 60000;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your fragment here
        }

        // Inflando o Fragment
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.OxyplotTab, container, false);
            context = view.Context;

            plotView = view.FindViewById<PlotView>(Resource.Id.plot_view);

            return view;
        }

        public override async void OnStart()
        {
            base.OnStart();

            // Requisição de dados
            var channel = Resources.GetString(Resource.String.channel);
            var field = Resources.GetString(Resource.String.field);
            var results = Convert.ToInt32(Resources.GetString(Resource.String.num_results));

            var response = await new RequestThingSpeakData(channel, field).CustomAsync(results);


            // Cria o gráfico
            if (response != null) new LinearOxyPlot(plotView, field).Create(response);

            // Seta handler para atualizar os dados 
            handler = new Handler();
            handler.PostDelayed(UpdateChartAsync, time);

        }

        // Método que atualiza o gráfico
        private async void UpdateChartAsync()
        {
            Toast.MakeText(context, "!", ToastLength.Short).Show();
            
            // Requisição dos dados
            var channel = Resources.GetString(Resource.String.channel);
            var field = Resources.GetString(Resource.String.field);

            var response = await new RequestThingSpeakData(channel, field).CustomAsync(25);

            // Atualiza o gráfico
            if (response != null) new LinearOxyPlot(plotView, field).Update(response.Feeds);
            
            // Seta handler para ser chamado novamente
            handler.PostDelayed(UpdateChartAsync, time);
        }

        public override void OnPause()
        {
            base.OnPause();

            // Encerra handler
            try { 
            handler.RemoveCallbacks(UpdateChartAsync);
            }
            catch(Exception e)
            {
                Toast.MakeText(context, e.Message, ToastLength.Short).Show();
            }

        }
    }


}