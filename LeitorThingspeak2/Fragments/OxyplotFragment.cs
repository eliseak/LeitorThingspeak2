using System;
using Android.App;
using Android.OS;
using Android.Views;
using LeitorThingspeak2.Utils;
using OxyPlot.Xamarin.Android;

namespace LeitorThingspeak2
{
    public class OxyplotFragment : Fragment
    {
        private PlotView plotView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        // Inflando o fragmento
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.OxyplotTab, container, false);
            plotView = view.FindViewById<PlotView>(Resource.Id.plot_view);

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

            // Cria o gráfico com a biblioteca Oxyplot
            if (response != null) new LinearOxyPlot(plotView, field).Create(response);
        }
        
    }
}